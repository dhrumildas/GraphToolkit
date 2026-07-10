using System;
using System.Collections.Generic;
using UnityEngine;
namespace FuzzyGraph.Runtime
{
    public static class WriteBackProcessor
    {
        public static int ApplyAll(PersistentContext context, IEnumerable<RuntimeWriteBack> writeBacks)
        {
            if (context == null || writeBacks == null)
                return 0;

            int appliedCount = 0;

            foreach(RuntimeWriteBack writeBack in writeBacks)
            {
                if(Apply(context, writeBack))
                    appliedCount++;
            }
            return appliedCount;
        }

        private static bool Apply(PersistentContext context, RuntimeWriteBack writeBack)
        {
            if(context == null || writeBack == null) return false;

            if (string.IsNullOrWhiteSpace(writeBack.targetKey))
            {
                Debug.LogWarning(
                    "Write-back failed because targetKey was empty.");

                return false;
            }

            return writeBack.operation switch
            {
                WriteBackOperation.Set => ApplySet(context, writeBack),
                WriteBackOperation.Add => ApplyNumeric(context, writeBack, subtract:false),
                WriteBackOperation.Subtract => ApplyNumeric(context, writeBack, subtract:true),
                WriteBackOperation.Toggle => ApplyToggle(context, writeBack),
                _=> false
            };
        }

        private static bool ApplyToggle(PersistentContext context, RuntimeWriteBack writeBack)
        {
            bool valExists = context.TryGet(writeBack.targetKey, out FuzzyValue currentVal);

            // A missing bool is to be tagged as false and toggled to true
            if(!valExists)
            {
                context.Set(writeBack.targetKey, FuzzyValue.FromBool(true));
                return true;
            }

            if(currentVal.type != FuzzyValueType.Bool)
            {
                Debug.LogWarning(
                    $"Write-back '{writeBack.targetKey}' failed because " +
                    "Toggle requires a bool value.");

                return false;
            }

            context.Set(writeBack.targetKey, FuzzyValue.FromBool(!currentVal.boolVal));
            return true;
        }

        private static bool ApplyNumeric(PersistentContext context, RuntimeWriteBack writeBack, bool subtract)
        {
            bool valExists = context.TryGet(writeBack.targetKey, out FuzzyValue currentVal);
            if(!valExists)
            {
                return CreateNumericVal(context, writeBack, subtract);
            }
            if (currentVal.type != writeBack.val.type)
            {
                Debug.LogWarning(
                    $"Write-back '{writeBack.targetKey}' failed because " + 
                    $"the stored type '{currentVal.type}' does NOT match" + 
                    $"the incoming type {writeBack.val.type}.");
                return false;
            }

            switch (currentVal.type)
            {
                case FuzzyValueType.Int:
                    {
                        int amount = writeBack.val.intVal;

                        int result = subtract
                            ? currentVal.intVal - amount
                            : currentVal.intVal + amount;

                        context.Set(
                            writeBack.targetKey,
                            FuzzyValue.FromInt(result));

                        return true;
                    }

                case FuzzyValueType.Float:
                    {
                        float amount = writeBack.val.floatVal;

                        float result = subtract
                            ? currentVal.floatVal - amount
                            : currentVal.floatVal + amount;

                        context.Set(
                            writeBack.targetKey,
                            FuzzyValue.FromFloat(result));

                        return true;
                    }

                default:
                    Debug.LogWarning(
                        $"Write-back '{writeBack.targetKey}' failed because " +
                        $"{currentVal.type} is not numeric.");

                    return false;
            }
        }

        private static bool CreateNumericVal(PersistentContext context, RuntimeWriteBack writeBack, bool subtract)
        {
            switch (writeBack.val.type)
            {
                case FuzzyValueType.Int:
                    {
                        int val = subtract ? -writeBack.val.intVal : writeBack.val.intVal;
                        context.Set(writeBack.targetKey, FuzzyValue.FromInt(val));
                        return true;
                    }
                case FuzzyValueType.Float:
                    {
                        float val = subtract ? -writeBack.val.floatVal : writeBack.val.floatVal;
                        context.Set(writeBack.targetKey, FuzzyValue.FromFloat(val));
                        return true;
                    }
                default:
                    Debug.LogWarning(
                        $"Write-back '{writeBack.targetKey}' could NOT be " +
                        "created because Add and Subtract require an int " + 
                        " or float value.");
                    return false;
            }
        }

        private static bool ApplySet(PersistentContext context, RuntimeWriteBack writeBack)
        {
            context.Set(writeBack.targetKey, writeBack.val); return true;
        }
    }
}