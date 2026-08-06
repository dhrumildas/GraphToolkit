using System;
using FuzzyGraph.Runtime;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    [Serializable]
    [UseWithGraph(typeof(FuzzyGraph2Asset))]
    public sealed class WriteBackNode : Node
    {
        public const string TargetKeyOptionName = "targetKey";
        public const string OperationOptionName = "operation";
        public const string ValueTypeOptionName = "valueType";

        public const string BoolValueOptionName = "boolValue";
        public const string IntValueOptionName = "intValue";
        public const string FloatValueOptionName = "floatValue";
        public const string StringValueOptionName = "stringValue";

        public const string ConsequencePortName = "consequence";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            base.OnDefineOptions(context);

            // state key getting changed
            context
                .AddOption<string>(TargetKeyOptionName)
                .WithDisplayName("Target Key")
                .WithDefaultValue("Player.FoundPitRoute")
                .Build();

            // set add subtract or flip
            context
                .AddOption<WriteBackOperation>(OperationOptionName)
                .WithDisplayName("Operation")
                .WithDefaultValue(WriteBackOperation.Set)
                .Build();

            // tells the compiler which value matters
            context
                .AddOption<FuzzyValueType>(ValueTypeOptionName)
                .WithDisplayName("Value Type")
                .WithDefaultValue(FuzzyValueType.Bool)
                .Build();

            // only the selected type gets compiled
            context
                .AddOption<bool>(BoolValueOptionName)
                .WithDisplayName("Bool Value")
                .WithDefaultValue(true)
                .Build();

            context
                .AddOption<int>(IntValueOptionName)
                .WithDisplayName("Int Value")
                .WithDefaultValue(1)
                .Build();

            context
                .AddOption<float>(FloatValueOptionName)
                .WithDisplayName("Float Value")
                .WithDefaultValue(1f)
                .Build();

            context
                .AddOption<string>(StringValueOptionName)
                .WithDisplayName("String Value")
                .WithDefaultValue("none")
                .Build();
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            // selected consequence owns this state change
            context.AddInputPort(ConsequencePortName).WithDisplayName("Consequence").Build();
        }
    }
}
