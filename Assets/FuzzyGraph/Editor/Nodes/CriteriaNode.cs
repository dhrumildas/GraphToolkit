using Unity.GraphToolkit.Editor;
using UnityEngine;
using System;
using FuzzyGraph.Runtime;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class CriteriaNode : Node
    {
        public const string fieldKeyOption = "fieldKey";
        public const string valTypeOption = "valType";
        public const string criteriaOperatorOption = "criteriaOperator";
        public const string expectedBoolOption = "expectedBool";
        public const string expectedIntOption = "expectedInt";
        public const string expectedFloatOption = "expectedFloat";
        public const string expectedStringOption = "expectedString";
        public const string weightOption = "weight";
        public const string evalModeOption = "evalMode";
        public const string fuzzyMaxOption = "fuzzyMax";
        public const string fuzzyMinOption = "fuzzyMin";
        public const string fuzzyFallOffOption = "fuzzyFallOff";
        public const string ruleOutPort = "rule";
        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<string>(fieldKeyOption)
                .WithDisplayName("Field Key")
                .WithDefaultValue("Guard.Relationship")
                .Delayed();

            context.AddOption<FuzzyValueType>(valTypeOption)
                .WithDisplayName("Value Type")
                .WithDefaultValue(FuzzyValueType.Int);

            context.AddOption<CriteriaOperator>(criteriaOperatorOption)
                .WithDisplayName("Critera Operator")
                .WithDefaultValue(CriteriaOperator.LessThanOrEqual);

            context.AddOption<bool>(expectedBoolOption)
                .WithDisplayName("Expected Bool")
                .WithDefaultValue(true);

            context.AddOption<int>(expectedIntOption)
                .WithDisplayName("Expected Int")
                .WithDefaultValue(3)
                .Delayed();

            context.AddOption<float>(expectedFloatOption)
                .WithDisplayName("Expected Float")
                .WithDefaultValue(0f)
                .Delayed();

            context.AddOption<string>(expectedStringOption)
                .WithDisplayName("Expected String")
                .WithDefaultValue(string.Empty)
                .Delayed();

            context.AddOption<float>(weightOption)
                .WithDisplayName("Weight")
                .WithDefaultValue(3f)
                .Delayed();

            context.AddOption<FuzzyEvaluationMode>(evalModeOption)
                .WithDisplayName("Evaluation Mode")
                .WithDefaultValue(FuzzyEvaluationMode.FuzzyLow);

            context.AddOption<float>(fuzzyMaxOption)
                .WithDisplayName("Fuzzy Max")
                .WithDefaultValue(8f)
                .Delayed();

            context.AddOption<float>(fuzzyMinOption)
                .WithDisplayName("Fuzzy Min")
                .WithDefaultValue(2f)
                .Delayed();

            context.AddOption<float>(fuzzyFallOffOption)
                .WithDisplayName("Fuzzy FallOff")
                .WithDefaultValue(1f)
                .Delayed();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort(ruleOutPort)
                .WithDisplayName("Rule")
                .Build();
        }
    }
}
