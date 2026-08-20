using System;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [UseWithGraph(typeof(OptionSandboxGraph))]
    public sealed class CriterionPortProbeNode : Node
    {
        // The ONLY node option in this experiment.
        private const string ModeOptionName = "mode";

        // Keep these names aligned with the real CriterionNode semantics.
        private const string VariableIdPortName = "variableId";
        private const string SetNamePortName = "setName";
        private const string ShapePortName = "shape";
        private const string MinimumPortName = "minimum";
        private const string MaximumPortName = "maximum";
        private const string FirstPortName = "first";
        private const string SecondPortName = "second";
        private const string ThirdPortName = "third";
        private const string FourthPortName = "fourth";
        private const string ExpectedBoolPortName = "expectedBool";
        private const string ExpectedIdPortName = "expectedId";
        private const string ComparisonPortName = "comparison";
        private const string ComparisonValuePortName = "comparisonValue";
        private const string ComparisonValue2PortName = "comparisonValue2";

        private const string CriteriaAPortName = "criteriaA";
        private const string CriteriaBPortName = "criteriaB";
        private const string RulesPortName = "rules";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            context
                .AddOption<CriterionMode>(ModeOptionName)
                .WithDisplayName("Mode")
                .WithDefaultValue(CriterionMode.BoolEquals)
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            CriterionMode mode = CriterionMode.BoolEquals;

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

                    // Critical test: enum stored as an inline input-port value.
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

                    // Critical test: second enum stored as an inline input-port value.
                    context
                        .AddInputPort<NumberComparison>(ComparisonPortName)
                        .WithDisplayName("Comparison")
                        .Build();

                    context
                        .AddInputPort<float>(ComparisonValuePortName)
                        .WithDisplayName("Compare A")
                        .Build();

                    // Keep B visible for every comparison for now.
                    // Nested dynamic hiding is deliberately out of scope.
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