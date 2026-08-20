using System;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    public enum CriterionMode
    {
        FuzzyNumber,
        BoolEquals,
        IdEquals,
        NumberCompare,
        And,
        Or,
        Not,
        Exists,
        DoesNotExist
    }

    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class CriterionNode : Node
    {
        public const string ModeOptionName = "mode";
        public const string VariableIdOptionName = "variableId";

        public const string SetNameOptionName = "setName";
        public const string ShapeOptionName = "shape";
        public const string MinimumOptionName = "minimum";
        public const string MaximumOptionName = "maximum";
        public const string FirstOptionName = "first";
        public const string SecondOptionName = "second";
        public const string ThirdOptionName = "third";
        public const string FourthOptionName = "fourth";

        public const string ExpectedBoolOptionName = "expectedBool";
        public const string ExpectedIdOptionName = "expectedId";

        public const string ComparisonOptionName = "comparison";
        public const string ComparisonValueOptionName = "comparisonValue";
        public const string ComparisonValue2OptionName = "comparisonValue2";

        public const string CriteriaAPortName = "criteriaA";
        public const string CriteriaBPortName = "criteriaB";
        public const string RulesPortName = "rules";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // mode picks the bits that matter
            context
                .AddOption<CriterionMode>(ModeOptionName)
                .WithDisplayName("Mode")
                .WithDefaultValue(CriterionMode.FuzzyNumber)
                .Build();

            // state key used by leaf modes
            context
                .AddOption<string>(VariableIdOptionName)
                .WithDisplayName("Variable ID")
                .WithDefaultValue("Player.DistanceToCarpet")
                .Build();

            // fuzzy number stuff
            context
                .AddOption<string>(SetNameOptionName)
                .WithDisplayName("Set")
                .WithDefaultValue("Near")
                .Build();

            context
                .AddOption<FuzzySetShape>(ShapeOptionName)
                .WithDisplayName("Shape")
                .WithDefaultValue(FuzzySetShape.Low)
                .Build();

            context
                .AddOption<float>(MinimumOptionName)
                .WithDisplayName("Minimum")
                .WithDefaultValue(0f)
                .Build();

            context
                .AddOption<float>(MaximumOptionName)
                .WithDisplayName("Maximum")
                .WithDefaultValue(10f)
                .Build();

            context
                .AddOption<float>(FirstOptionName)
                .WithDisplayName("Point A")
                .WithDefaultValue(0f)
                .Build();

            context
                .AddOption<float>(SecondOptionName)
                .WithDisplayName("Point B")
                .WithDefaultValue(2.5f)
                .Build();

            context
                .AddOption<float>(ThirdOptionName)
                .WithDisplayName("Point C")
                .WithDefaultValue(0f)
                .Build();

            context
                .AddOption<float>(FourthOptionName)
                .WithDisplayName("Point D")
                .WithDefaultValue(0f)
                .Build();

            // exact bool stuff
            context
                .AddOption<bool>(ExpectedBoolOptionName)
                .WithDisplayName("Expected Bool")
                .WithDefaultValue(true)
                .Build();

            // exact string or id stuff
            context
                .AddOption<string>(ExpectedIdOptionName)
                .WithDisplayName("Expected ID")
                .WithDefaultValue("lie")
                .Build();

            // exact number stuff
            context
                .AddOption<NumberComparison>(ComparisonOptionName)
                .WithDisplayName("Comparison")
                .WithDefaultValue(NumberComparison.LessThanOrEqual)
                .Build();

            context
                .AddOption<float>(ComparisonValueOptionName)
                .WithDisplayName("Compare A")
                .WithDefaultValue(3f)
                .Build();

            context
                .AddOption<float>(ComparisonValue2OptionName)
                .WithDisplayName("Compare B")
                .WithDefaultValue(7f)
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            CriterionMode mode = CriterionMode.FuzzyNumber;

            INodeOption modeOption = GetNodeOptionByName(ModeOptionName);

            if (modeOption != null)
            {
                modeOption.TryGetValue<CriterionMode>(out mode);
            }

            switch (mode)
            {
                case CriterionMode.And:
                case CriterionMode.Or:
                    // binary modes need two child criteria
                    context.AddInputPort(CriteriaAPortName).WithDisplayName("Criteria A").Build();

                    context.AddInputPort(CriteriaBPortName).WithDisplayName("Criteria B").Build();
                    break;

                case CriterionMode.Not:
                    // not only flips one child criterion
                    context.AddInputPort(CriteriaAPortName).WithDisplayName("Criteria").Build();
                    break;
            }

            // every mode sends one result onward
            context.AddOutputPort(RulesPortName).WithDisplayName("Rules").Build();
        }

    }
}