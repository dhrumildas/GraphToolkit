using System;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class CriterionNodeV2 : Node
    {
        public const string ModeOptionName = "mode";

        public const string VariableIdPortName = "variableId";

        public const string SetNamePortName = "setName";
        public const string ShapePortName = "shape";
        public const string MinimumPortName = "minimum";
        public const string MaximumPortName = "maximum";
        public const string FirstPortName = "first";
        public const string SecondPortName = "second";
        public const string ThirdPortName = "third";
        public const string FourthPortName = "fourth";

        public const string ExpectedBoolPortName = "expectedBool";
        public const string ExpectedIdPortName = "expectedId";

        public const string ComparisonPortName = "comparison";
        public const string ComparisonValuePortName = "comparisonValue";
        public const string ComparisonValue2PortName = "comparisonValue2";

        public const string CriteriaAPortName = "criteriaA";
        public const string CriteriaBPortName = "criteriaB";
        public const string RulesPortName = "rules";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            context
                .AddOption<CriterionMode>(ModeOptionName)
                .WithDisplayName("Mode")
                .WithDefaultValue(CriterionMode.FuzzyNumber)
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            CriterionMode mode = CriterionMode.FuzzyNumber;

            INodeOption modeOption = GetNodeOptionByName(ModeOptionName);

            if (modeOption != null)
            {
                modeOption.TryGetValue(out mode);
            }

            switch (mode)
            {
                case CriterionMode.FuzzyNumber:
                    AddVariableId(context);

                    context
                        .AddInputPort<string>(SetNamePortName)
                        .WithDisplayName("Set")
                        .Build();

                    context
                        .AddInputPort<FuzzySetShape>(ShapePortName)
                        .WithDisplayName("Shape")
                        .Build();

                    context
                        .AddInputPort<float>(MinimumPortName)
                        .WithDisplayName("Minimum")
                        .Build();

                    context
                        .AddInputPort<float>(MaximumPortName)
                        .WithDisplayName("Maximum")
                        .Build();

                    context
                        .AddInputPort<float>(FirstPortName)
                        .WithDisplayName("Point A")
                        .Build();

                    context
                        .AddInputPort<float>(SecondPortName)
                        .WithDisplayName("Point B")
                        .Build();

                    context
                        .AddInputPort<float>(ThirdPortName)
                        .WithDisplayName("Point C")
                        .Build();

                    context
                        .AddInputPort<float>(FourthPortName)
                        .WithDisplayName("Point D")
                        .Build();
                    break;

                case CriterionMode.BoolEquals:
                    AddVariableId(context);

                    context
                        .AddInputPort<bool>(ExpectedBoolPortName)
                        .WithDisplayName("Expected Bool")
                        .Build();
                    break;

                case CriterionMode.IdEquals:
                    AddVariableId(context);

                    context
                        .AddInputPort<string>(ExpectedIdPortName)
                        .WithDisplayName("Expected ID")
                        .Build();
                    break;

                case CriterionMode.NumberCompare:
                    AddVariableId(context);

                    context
                        .AddInputPort<NumberComparison>(ComparisonPortName)
                        .WithDisplayName("Comparison")
                        .Build();

                    context
                        .AddInputPort<float>(ComparisonValuePortName)
                        .WithDisplayName("Compare A")
                        .Build();

                    context
                        .AddInputPort<float>(ComparisonValue2PortName)
                        .WithDisplayName("Compare B")
                        .Build();
                    break;

                case CriterionMode.And:
                case CriterionMode.Or:
                    context
                        .AddInputPort(CriteriaAPortName)
                        .WithDisplayName("Criteria A")
                        .Build();

                    context
                        .AddInputPort(CriteriaBPortName)
                        .WithDisplayName("Criteria B")
                        .Build();
                    break;

                case CriterionMode.Not:
                    context
                        .AddInputPort(CriteriaAPortName)
                        .WithDisplayName("Criteria")
                        .Build();
                    break;

                case CriterionMode.Exists:
                case CriterionMode.DoesNotExist:
                    AddVariableId(context);
                    break;
            }

            context
                .AddOutputPort(RulesPortName)
                .WithDisplayName("Rules")
                .Build();
        }

        private static void AddVariableId(IPortDefinitionContext context)
        {
            context
                .AddInputPort<string>(VariableIdPortName)
                .WithDisplayName("Variable ID")
                .Build();
        }
    }
}