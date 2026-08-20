using System;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    /// <summary>
    /// Sandbox-only visual replica of the production CriterionNode.
    /// It is intentionally restricted to OptionSandboxGraph and does not
    /// replace, modify, compile, or execute the real CriterionNode.
    /// </summary>
    [Serializable]
    [UseWithGraph(typeof(OptionSandboxGraph))]
    public sealed class EnumOptionProbeNode : Node
    {
        private const string ModeOptionName = "replicaMode";
        private const string VariableIdOptionName = "replicaVariableId";
        private const string ShapeOptionName = "replicaShape";
        private const string ComparisonOptionName = "replicaComparison";

        private const string CriteriaAPortName = "replicaCriteriaA";
        private const string CriteriaBPortName = "replicaCriteriaB";
        private const string RulesPortName = "replicaRules";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // Always-visible controls.
            context
                .AddOption<FuzzyGraph2.Editor.CriterionMode>(ModeOptionName)
                .WithDisplayName("Mode")
                .WithDefaultValue(FuzzyGraph2.Editor.CriterionMode.FuzzyNumber)
                .Build();

            context
                .AddOption<string>(VariableIdOptionName)
                .WithDisplayName("Variable ID")
                .WithDefaultValue("Player.DistanceToCarpet")
                .Build();

            // These stay visible for this experiment because they are enums
            // that can themselves drive another level of dynamic structure.
            context
                .AddOption<FuzzySetShape>(ShapeOptionName)
                .WithDisplayName("Fuzzy Shape")
                .WithDefaultValue(FuzzySetShape.Low)
                .Build();

            context
                .AddOption<NumberComparison>(ComparisonOptionName)
                .WithDisplayName("Comparison")
                .WithDefaultValue(NumberComparison.LessThanOrEqual)
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            FuzzyGraph2.Editor.CriterionMode mode =
                FuzzyGraph2.Editor.CriterionMode.FuzzyNumber;

            FuzzySetShape shape = FuzzySetShape.Low;
            NumberComparison comparison = NumberComparison.LessThanOrEqual;

            INodeOption modeOption = GetNodeOptionByName(ModeOptionName);
            if (modeOption != null)
            {
                modeOption.TryGetValue<FuzzyGraph2.Editor.CriterionMode>(out mode);
            }

            INodeOption shapeOption = GetNodeOptionByName(ShapeOptionName);
            if (shapeOption != null)
            {
                shapeOption.TryGetValue<FuzzySetShape>(out shape);
            }

            INodeOption comparisonOption = GetNodeOptionByName(ComparisonOptionName);
            if (comparisonOption != null)
            {
                comparisonOption.TryGetValue<NumberComparison>(out comparison);
            }

            switch (mode)
            {
                case FuzzyGraph2.Editor.CriterionMode.FuzzyNumber:
                    AddFuzzyPorts(context, shape);
                    break;

                case FuzzyGraph2.Editor.CriterionMode.BoolEquals:
                    context
                        .AddInputPort<bool>("Expected Bool")
                        .Build();
                    break;

                case FuzzyGraph2.Editor.CriterionMode.IdEquals:
                    context
                        .AddInputPort<string>("Expected ID")
                        .Build();
                    break;

                case FuzzyGraph2.Editor.CriterionMode.NumberCompare:
                    context
                        .AddInputPort<float>("Compare A")
                        .Build();

                    if (comparison == NumberComparison.InclusiveRange)
                    {
                        context
                            .AddInputPort<float>("Compare B")
                            .Build();
                    }
                    break;

                case FuzzyGraph2.Editor.CriterionMode.And:
                case FuzzyGraph2.Editor.CriterionMode.Or:
                    context
                        .AddInputPort(CriteriaAPortName)
                        .WithDisplayName("Criteria A")
                        .Build();

                    context
                        .AddInputPort(CriteriaBPortName)
                        .WithDisplayName("Criteria B")
                        .Build();
                    break;

                case FuzzyGraph2.Editor.CriterionMode.Not:
                    context
                        .AddInputPort(CriteriaAPortName)
                        .WithDisplayName("Criteria")
                        .Build();
                    break;

                case FuzzyGraph2.Editor.CriterionMode.Exists:
                case FuzzyGraph2.Editor.CriterionMode.DoesNotExist:
                    // Variable ID is enough for these modes.
                    break;
            }

            context
                .AddOutputPort(RulesPortName)
                .WithDisplayName("Rules")
                .Build();
        }

        private static void AddFuzzyPorts(
            IPortDefinitionContext context,
            FuzzySetShape shape)
        {
            context
                .AddInputPort<string>("Set")
                .Build();

            context
                .AddInputPort<float>("Minimum")
                .Build();

            context
                .AddInputPort<float>("Maximum")
                .Build();

            context
                .AddInputPort<float>("Point A")
                .Build();

            context
                .AddInputPort<float>("Point B")
                .Build();

            if (shape == FuzzySetShape.Range)
            {
                context
                    .AddInputPort<float>("Point C")
                    .Build();

                context
                    .AddInputPort<float>("Point D")
                    .Build();
            }
        }
    }
}