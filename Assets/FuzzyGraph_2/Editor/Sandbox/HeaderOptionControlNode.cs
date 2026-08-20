using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [UseWithGraph(typeof(OptionSandboxGraph))]
    public sealed class HeaderOptionControlNode : Node
    {
        protected override void OnDefineOptions(
            IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            context
                .AddOption<string>("testText")
                .WithDisplayName("Test Text")
                .WithDefaultValue("HELLO")
                .Build();
        }

        protected override void OnDefinePorts(
            IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context
                .AddOutputPort<float>("Result")
                .Build();
        }
    }
}