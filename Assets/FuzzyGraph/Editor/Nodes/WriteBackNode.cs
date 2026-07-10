using FuzzyGraph.Runtime;
using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class WriteBackNode : Node
    {
        public const string targetKeyOption = "targetKey";
        public const string operationOption = "operation";
        public const string valTypeOption = "valType";
        public const string boolValOption = "boolVal";
        public const string intValOption = "intVal";
        public const string floatValOption = "floatVal";
        public const string stringValOption = "stringVal";
        public const string ruleInputPort = "rule";
        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<string>(targetKeyOption)
                .WithDisplayName("Target Key")
                .WithDefaultValue("Guard.Suspicious")
                .Delayed();

            context.AddOption<WriteBackOperation>(operationOption)
                .WithDisplayName("Operation")
                .WithDefaultValue(WriteBackOperation.Set);

            context.AddOption<FuzzyValueType>(valTypeOption)
                .WithDisplayName("Value Type")
                .WithDefaultValue(FuzzyValueType.Bool);

            context.AddOption<bool>(boolValOption)
                .WithDisplayName("Bool Value")
                .WithDefaultValue(true);

            context.AddOption<int>(intValOption)
                .WithDisplayName("Int Value")
                .WithDefaultValue(25)
                .Delayed();

            context.AddOption<float>(floatValOption)
                .WithDisplayName("Float Value")
                .WithDefaultValue(0f)
                .Delayed();

            context.AddOption<string>(stringValOption)
                .WithDisplayName("String Value")
                .WithDefaultValue("")
                .Delayed();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort(ruleInputPort)
                .WithDisplayName("Rule")
                .Build();
        }
    }
}
