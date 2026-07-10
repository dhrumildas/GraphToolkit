using Unity.GraphToolkit.Editor;
using System;
using FuzzyGraph.Runtime;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class ConsequenceNode : Node
    {
        public const string consequenceTypeOption = "consequenceType";
        public const string targetKeyOption = "targetKey";
        public const string valTypeOption = "valType";
        public const string boolValOption = "boolVal";
        public const string intValOption = "intVal";
        public const string floatValOption = "floatVal";
        public const string stringValOption = "stringVal";
        public const string payloadOption = "payload";
        public const string ruleInPort = "rule";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<ConsequenceType>(consequenceTypeOption)
                .WithDisplayName("Consequence Type")
                .WithDefaultValue(ConsequenceType.Dialogue);

            context.AddOption<string>("targetKey")
                .WithDisplayName("Target Key")
                .WithDefaultValue(string.Empty)
                .Delayed();

            context.AddOption<FuzzyValueType>(valTypeOption)
                .WithDisplayName("Value Type")
                .WithDefaultValue(FuzzyValueType.String);

            context.AddOption<bool>(boolValOption)
                .WithDisplayName("Boolean Value")
                .WithDefaultValue(false);

            context.AddOption<int>(intValOption)
                .WithDisplayName("Int Value")
                .WithDefaultValue(0)
                .Delayed();

            context.AddOption<float>(floatValOption)
                .WithDisplayName("Float Value")
                .WithDefaultValue(0f)
                .Delayed();

            context.AddOption<string>(stringValOption)
                .WithDisplayName("String Value")
                .WithDefaultValue(string.Empty)
                .Delayed();

            context.AddOption<string>(payloadOption)
                .WithDisplayName("Payload")
                .WithDefaultValue("I know what you did. Do NOT lie to me again.")
                .Delayed();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort(ruleInPort)
                .WithDisplayName("Rule")
                .Build();
        }
    }
}
