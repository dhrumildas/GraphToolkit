using System;
using System.Collections.Generic;
using FuzzyGraph.Runtime;
using UnityEngine;

namespace FuzzyGraph2.Runtime
{
    // compiled fuzzy set data
    [Serializable]
    public sealed class CompiledFuzzySet
    {
        public string name;
        public FuzzySetShape shape;

        // shape boundaries
        public float first;
        public float second;
        public float third;
        public float fourth;
    }

    // compiled numeric variable and its sets
    [Serializable]
    public sealed class CompiledFuzzyVariable
    {
        public string id;
        public string displayName;

        public float minimum;
        public float maximum = 1f;

        public List<CompiledFuzzySet> sets = new List<CompiledFuzzySet>();
    }

    // supported expression types
    public enum CompiledExpressionKind
    {
        FuzzyIs,
        BoolEquals,
        And,
        Or,
        Not
    }

    // compiled expression data
    [Serializable]
    public sealed class CompiledFuzzyExpression
    {
        public CompiledExpressionKind kind;

        // used by fuzzy and boolean expressions
        public string variableId;
        public string setName;
        public bool expectedBool;

        // used by and, or and not
        public List<int> childExpressionIndices = new List<int>();
    }

    // compiled sugeno rule
    [Serializable]
    public sealed class CompiledSugenoRule
    {
        public string ruleId;

        // points to an expression in the event
        public int antecedentExpressionIndex = -1;

        public float consequent;
    }

    // compiled narrative result band
    [Serializable]
    public sealed class CompiledOutputBand
    {
        public float minimumInclusive;
        public string outcomeId;

        public List<RuntimeConsequence> consequences = new List<RuntimeConsequence>();

        public List<RuntimeWriteBack> writeBacks = new List<RuntimeWriteBack>();
    }

    // all compiled data for one event
    [Serializable]
    public sealed class CompiledFuzzyEvent
    {
        public string eventId;

        public List<CompiledFuzzyVariable> variables = new List<CompiledFuzzyVariable>();

        public List<CompiledFuzzyExpression> expressions = new List<CompiledFuzzyExpression>();

        public List<CompiledSugenoRule> rules = new List<CompiledSugenoRule>();

        public List<CompiledOutputBand> outputBands = new List<CompiledOutputBand>();

        public CompiledOutputBand fallbackBand = new CompiledOutputBand();
    }

    // runtime data generated from the graph
    public sealed class RuntimeFuzzyGraph2 : ScriptableObject
    {
        [SerializeField]
        private List<CompiledFuzzyEvent> _events = new List<CompiledFuzzyEvent>();

        public IReadOnlyList<CompiledFuzzyEvent> Events => _events;

        // replaces the current compiled events
        public void SetCompiledEvents(IEnumerable<CompiledFuzzyEvent> compiledEvents)
        {
            if (compiledEvents == null)
                throw new ArgumentNullException(nameof(compiledEvents));

            List<CompiledFuzzyEvent> replacement = new List<CompiledFuzzyEvent>();

            HashSet<string> eventIds = new HashSet<string>(StringComparer.Ordinal);

            foreach (CompiledFuzzyEvent compiledEvent in compiledEvents)
            {
                if (compiledEvent == null)
                    throw new ArgumentException(
                        "The compiled graph contains a null event.",
                        nameof(compiledEvents)
                    );

                if (string.IsNullOrWhiteSpace(compiledEvent.eventId))
                    throw new ArgumentException(
                        "Every compiled event requires an event ID.",
                        nameof(compiledEvents)
                    );

                compiledEvent.eventId = compiledEvent.eventId.Trim();

                if (!eventIds.Add(compiledEvent.eventId))
                {
                    throw new ArgumentException(
                        $"More than one compiled event uses the ID "
                            + $"'{compiledEvent.eventId}'.",
                        nameof(compiledEvents)
                    );
                }

                replacement.Add(compiledEvent);
            }

            _events = replacement;
        }

        // tries to find an event by its exact id
        public bool TryGetEvent(string eventId, out CompiledFuzzyEvent compiledEvent)
        {
            compiledEvent = null;

            if (string.IsNullOrWhiteSpace(eventId))
                return false;

            foreach (CompiledFuzzyEvent candidate in _events)
            {
                if (string.Equals(candidate.eventId, eventId, StringComparison.Ordinal))
                {
                    compiledEvent = candidate;
                    return true;
                }
            }

            return false;
        }

        // gets an event or throws a clear error
        public CompiledFuzzyEvent GetEvent(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new ArgumentException("An event ID is required.", nameof(eventId));
            }

            if (!TryGetEvent(eventId.Trim(), out CompiledFuzzyEvent compiledEvent))
            {
                throw new KeyNotFoundException(
                    $"RuntimeFuzzyGraph2 does not contain an event " + $"named '{eventId}'."
                );
            }

            return compiledEvent;
        }
    }
}
