using System;
using FuzzyGraph.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class ConsequenceNode : Node
    {
        public const string OutcomeIdOptionName = "outcomeId";
        public const string MinimumOptionName = "minimum";
        public const string FallbackOptionName = "fallback";
        public const string RunActionOptionName = "runAction";

        // legacy key kept intentionally.
        // existing graph data may still contain this option,
        // but fuzzygraph2 now always compiles actions as fireevent.
        public const string TypeOptionName = "type";

        public const string TargetOptionName = "target";
        public const string PayloadOptionName = "payload";

        public const string EventPortName = "event";
        public const string WriteBacksPortName = "writeBacks";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            context.AddOption<string>(OutcomeIdOptionName).WithDisplayName("Outcome ID").WithDefaultValue("Vendor.RevealsSecret").Build();
            context.AddOption<float>(MinimumOptionName).WithDisplayName("Minimum").WithDefaultValue(0.7f).Build();
            context.AddOption<bool>(FallbackOptionName).WithDisplayName("Fallback").WithDefaultValue(false).Build();

            // all executable fg2 consequences are dispatched as fireevent.
            context.AddOption<bool>(RunActionOptionName).WithDisplayName("Fire Event").WithDefaultValue(true).Build();
            context.AddOption<string>(TargetOptionName).WithDisplayName("Target").WithDefaultValue("DialogueGraph").Build();
            context.AddOption<string>(PayloadOptionName).WithDisplayName("Payload").WithDefaultValue("VendorSecretRevealed").Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort(EventPortName).WithDisplayName("Event").Build();
            context.AddOutputPort(WriteBacksPortName).WithDisplayName("Write-Backs").Build();
        }
    }
}