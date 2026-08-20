using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [UseWithGraph(typeof(OptionSandboxGraph))]
    public sealed class DocumentedOptionProbeNode : Node
    {
        private const string PortCountOption = "portCount";

        protected override void OnDefineOptions(
            IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            context
                .AddOption<int>(PortCountOption)
                .WithDisplayName("Port Count")
                .WithDefaultValue(2)
                .Delayed()
                .Build();
        }

        protected override void OnDefinePorts(
            IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            int portCount = 2;

            INodeOption option =
                GetNodeOptionByName(PortCountOption);

            if (option != null)
                option.TryGetValue(out portCount);

            portCount = Mathf.Clamp(portCount, 1, 10);

            for (int i = 0; i < portCount; i++)
            {
                context
                    .AddInputPort<float>($"Input {i + 1}")
                    .Build();
            }

            context
                .AddOutputPort<float>("Output")
                .Build();
        }
    }
}