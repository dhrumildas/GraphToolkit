using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class RuleNode : Node
    {
        public const string RuleIdOptionName = "ruleId";
        public const string ConsequentOptionName = "consequent";

        public const string EventPortName = "event";
        public const string CriteriaPortName = "criteria";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // handy id for traces n errors
            context
                .AddOption<string>(RuleIdOptionName)
                .WithDisplayName("Rule ID")
                .WithDefaultValue("vendor_reveal_secret")
                .Build();

            // this is the rule's sugeno vote
            context
                .AddOption<float>(ConsequentOptionName)
                .WithDisplayName("Consequent")
                .WithDefaultValue(0.9f)
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            // event owns this rule
            context.AddInputPort(EventPortName).WithDisplayName("Event").Build();

            // condition spaghetti goes here later
            context.AddInputPort(CriteriaPortName).WithDisplayName("Criteria").Build();
        }
    }
}
