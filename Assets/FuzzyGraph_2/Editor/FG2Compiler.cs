using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace FuzzyGraph2.Editor
{
    public readonly struct FuzzyGraph2CompileReport
    {
        public int EventCount { get; }
        public int VariableCount { get; }
        public int ExpressionCount { get; }
        public int RuleCount { get; }
        public int BandCount { get; }
        public int FallbackCount { get; }
        public int ConsequenceCount { get; }
        public int WriteBackCount { get; }

        private FuzzyGraph2CompileReport(
            int eventCount,
            int variableCount,
            int expressionCount,
            int ruleCount,
            int bandCount,
            int fallbackCount,
            int consequenceCount,
            int writeBackCount
        )
        {
            EventCount = eventCount;
            VariableCount = variableCount;
            ExpressionCount = expressionCount;
            RuleCount = ruleCount;
            BandCount = bandCount;
            FallbackCount = fallbackCount;
            ConsequenceCount = consequenceCount;
            WriteBackCount = writeBackCount;
        }

        public static FuzzyGraph2CompileReport From(RuntimeFuzzyGraph2 graph)
        {
            int variables = 0;
            int expressions = 0;
            int rules = 0;
            int bands = 0;
            int fallbacks = 0;
            int consequences = 0;
            int writeBacks = 0;

            foreach (CompiledFuzzyEvent compiledEvent in graph.Events)
            {
                variables += compiledEvent.variables?.Count ?? 0;
                expressions += compiledEvent.expressions?.Count ?? 0;
                rules += compiledEvent.rules?.Count ?? 0;
                bands += compiledEvent.outputBands?.Count ?? 0;

                if (compiledEvent.outputBands != null)
                {
                    foreach (CompiledOutputBand band in compiledEvent.outputBands)
                    {
                        consequences += band?.consequences?.Count ?? 0;
                        writeBacks += band?.writeBacks?.Count ?? 0;
                    }
                }

                if (
                    compiledEvent.fallbackBand != null
                    && !string.IsNullOrWhiteSpace(compiledEvent.fallbackBand.outcomeId))
                {
                    fallbacks++;

                    consequences += compiledEvent.fallbackBand.consequences?.Count ?? 0;

                    writeBacks += compiledEvent.fallbackBand.writeBacks?.Count ?? 0;
                }
            }

            return new FuzzyGraph2CompileReport(
                graph.Events.Count,
                variables,
                expressions,
                rules,
                bands,
                fallbacks,
                consequences,
                writeBacks
            );
        }

        public string ToLogLine(string assetName)
        {
            return $"[fuzzygraph2] compiled '{assetName}' | "
                + $"events {EventCount} | "
                + $"variables {VariableCount} | "
                + $"expressions {ExpressionCount} | "
                + $"rules {RuleCount} | "
                + $"bands {BandCount} | "
                + $"fallbacks {FallbackCount} | "
                + $"consequences {ConsequenceCount} | "
                + $"write-backs {WriteBackCount}";
        }
    }

    public static class FG2Compiler
    {
        public static RuntimeFuzzyGraph2 Compile(FuzzyGraph2Asset graph,out FuzzyGraph2CompileReport report)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            // bake the editor nodes down
            RuntimeFuzzyGraph2 runtimeGraph = ScriptableObject.CreateInstance<RuntimeFuzzyGraph2>();

            List<CompiledFuzzyEvent> events = new List<CompiledFuzzyEvent>();

            foreach (EventNode eventNode in graph.GetNodes().OfType<EventNode>())
            {
                string eventId = GetOptionValue(eventNode,EventNode.EventIdOptionName,string.Empty);

                if (string.IsNullOrWhiteSpace(eventId))
                    throw new InvalidOperationException("an event node has no event id");


                events.Add(new CompiledFuzzyEvent { eventId = eventId.Trim() });
            }

            runtimeGraph.SetCompiledEvents(events);

            // count whatever got baked
            report = FuzzyGraph2CompileReport.From(runtimeGraph);

            return runtimeGraph;
        }

        private static T GetOptionValue<T>(Node node, string optionName, T fallback)
        {
            // bad option means fallback, no drama
            INodeOption option = node?.GetNodeOptionByName(optionName);

            if (option != null && option.TryGetValue(out T value))
                return value;

            return fallback;
        }
    }
}
