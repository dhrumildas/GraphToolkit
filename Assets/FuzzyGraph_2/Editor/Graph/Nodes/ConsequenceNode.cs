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
        public const string TypeOptionName = "type";
        public const string TargetOptionName = "target";
        public const string PayloadOptionName = "payload";

        public const string EventPortName = "event";
        public const string WriteBacksPortName = "writeBacks";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // id returned when this band wins
            context
                .AddOption<string>(OutcomeIdOptionName)
                .WithDisplayName("Outcome ID")
                .WithDefaultValue("Vendor.RevealsSecret")
                .Build();

            // output has to reach this value
            context
                .AddOption<float>(MinimumOptionName)
                .WithDisplayName("Minimum")
                .WithDefaultValue(0.7f)
                .Build();

            // fallback skips the numeric threshold
            context
                .AddOption<bool>(FallbackOptionName)
                .WithDisplayName("Fallback")
                .WithDefaultValue(false)
                .Build();

            // state-only outcomes can skip the action
            context
                .AddOption<bool>(RunActionOptionName)
                .WithDisplayName("Run Action")
                .WithDefaultValue(true)
                .Build();

            // old runtime already knows these actions
            context
                .AddOption<ConsequenceType>(TypeOptionName)
                .WithDisplayName("Action Type")
                .WithDefaultValue(ConsequenceType.FireEvent)
                .Build();

            context
                .AddOption<string>(TargetOptionName)
                .WithDisplayName("Target")
                .WithDefaultValue("DialogueGraph")
                .Build();

            context
                .AddOption<string>(PayloadOptionName)
                .WithDisplayName("Payload")
                .WithDefaultValue("VendorSecretRevealed")
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            // event owns this outcome
            context.AddInputPort(EventPortName).WithDisplayName("Event").Build();

            // state changes plug in later
            context.AddOutputPort(WriteBacksPortName).WithDisplayName("Write-Backs").Build();
        }
    }
}
