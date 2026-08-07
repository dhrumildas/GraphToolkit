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
        public const string ConsequencesPortName = "consequences";

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

            // outcomes hang off the event here
            context.AddOutputPort(ConsequencesPortName).WithDisplayName("Consequences").Build();

            // rules plug in here
            context.AddOutputPort(RulesPortName).WithDisplayName("Rules").Build();

        }
    }
}