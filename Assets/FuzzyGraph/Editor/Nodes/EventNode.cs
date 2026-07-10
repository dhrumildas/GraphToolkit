using UnityEngine;
using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class EventNode : Node
    {
        public const string eventIDOption = "eventID";
        public const string rulesOutPort = "rules";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<string>(eventIDOption)
                .WithDisplayName("Event ID")
                .WithDefaultValue("talkToGuard")
                .Delayed();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort(rulesOutPort)
                .WithDisplayName("Rules")
                .Build();
        }
    }
}
