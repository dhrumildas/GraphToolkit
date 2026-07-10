using Unity.GraphToolkit.Editor;
using System;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class RuleNode : Node
    {
        public const string ruleIDOption = "ruleID";
        public const string priorityOption = "priority";
        public const string eventInPort = "eventIn";
        public const string criteriaInPort = "criteriaIn";
        public const string consequencesOutPort = "consequences";
        public const string writeBacksOutPort = "writeBacks";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<string>(ruleIDOption)
                .WithDisplayName("Rule ID")
                .WithDefaultValue("suspicious_guard_response")
                .Delayed();

            context.AddOption<int>(priorityOption)
                .WithDisplayName("Priority")
                .WithDefaultValue(0)
                .Delayed();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort(eventInPort)
                .WithDisplayName("Event")
                .Build();

            context.AddInputPort(criteriaInPort)
                .WithDisplayName("Criteria")
                .Build();

            context.AddOutputPort(consequencesOutPort)
                .WithDisplayName("Consequences")
                .Build();

            context.AddOutputPort(writeBacksOutPort)
                .WithDisplayName("Write-Backs")
                .Build();
        }
    }
}
