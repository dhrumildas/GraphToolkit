using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [UseWithContext(typeof(OptionTestContextNode))]
    public sealed class OptionTestBlockNode : BlockNode
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
    }
}