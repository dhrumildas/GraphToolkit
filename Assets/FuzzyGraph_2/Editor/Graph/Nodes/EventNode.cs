using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class EventNode : Node
    {
        public const string EventIdOptionName = "eventId";
        public const string RulesPortName = "rules";

        protected override void OnDefineOptions(
            IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // gameplay uses this id later
            context
                .AddOption<string>(EventIdOptionName).WithDisplayName("Event ID").WithDefaultValue("Inspect").Build();
        }

        protected override void OnDefinePorts(
            IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            // rules plug in here
            context.AddOutputPort(RulesPortName).WithDisplayName("Rules").Build();
        }
    }
}