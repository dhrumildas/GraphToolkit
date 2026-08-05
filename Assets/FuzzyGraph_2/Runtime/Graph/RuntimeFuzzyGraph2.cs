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
        IdEquals,
        NumberCompare,
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
        public string expectedId;

        public NumberComparison numberComparison;
        public float comparisonValue;
        public float comparisonValue2;


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
        public SugenoNarrativeResult Resolve(string eventId, WorldStateQuery query)
        // rebuilds and runs one compiled event
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            CompiledFuzzyEvent compiledEvent = GetEvent(eventId);

            Dictionary<string, FuzzyVariableDefinition> variables = BuildVariables(compiledEvent);

            IFuzzyExpression[] expressions = BuildExpressions(compiledEvent, variables);

            List<SugenoRule> rules = BuildRules(compiledEvent, expressions);

            NarrativeOutputMapping outputMapping = BuildOutputMapping(compiledEvent);

            return SugenoNarrativeResolver.Resolve(rules, outputMapping, query);
        }

        private static Dictionary<string, FuzzyVariableDefinition> BuildVariables(CompiledFuzzyEvent compiledEvent)
        // rebuilds the event's fuzzy variables
        {
            if (compiledEvent.variables == null)
            {
                throw new InvalidOperationException(
                    $"Event '{compiledEvent.eventId}' has no variable collection."
                );
            }

            Dictionary<string, FuzzyVariableDefinition> variables = new Dictionary<
                string,
                FuzzyVariableDefinition
            >(StringComparer.Ordinal);

            foreach (CompiledFuzzyVariable compiledVariable in compiledEvent.variables)
            {
                if (compiledVariable == null)
                {
                    throw new InvalidOperationException(
                        $"Event '{compiledEvent.eventId}' contains " + "a null variable."
                    );
                }

                if (compiledVariable.sets == null)
                {
                    throw new InvalidOperationException(
                        $"Variable '{compiledVariable.id}' has no set collection."
                    );
                }

                List<FuzzySetDefinition> sets = new List<FuzzySetDefinition>();

                foreach (CompiledFuzzySet compiledSet in compiledVariable.sets)
                {
                    sets.Add(BuildSet(compiledSet));
                }

                FuzzyVariableDefinition variable = new FuzzyVariableDefinition(
                    compiledVariable.id,
                    compiledVariable.displayName,
                    compiledVariable.minimum,
                    compiledVariable.maximum,
                    sets
                );

                if (variables.ContainsKey(variable.Id))
                {
                    throw new InvalidOperationException(
                        $"Event '{compiledEvent.eventId}' contains more than "
                            + $"one fuzzy variable named '{variable.Id}'."
                    );
                }

                variables.Add(variable.Id, variable);
            }

            return variables;
        }

        private static FuzzySetDefinition BuildSet(CompiledFuzzySet compiledSet)
        // rebuild one fuzzy set from compiled data
        {
            if (compiledSet == null)
            {
                throw new InvalidOperationException(
                    "A compiled fuzzy variable contains a null set."
                );
            }

            switch (compiledSet.shape)
            {
                case FuzzySetShape.Low:
                    return FuzzySetDefinition.Low(
                        compiledSet.name,
                        full: compiledSet.first,
                        end: compiledSet.second
                    );

                case FuzzySetShape.Range:
                    return FuzzySetDefinition.Range(
                        compiledSet.name,
                        start: compiledSet.first,
                        fullStart: compiledSet.second,
                        fullEnd: compiledSet.third,
                        end: compiledSet.fourth
                    );

                case FuzzySetShape.High:
                    return FuzzySetDefinition.High(
                        compiledSet.name,
                        start: compiledSet.first,
                        full: compiledSet.second
                    );

                default:
                    throw new InvalidOperationException(
                        $"Unsupported compiled fuzzy-set shape: " + $"{compiledSet.shape}."
                    );
            }
        }

        private static IFuzzyExpression[] BuildExpressions(
            CompiledFuzzyEvent compiledEvent,
            IReadOnlyDictionary<string, FuzzyVariableDefinition> variables)
        // rebuilds all expressions used by the event
        {
            if (compiledEvent.expressions == null)
            {
                throw new InvalidOperationException(
                    $"Event '{compiledEvent.eventId}' has no expression collection."
                );
            }

            IFuzzyExpression[] expressions = new IFuzzyExpression[compiledEvent.expressions.Count];

            // 0 = unvisited, 1 = building, 2 = finished
            byte[] buildStates = new byte[compiledEvent.expressions.Count];

            for (int index = 0; index < compiledEvent.expressions.Count; index++)
            {
                BuildExpression(
                    index,
                    compiledEvent.expressions,
                    variables,
                    expressions,
                    buildStates
                );
            }

            return expressions;
        }

        private static IFuzzyExpression BuildExpression(
            int expressionIndex,
            IReadOnlyList<CompiledFuzzyExpression> compiledExpressions,
            IReadOnlyDictionary<string, FuzzyVariableDefinition> variables,
            IFuzzyExpression[] builtExpressions,
            byte[] buildStates)
        // recursively rebuilds one expression
        {
            if (expressionIndex < 0 || expressionIndex >= compiledExpressions.Count)
            {
                throw new InvalidOperationException(
                    $"Expression index {expressionIndex} is outside "
                        + "the compiled expression collection."
                );
            }

            if (buildStates[expressionIndex] == 2)
            {
                return builtExpressions[expressionIndex];
            }

            if (buildStates[expressionIndex] == 1)
            {
                throw new InvalidOperationException(
                    $"The compiled expression at index {expressionIndex} "
                        + "contains a circular connection."
                );
            }

            CompiledFuzzyExpression compiledExpression = compiledExpressions[expressionIndex];

            if (compiledExpression == null)
            {
                throw new InvalidOperationException(
                    $"The compiled expression at index {expressionIndex} is null."
                );
            }

            buildStates[expressionIndex] = 1;

            IFuzzyExpression expression;

            switch (compiledExpression.kind)
            {
                case CompiledExpressionKind.FuzzyIs:
                    {
                        if (
                            !variables.TryGetValue(
                                compiledExpression.variableId,
                                out FuzzyVariableDefinition variable
                            )
                        )
                        {
                            throw new InvalidOperationException(
                                $"Expression {expressionIndex} references unknown "
                                    + $"fuzzy variable '{compiledExpression.variableId}'."
                            );
                        }

                        FuzzySetDefinition set = variable.GetSet(compiledExpression.setName);

                        expression = new FuzzyIsStatement(variable, set);
                        break;
                    }

                case CompiledExpressionKind.BoolEquals:
                    expression = FuzzyExpression.BoolEquals(
                        compiledExpression.variableId,
                        compiledExpression.expectedBool
                    );
                    break;

                case CompiledExpressionKind.IdEquals:
                    expression =
                        FuzzyExpression.IdEquals(
                            compiledExpression.variableId,
                            compiledExpression.expectedId);
                    break;

                case CompiledExpressionKind.NumberCompare:
                    expression =
                        FuzzyExpression.NumberCompare(
                            compiledExpression.variableId,
                            compiledExpression.numberComparison,
                            compiledExpression.comparisonValue,
                            compiledExpression.comparisonValue2);
                    break;

                case CompiledExpressionKind.And:
                    expression = FuzzyExpression.And(
                        BuildMultipleChildren(
                            expressionIndex,
                            compiledExpression,
                            compiledExpressions,
                            variables,
                            builtExpressions,
                            buildStates,
                            "AND"
                        )
                    );
                    break;

                case CompiledExpressionKind.Or:
                    expression = FuzzyExpression.Or(
                        BuildMultipleChildren(
                            expressionIndex,
                            compiledExpression,
                            compiledExpressions,
                            variables,
                            builtExpressions,
                            buildStates,
                            "OR"
                        )
                    );
                    break;

                case CompiledExpressionKind.Not:
                    expression = FuzzyExpression.Not(
                        BuildSingleChild(
                            expressionIndex,
                            compiledExpression,
                            compiledExpressions,
                            variables,
                            builtExpressions,
                            buildStates
                        )
                    );
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported compiled expression kind: " + $"{compiledExpression.kind}."
                    );
            }

            builtExpressions[expressionIndex] = expression;
            buildStates[expressionIndex] = 2;

            return expression;
        }

        private static IFuzzyExpression[] BuildMultipleChildren(
            int expressionIndex,
            CompiledFuzzyExpression compiledExpression,
            IReadOnlyList<CompiledFuzzyExpression> compiledExpressions,
            IReadOnlyDictionary<string, FuzzyVariableDefinition> variables,
            IFuzzyExpression[] builtExpressions,
            byte[] buildStates,
            string operatorName)
        // rebuild the children of an and or or expression
        {
            if (
                compiledExpression.childExpressionIndices == null
                || compiledExpression.childExpressionIndices.Count < 2
            )
            {
                throw new InvalidOperationException(
                    $"{operatorName} expression {expressionIndex} requires "
                        + "at least two child expressions."
                );
            }

            IFuzzyExpression[] children = new IFuzzyExpression[
                compiledExpression.childExpressionIndices.Count
            ];

            for (int index = 0; index < compiledExpression.childExpressionIndices.Count; index++)
            {
                children[index] = BuildExpression(
                    compiledExpression.childExpressionIndices[index],
                    compiledExpressions,
                    variables,
                    builtExpressions,
                    buildStates
                );
            }

            return children;
        }

        private static IFuzzyExpression BuildSingleChild(
            int expressionIndex,
            CompiledFuzzyExpression compiledExpression,
            IReadOnlyList<CompiledFuzzyExpression> compiledExpressions,
            IReadOnlyDictionary<string, FuzzyVariableDefinition> variables,
            IFuzzyExpression[] builtExpressions,
            byte[] buildStates)
        // rebuild the child of a not expression

        {
            if (
                compiledExpression.childExpressionIndices == null
                || compiledExpression.childExpressionIndices.Count != 1
            )
            {
                throw new InvalidOperationException(
                    $"NOT expression {expressionIndex} requires exactly " + "one child expression."
                );
            }

            return BuildExpression(
                compiledExpression.childExpressionIndices[0],
                compiledExpressions,
                variables,
                builtExpressions,
                buildStates
            );
        }

        private static List<SugenoRule> BuildRules(
            CompiledFuzzyEvent compiledEvent,
            IReadOnlyList<IFuzzyExpression> expressions)
        // rebuild the event's sugeno rules
        {
            if (compiledEvent.rules == null)
            {
                throw new InvalidOperationException(
                    $"Event '{compiledEvent.eventId}' has no rule collection."
                );
            }

            List<SugenoRule> rules = new List<SugenoRule>();

            HashSet<string> ruleIds = new HashSet<string>(StringComparer.Ordinal);

            foreach (CompiledSugenoRule compiledRule in compiledEvent.rules)
            {
                if (compiledRule == null)
                {
                    throw new InvalidOperationException(
                        $"Event '{compiledEvent.eventId}' contains a null rule."
                    );
                }

                if (
                    compiledRule.antecedentExpressionIndex < 0
                    || compiledRule.antecedentExpressionIndex >= expressions.Count
                )
                {
                    throw new InvalidOperationException(
                        $"Rule '{compiledRule.ruleId}' references invalid "
                            + $"expression index "
                            + $"{compiledRule.antecedentExpressionIndex}."
                    );
                }

                SugenoRule rule = new SugenoRule(
                    compiledRule.ruleId,
                    expressions[compiledRule.antecedentExpressionIndex],
                    compiledRule.consequent
                );

                if (!ruleIds.Add(rule.Id))
                {
                    throw new InvalidOperationException(
                        $"Event '{compiledEvent.eventId}' contains more than "
                            + $"one rule named '{rule.Id}'."
                    );
                }

                rules.Add(rule);
            }

            return rules;
        }

        private static NarrativeOutputMapping BuildOutputMapping(CompiledFuzzyEvent compiledEvent)
        // rebuild the event's output bands and fallback
        {
            if (compiledEvent.outputBands == null)
            {
                throw new InvalidOperationException(
                    $"Event '{compiledEvent.eventId}' has no output-band collection."
                );
            }

            List<NarrativeOutputMapping.Band> outputBands = new List<NarrativeOutputMapping.Band>();

            foreach (CompiledOutputBand compiledBand in compiledEvent.outputBands)
            {
                outputBands.Add(BuildOutputBand(compiledBand));
            }

            if (compiledEvent.fallbackBand == null)
            {
                throw new InvalidOperationException(
                    $"Event '{compiledEvent.eventId}' has no authored fallback."
                );
            }

            NarrativeOutputMapping.Band fallbackBand = BuildOutputBand(compiledEvent.fallbackBand);

            return new NarrativeOutputMapping(outputBands, fallbackBand);
        }

        private static NarrativeOutputMapping.Band BuildOutputBand(CompiledOutputBand compiledBand)
        // rebuild one narrative output band
        {
            if (compiledBand == null)
            {
                throw new InvalidOperationException(
                    "The compiled event contains a null output band."
                );
            }

            return new NarrativeOutputMapping.Band(
                compiledBand.minimumInclusive,
                compiledBand.outcomeId,
                compiledBand.consequences,
                compiledBand.writeBacks
            );
        }
    }
}
