using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyGraph2.Runtime;
using FuzzyGraph.Runtime;
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

        private FuzzyGraph2CompileReport(int eventCount, int variableCount, int expressionCount, int ruleCount, int bandCount, int fallbackCount, int consequenceCount, int writeBackCount)
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
            int variables = 0, expressions = 0, rules = 0, bands = 0, fallbacks = 0, consequences = 0, writeBacks = 0;

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

                if (compiledEvent.fallbackBand != null && !string.IsNullOrWhiteSpace(compiledEvent.fallbackBand.outcomeId))
                {
                    fallbacks++;
                    consequences += compiledEvent.fallbackBand.consequences?.Count ?? 0;
                    writeBacks += compiledEvent.fallbackBand.writeBacks?.Count ?? 0;
                }
            }

            return new FuzzyGraph2CompileReport(graph.Events.Count, variables, expressions, rules, bands, fallbacks, consequences, writeBacks);
        }

        public string ToLogLine(string assetName)
        {
            return $"[fuzzygraph2] compiled '{assetName}' | events {EventCount} | variables {VariableCount} | expressions {ExpressionCount} | rules {RuleCount} | bands {BandCount} | fallbacks {FallbackCount} | consequences {ConsequenceCount} | write-backs {WriteBackCount}";
        }
    }

    public static class FG2Compiler
    {
        public static RuntimeFuzzyGraph2 Compile(FuzzyGraph2Asset graph, out FuzzyGraph2CompileReport report)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            RuntimeFuzzyGraph2 runtimeGraph = ScriptableObject.CreateInstance<RuntimeFuzzyGraph2>();
            List<CompiledFuzzyEvent> events = new List<CompiledFuzzyEvent>();

            foreach (EventNode eventNode in graph.GetNodes().OfType<EventNode>())
            {
                string eventId = GetOptionValue(eventNode, EventNode.EventIdOptionName, string.Empty);
                if (string.IsNullOrWhiteSpace(eventId)) throw new InvalidOperationException("an event node has no event id");

                CompiledFuzzyEvent compiledEvent = new CompiledFuzzyEvent { eventId = eventId.Trim() };
                Dictionary<CriterionNode, int> compiledCriteria = new Dictionary<CriterionNode, int>();
                HashSet<CriterionNode> buildingCriteria = new HashSet<CriterionNode>();

                Dictionary<CriterionNodeV2, int> compiledCriteriaV2 = new Dictionary<CriterionNodeV2, int>();
                HashSet<CriterionNodeV2> buildingCriteriaV2 = new HashSet<CriterionNodeV2>();

                IEnumerable<RuleNode> connectedRules = GetConnectedNodesFromOutput<RuleNode>(eventNode, EventNode.RulesPortName);

                foreach (RuleNode ruleNode in connectedRules)
                {
                    string ruleId = GetOptionValue(ruleNode, RuleNode.RuleIdOptionName, string.Empty);
                    if (string.IsNullOrWhiteSpace(ruleId)) throw new InvalidOperationException($"event '{eventId}' has a rule with no id");

                    List<CriterionNode> criteria = GetConnectedNodesFromInput<CriterionNode>(ruleNode, RuleNode.CriteriaPortName).ToList();

                    List<CriterionNodeV2> v2Criteria = GetConnectedNodesFromInput<CriterionNodeV2>(ruleNode,RuleNode.CriteriaPortName).ToList();

                    if (criteria.Count + v2Criteria.Count != 1) throw new InvalidOperationException($"rule '{ruleId}' needs one root criterion");

                    int antecedentIndex;/* = CompileCriterion(compiledEvent, criteria[0], compiledCriteria, buildingCriteria);*/

                    if(criteria.Count == 1)
                    {
                        antecedentIndex = CompileCriterion(compiledEvent, criteria[0], compiledCriteria, buildingCriteria);
                    }

                    else
                    {
                        antecedentIndex = CompileCriterionV2(compiledEvent, v2Criteria[0], compiledCriteriaV2, buildingCriteriaV2, compiledCriteria, buildingCriteria);
                    }

                        compiledEvent.rules.Add(new CompiledSugenoRule
                        {
                            ruleId = ruleId.Trim(),
                            antecedentExpressionIndex = antecedentIndex,
                            consequent = GetOptionValue(ruleNode, RuleNode.ConsequentOptionName, 0f)
                        });
                }

                List<ConsequenceNode> consequenceNodes = GetConnectedNodesFromOutput<ConsequenceNode>(eventNode, EventNode.ConsequencesPortName).ToList();
                HashSet<string> outcomeIds = new HashSet<string>(StringComparer.Ordinal);
                bool hasFallback = false;

                foreach (ConsequenceNode consequenceNode in consequenceNodes)
                {
                    string outcomeId = GetRequiredText(consequenceNode, ConsequenceNode.OutcomeIdOptionName, "outcome id");

                    if (!outcomeIds.Add(outcomeId)) throw new InvalidOperationException($"event '{eventId}' has duplicate outcome '{outcomeId}'");

                    bool isFallback = GetOptionValue(consequenceNode, ConsequenceNode.FallbackOptionName, false);
                    float minimum = GetOptionValue(consequenceNode, ConsequenceNode.MinimumOptionName, 0f);

                    if (!isFallback && (minimum < 0f || minimum > 1f)) throw new InvalidOperationException($"outcome '{outcomeId}' minimum must be 0 to 1");

                    CompiledOutputBand compiledBand = new CompiledOutputBand { outcomeId = outcomeId, minimumInclusive = minimum };
                    bool runAction = GetOptionValue(consequenceNode, ConsequenceNode.RunActionOptionName, true);

                    if (runAction)
                    {
                        compiledBand.consequences.Add(new RuntimeConsequence
                        {
                            consequenceType = ConsequenceType.FireEvent,
                            targetKey = GetOptionValue(consequenceNode, ConsequenceNode.TargetOptionName, string.Empty)?.Trim(),
                            payLoad = GetOptionValue(consequenceNode, ConsequenceNode.PayloadOptionName, string.Empty)?.Trim()
                        });
                    }

                    IEnumerable<WriteBackNode> writeBackNodes = GetConnectedNodesFromOutput<WriteBackNode>(consequenceNode, ConsequenceNode.WriteBacksPortName);
                    foreach (WriteBackNode writeBackNode in writeBackNodes)
                    {
                        compiledBand.writeBacks.Add(CompileWriteBack(writeBackNode, outcomeId));
                    }

                    if (isFallback)
                    {
                        if (hasFallback) throw new InvalidOperationException($"event '{eventId}' has multiple fallbacks");
                        compiledEvent.fallbackBand = compiledBand;
                        hasFallback = true;
                    }
                    else
                    {
                        compiledEvent.outputBands.Add(compiledBand);
                    }
                }

                events.Add(compiledEvent);
            }

            runtimeGraph.SetCompiledEvents(events);
            report = FuzzyGraph2CompileReport.From(runtimeGraph);
            return runtimeGraph;
        }

        private static IEnumerable<T> GetConnectedNodesFromOutput<T>(Node node, string portName) where T : Node
        {
            return GetConnectedNodes<T>(node.GetOutputPortByName(portName));
        }

        private static IEnumerable<T> GetConnectedNodes<T>(IPort port) where T : Node
        {
            if (port == null) return Enumerable.Empty<T>();
            List<IPort> connectedPorts = new List<IPort>();
            port.GetConnectedPorts(connectedPorts);
            return connectedPorts.Select(x => x.GetNode()).OfType<T>();
        }

        private static IEnumerable<T> GetConnectedNodesFromInput<T>(Node node, string portName) where T : Node
        {
            return GetConnectedNodes<T>(node.GetInputPortByName(portName));
        }

        private static int CompileCriterionV2(
            CompiledFuzzyEvent compiledEvent,
            CriterionNodeV2 node,
            IDictionary<CriterionNodeV2, int> compiledCriteria,
            ISet<CriterionNodeV2> buildingCriteria,
            IDictionary<CriterionNode, int> legacyCompiledCriteria,
            ISet<CriterionNode> legacyBuildingCriteria)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (compiledCriteria.TryGetValue(node, out int existingIndex))
                return existingIndex;

            if (!buildingCriteria.Add(node))
                throw new InvalidOperationException("criterion nodes contain a loop");

            try
            {
                CriterionMode mode = GetOptionValue(node,CriterionNodeV2.ModeOptionName,CriterionMode.BoolEquals);

                int expressionIndex;

                switch (mode)
                {

                    case CriterionMode.FuzzyNumber:
                        expressionIndex = CompileFuzzyNumberCriterionV2(
                            compiledEvent,
                            node);
                        break;

                    case CriterionMode.BoolEquals:
                        expressionIndex = AddExpression(
                            compiledEvent,
                            new CompiledFuzzyExpression
                            {
                                kind = CompiledExpressionKind.BoolEquals,

                                variableId = GetRequiredPortText(
                                    node,
                                    CriterionNodeV2.VariableIdPortName,
                                    "bool criterion variable id"),

                                expectedBool = GetPortValue(
                                    node,
                                    CriterionNodeV2.ExpectedBoolPortName,
                                    true)
                            });
                        break;

                    case CriterionMode.Exists:
                        expressionIndex = AddExpression(
                            compiledEvent,
                            new CompiledFuzzyExpression
                            {
                                kind = CompiledExpressionKind.Exists,

                                variableId = GetRequiredPortText(
                                    node,
                                    CriterionNodeV2.VariableIdPortName,
                                    "exists criterion variable id")
                            });
                        break;

                    case CriterionMode.DoesNotExist:
                        expressionIndex = AddExpression(
                            compiledEvent,
                            new CompiledFuzzyExpression
                            {
                                kind = CompiledExpressionKind.DoesNotExist,

                                variableId = GetRequiredPortText(
                                    node,
                                    CriterionNodeV2.VariableIdPortName,
                                    "does-not-exist criterion variable id")
                            });
                        break;

                    case CriterionMode.NumberCompare:
                        expressionIndex = AddExpression(
                            compiledEvent,
                            new CompiledFuzzyExpression
                            {
                                kind = CompiledExpressionKind.NumberCompare,

                                variableId = GetRequiredPortText(
                                    node,
                                    CriterionNodeV2.VariableIdPortName,
                                    "number criterion variable id"),

                                numberComparison = GetPortValue(
                                    node,
                                    CriterionNodeV2.ComparisonPortName,
                                    NumberComparison.LessThanOrEqual),

                                comparisonValue = GetPortValue(
                                    node,
                                    CriterionNodeV2.ComparisonValuePortName,
                                    0f),

                                comparisonValue2 = GetPortValue(
                                    node,
                                    CriterionNodeV2.ComparisonValue2PortName,
                                    0f)
                            });
                        break;


                    case CriterionMode.And:
                        expressionIndex = CompileBinaryCriterionV2(
                            compiledEvent,
                            node,
                            CompiledExpressionKind.And,
                            compiledCriteria,
                            buildingCriteria,
                            legacyCompiledCriteria,
                            legacyBuildingCriteria);
                        break;

                    case CriterionMode.Or:
                        expressionIndex = CompileBinaryCriterionV2(
                            compiledEvent,
                            node,
                            CompiledExpressionKind.Or,
                            compiledCriteria,
                            buildingCriteria,
                            legacyCompiledCriteria,
                            legacyBuildingCriteria);
                        break;

                    case CriterionMode.Not:
                        expressionIndex = CompileNotCriterionV2(
                            compiledEvent,
                            node,
                            compiledCriteria,
                            buildingCriteria,
                            legacyCompiledCriteria,
                            legacyBuildingCriteria);
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"CriterionNodeV2 mode '{mode}' is not supported yet");
                }

                compiledCriteria.Add(node, expressionIndex);
                return expressionIndex;
            }
            finally
            {
                buildingCriteria.Remove(node);
            }
        }

        private static int CompileBinaryCriterionV2(
            CompiledFuzzyEvent compiledEvent,
            CriterionNodeV2 node,
            CompiledExpressionKind kind,
            IDictionary<CriterionNodeV2, int> compiledCriteria,
            ISet<CriterionNodeV2> buildingCriteria,
            IDictionary<CriterionNode, int> legacyCompiledCriteria,
            ISet<CriterionNode> legacyBuildingCriteria)
        {
            int childAIndex = CompileCriterionInputV2(
                compiledEvent,
                node,
                CriterionNodeV2.CriteriaAPortName,
                compiledCriteria,
                buildingCriteria,
                legacyCompiledCriteria,
                legacyBuildingCriteria);

            int childBIndex = CompileCriterionInputV2(
                compiledEvent,
                node,
                CriterionNodeV2.CriteriaBPortName,
                compiledCriteria,
                buildingCriteria,
                legacyCompiledCriteria,
                legacyBuildingCriteria);

            CompiledFuzzyExpression expression =
                new CompiledFuzzyExpression
                {
                    kind = kind
                };

            expression.childExpressionIndices.Add(childAIndex);
            expression.childExpressionIndices.Add(childBIndex);

            return AddExpression(compiledEvent, expression);
        }

        private static int CompileNotCriterionV2(
            CompiledFuzzyEvent compiledEvent,
            CriterionNodeV2 node,
            IDictionary<CriterionNodeV2, int> compiledCriteria,
            ISet<CriterionNodeV2> buildingCriteria,
            IDictionary<CriterionNode, int> legacyCompiledCriteria,
            ISet<CriterionNode> legacyBuildingCriteria)
        {
            int childIndex = CompileCriterionInputV2(
                compiledEvent,
                node,
                CriterionNodeV2.CriteriaAPortName,
                compiledCriteria,
                buildingCriteria,
                legacyCompiledCriteria,
                legacyBuildingCriteria);

            CompiledFuzzyExpression expression =
                new CompiledFuzzyExpression
                {
                    kind = CompiledExpressionKind.Not
                };

            expression.childExpressionIndices.Add(childIndex);

            return AddExpression(compiledEvent, expression);
        }

        private static int CompileCriterionInputV2(
            CompiledFuzzyEvent compiledEvent,
            CriterionNodeV2 node,
            string portName,
            IDictionary<CriterionNodeV2, int> compiledCriteria,
            ISet<CriterionNodeV2> buildingCriteria,
            IDictionary<CriterionNode, int> legacyCompiledCriteria,
            ISet<CriterionNode> legacyBuildingCriteria)
        {
            List<CriterionNode> legacyChildren =
                GetConnectedNodesFromInput<CriterionNode>(
                    node,
                    portName).ToList();

            List<CriterionNodeV2> v2Children =
                GetConnectedNodesFromInput<CriterionNodeV2>(
                    node,
                    portName).ToList();

            if (legacyChildren.Count + v2Children.Count != 1)
            {
                throw new InvalidOperationException(
                    $"criterion '{node}' needs one link on '{portName}'");
            }

            if (legacyChildren.Count == 1)
            {
                return CompileCriterion(
                    compiledEvent,
                    legacyChildren[0],
                    legacyCompiledCriteria,
                    legacyBuildingCriteria);
            }

            return CompileCriterionV2(
                compiledEvent,
                v2Children[0],
                compiledCriteria,
                buildingCriteria,
                legacyCompiledCriteria,
                legacyBuildingCriteria);
        }

        private static int CompileCriterion(CompiledFuzzyEvent compiledEvent, CriterionNode node, IDictionary<CriterionNode, int> compiledCriteria, ISet<CriterionNode> buildingCriteria)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (compiledCriteria.TryGetValue(node, out int existingIndex)) return existingIndex;
            if (!buildingCriteria.Add(node)) throw new InvalidOperationException("criterion nodes contain a loop");

            try
            {
                CriterionMode mode = GetOptionValue(node, CriterionNode.ModeOptionName, CriterionMode.FuzzyNumber);
                int expressionIndex;

                switch (mode)
                {
                    case CriterionMode.FuzzyNumber:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = CompileFuzzyNumberCriterion(compiledEvent, node);
                        break;

                    case CriterionMode.BoolEquals:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = AddExpression(compiledEvent, new CompiledFuzzyExpression
                        {
                            kind = CompiledExpressionKind.BoolEquals,
                            variableId = GetRequiredText(node, CriterionNode.VariableIdOptionName, "bool criterion variable id"),
                            expectedBool = GetOptionValue(node, CriterionNode.ExpectedBoolOptionName, true)
                        });
                        break;

                    case CriterionMode.IdEquals:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = AddExpression(compiledEvent, new CompiledFuzzyExpression
                        {
                            kind = CompiledExpressionKind.IdEquals,
                            variableId = GetRequiredText(node, CriterionNode.VariableIdOptionName, "id criterion variable id"),
                            expectedId = GetRequiredText(node, CriterionNode.ExpectedIdOptionName, "expected id")
                        });
                        break;

                    case CriterionMode.NumberCompare:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = AddExpression(compiledEvent, new CompiledFuzzyExpression
                        {
                            kind = CompiledExpressionKind.NumberCompare,
                            variableId = GetRequiredText(node, CriterionNode.VariableIdOptionName, "number criterion variable id"),
                            numberComparison = GetOptionValue(node, CriterionNode.ComparisonOptionName, NumberComparison.LessThanOrEqual),
                            comparisonValue = GetOptionValue(node, CriterionNode.ComparisonValueOptionName, 0f),
                            comparisonValue2 = GetOptionValue(node, CriterionNode.ComparisonValue2OptionName, 0f)
                        });
                        break;

                    case CriterionMode.Exists:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = AddExpression(compiledEvent, new CompiledFuzzyExpression
                        {
                            kind = CompiledExpressionKind.Exists,
                            variableId = GetRequiredText(node, CriterionNode.VariableIdOptionName, "exists criterion variable id")
                        });
                        break;

                    case CriterionMode.DoesNotExist:
                        EnsureLeafCriterion(node, mode);
                        expressionIndex = AddExpression(compiledEvent, new CompiledFuzzyExpression
                        {
                            kind = CompiledExpressionKind.DoesNotExist,
                            variableId = GetRequiredText(node, CriterionNode.VariableIdOptionName, "does-not-exist criterion variable id")
                        });
                        break;

                    case CriterionMode.And:
                        expressionIndex = CompileBinaryCriterion(compiledEvent, node, CompiledExpressionKind.And, compiledCriteria, buildingCriteria);
                        break;

                    case CriterionMode.Or:
                        expressionIndex = CompileBinaryCriterion(compiledEvent, node, CompiledExpressionKind.Or, compiledCriteria, buildingCriteria);
                        break;

                    case CriterionMode.Not:
                        expressionIndex = CompileNotCriterion(compiledEvent, node, compiledCriteria, buildingCriteria);
                        break;

                    default:
                        throw new InvalidOperationException($"unsupported criterion mode: {mode}");
                }

                compiledCriteria.Add(node, expressionIndex);
                return expressionIndex;
            }
            finally
            {
                buildingCriteria.Remove(node);
            }
        }

        private static int CompileBinaryCriterion(CompiledFuzzyEvent compiledEvent, CriterionNode node, CompiledExpressionKind kind, IDictionary<CriterionNode, int> compiledCriteria, ISet<CriterionNode> buildingCriteria)
        {
            CriterionNode childA = GetRequiredCriterionInput(node, CriterionNode.CriteriaAPortName);
            CriterionNode childB = GetRequiredCriterionInput(node, CriterionNode.CriteriaBPortName);

            int childAIndex = CompileCriterion(compiledEvent, childA, compiledCriteria, buildingCriteria);
            int childBIndex = CompileCriterion(compiledEvent, childB, compiledCriteria, buildingCriteria);

            CompiledFuzzyExpression expression = new CompiledFuzzyExpression { kind = kind };
            expression.childExpressionIndices.Add(childAIndex);
            expression.childExpressionIndices.Add(childBIndex);

            return AddExpression(compiledEvent, expression);
        }

        private static int CompileNotCriterion(CompiledFuzzyEvent compiledEvent, CriterionNode node, IDictionary<CriterionNode, int> compiledCriteria, ISet<CriterionNode> buildingCriteria)
        {
            CriterionNode child = GetRequiredCriterionInput(node, CriterionNode.CriteriaAPortName);
            if (HasCriterionInput(node, CriterionNode.CriteriaBPortName)) throw new InvalidOperationException("not only uses criteria a");

            int childIndex = CompileCriterion(compiledEvent, child, compiledCriteria, buildingCriteria);

            CompiledFuzzyExpression expression = new CompiledFuzzyExpression { kind = CompiledExpressionKind.Not };
            expression.childExpressionIndices.Add(childIndex);

            return AddExpression(compiledEvent, expression);
        }

        private static CriterionNode GetRequiredCriterionInput(CriterionNode node, string portName)
        {
            List<CriterionNode> connected = GetConnectedNodesFromInput<CriterionNode>(node, portName).ToList();
            if (connected.Count != 1) throw new InvalidOperationException($"criterion '{node}' needs one link on '{portName}'");
            return connected[0];
        }

        private static bool HasCriterionInput(CriterionNode node, string portName)
        {
            return GetConnectedNodesFromInput<CriterionNode>(node, portName).Any();
        }

        private static void EnsureLeafCriterion(CriterionNode node, CriterionMode mode)
        {
            if (HasCriterionInput(node, CriterionNode.CriteriaAPortName) || HasCriterionInput(node, CriterionNode.CriteriaBPortName))
            {
                throw new InvalidOperationException($"{mode} cannot have child criteria");
            }
        }

        private static int AddExpression(CompiledFuzzyEvent compiledEvent, CompiledFuzzyExpression expression)
        {
            int expressionIndex = compiledEvent.expressions.Count;
            compiledEvent.expressions.Add(expression);
            return expressionIndex;
        }

        private static string GetRequiredText(Node node, string optionName, string label)
        {
            string value = GetOptionValue(node, optionName, string.Empty);
            if (string.IsNullOrWhiteSpace(value)) throw new InvalidOperationException($"{label} is missing");
            return value.Trim();
        }


        //v2 FN Compilation Function
        private static int CompileFuzzyNumberCriterionV2(CompiledFuzzyEvent compiledEvent,CriterionNodeV2 node)
        {
            string variableId = GetRequiredPortText(
                node,
                CriterionNodeV2.VariableIdPortName,
                "fuzzy criterion variable id");

            string setName = GetRequiredPortText(
                node,
                CriterionNodeV2.SetNamePortName,
                "fuzzy set name");

            float minimum = GetPortValue(
                node,
                CriterionNodeV2.MinimumPortName,
                0f);

            float maximum = GetPortValue(
                node,
                CriterionNodeV2.MaximumPortName,
                1f);

            FuzzySetShape shape = GetPortValue(
                node,
                CriterionNodeV2.ShapePortName,
                FuzzySetShape.Low);

            CompiledFuzzySet compiledSet = new CompiledFuzzySet
            {
                name = setName,
                shape = shape,

                first = GetPortValue(
                    node,
                    CriterionNodeV2.FirstPortName,
                    0f),

                second = GetPortValue(
                    node,
                    CriterionNodeV2.SecondPortName,
                    0f),

                third = GetPortValue(
                    node,
                    CriterionNodeV2.ThirdPortName,
                    0f),

                fourth = GetPortValue(
                    node,
                    CriterionNodeV2.FourthPortName,
                    0f)
            };

            ValidateFuzzyDefinition(
                variableId,
                minimum,
                maximum,
                compiledSet);

            CompiledFuzzyVariable variable =
                compiledEvent.variables.FirstOrDefault(
                    x => string.Equals(
                        x.id,
                        variableId,
                        StringComparison.Ordinal));

            if (variable == null)
            {
                variable = new CompiledFuzzyVariable
                {
                    id = variableId,
                    displayName = variableId,
                    minimum = minimum,
                    maximum = maximum
                };

                compiledEvent.variables.Add(variable);
            }
            else if (
                !Mathf.Approximately(variable.minimum, minimum) ||
                !Mathf.Approximately(variable.maximum, maximum))
            {
                throw new InvalidOperationException(
                    $"variable '{variableId}' has mixed domains");
            }

            CompiledFuzzySet existingSet =
                variable.sets.FirstOrDefault(
                    x => string.Equals(
                        x.name,
                        setName,
                        StringComparison.Ordinal));

            if (existingSet == null)
            {
                variable.sets.Add(compiledSet);
            }
            else if (!SameSet(existingSet, compiledSet))
            {
                throw new InvalidOperationException(
                    $"set '{variableId}.{setName}' has mixed values");
            }

            return AddExpression(
                compiledEvent,
                new CompiledFuzzyExpression
                {
                    kind = CompiledExpressionKind.FuzzyIs,
                    variableId = variableId,
                    setName = setName
                });
        }


        //legacy compilation function
        private static int CompileFuzzyNumberCriterion(CompiledFuzzyEvent compiledEvent, CriterionNode node)
        {
            string variableId = GetOptionValue(node, CriterionNode.VariableIdOptionName, string.Empty);
            string setName = GetOptionValue(node, CriterionNode.SetNameOptionName, string.Empty);

            if (string.IsNullOrWhiteSpace(variableId)) throw new InvalidOperationException("a fuzzy criterion has no variable id");
            if (string.IsNullOrWhiteSpace(setName)) throw new InvalidOperationException($"'{variableId}' has no fuzzy set name");

            variableId = variableId.Trim();
            setName = setName.Trim();

            float minimum = GetOptionValue(node, CriterionNode.MinimumOptionName, 0f);
            float maximum = GetOptionValue(node, CriterionNode.MaximumOptionName, 1f);
            FuzzySetShape shape = GetOptionValue(node, CriterionNode.ShapeOptionName, FuzzySetShape.Low);

            CompiledFuzzySet compiledSet = new CompiledFuzzySet
            {
                name = setName,
                shape = shape,
                first = GetOptionValue(node, CriterionNode.FirstOptionName, 0f),
                second = GetOptionValue(node, CriterionNode.SecondOptionName, 0f),
                third = GetOptionValue(node, CriterionNode.ThirdOptionName, 0f),
                fourth = GetOptionValue(node, CriterionNode.FourthOptionName, 0f)
            };

            ValidateFuzzyDefinition(variableId, minimum, maximum, compiledSet);

            CompiledFuzzyVariable variable = compiledEvent.variables.FirstOrDefault(x => string.Equals(x.id, variableId, StringComparison.Ordinal));

            if (variable == null)
            {
                variable = new CompiledFuzzyVariable
                {
                    id = variableId,
                    displayName = variableId,
                    minimum = minimum,
                    maximum = maximum
                };
                compiledEvent.variables.Add(variable);
            }
            else if (!Mathf.Approximately(variable.minimum, minimum) || !Mathf.Approximately(variable.maximum, maximum))
            {
                throw new InvalidOperationException($"variable '{variableId}' has mixed domains");
            }

            CompiledFuzzySet existingSet = variable.sets.FirstOrDefault(x => string.Equals(x.name, setName, StringComparison.Ordinal));

            if (existingSet == null)
            {
                variable.sets.Add(compiledSet);
            }
            else if (!SameSet(existingSet, compiledSet))
            {
                throw new InvalidOperationException($"set '{variableId}.{setName}' has mixed values");
            }

            int expressionIndex = compiledEvent.expressions.Count;
            compiledEvent.expressions.Add(new CompiledFuzzyExpression
            {
                kind = CompiledExpressionKind.FuzzyIs,
                variableId = variableId,
                setName = setName
            });

            return expressionIndex;
        }

        private static void ValidateFuzzyDefinition(string variableId, float minimum, float maximum, CompiledFuzzySet set)
        {
            FuzzySetDefinition definition;

            switch (set.shape)
            {
                case FuzzySetShape.Low:
                    definition = FuzzySetDefinition.Low(set.name, set.first, set.second);
                    break;
                case FuzzySetShape.Range:
                    definition = FuzzySetDefinition.Range(set.name, set.first, set.second, set.third, set.fourth);
                    break;
                case FuzzySetShape.High:
                    definition = FuzzySetDefinition.High(set.name, set.first, set.second);
                    break;
                default:
                    throw new InvalidOperationException($"unknown fuzzy shape '{set.shape}'");
            }

            // lets runtime validation catch dodgy bounds
            _ = new FuzzyVariableDefinition(variableId, variableId, minimum, maximum, new[] { definition });
        }

        private static bool SameSet(CompiledFuzzySet left, CompiledFuzzySet right)
        {
            return left.shape == right.shape
                && Mathf.Approximately(left.first, right.first)
                && Mathf.Approximately(left.second, right.second)
                && Mathf.Approximately(left.third, right.third)
                && Mathf.Approximately(left.fourth, right.fourth);
        }

        private static RuntimeWriteBack CompileWriteBack(WriteBackNode node, string outcomeId)
        {
            string targetKey = GetRequiredText(node, WriteBackNode.TargetKeyOptionName, $"write-back target for '{outcomeId}'");
            WriteBackOperation operation = GetOptionValue(node, WriteBackNode.OperationOptionName, WriteBackOperation.Set);
            FuzzyValueType valueType = GetOptionValue(node, WriteBackNode.ValueTypeOptionName, FuzzyValueType.Bool);

            // toggle ignores the stored value
            if (operation == WriteBackOperation.Toggle)
            {
                return new RuntimeWriteBack
                {
                    targetKey = targetKey,
                    operation = operation,
                    val = FuzzyValue.FromBool(false)
                };
            }

            if ((operation == WriteBackOperation.Add || operation == WriteBackOperation.Subtract)
                && valueType != FuzzyValueType.Int && valueType != FuzzyValueType.Float)
            {
                throw new InvalidOperationException($"write-back '{targetKey}' uses {operation} but its value is {valueType}");
            }

            return new RuntimeWriteBack
            {
                targetKey = targetKey,
                operation = operation,
                val = BuildWriteBackValue(node, valueType)
            };
        }

        private static FuzzyValue BuildWriteBackValue(WriteBackNode node, FuzzyValueType valueType)
        {
            switch (valueType)
            {
                case FuzzyValueType.Bool:
                    return FuzzyValue.FromBool(GetOptionValue(node, WriteBackNode.BoolValueOptionName, false));
                case FuzzyValueType.Int:
                    return FuzzyValue.FromInt(GetOptionValue(node, WriteBackNode.IntValueOptionName, 0));
                case FuzzyValueType.Float:
                    return FuzzyValue.FromFloat(GetOptionValue(node, WriteBackNode.FloatValueOptionName, 0f));
                case FuzzyValueType.String:
                    return FuzzyValue.FromString(GetOptionValue(node, WriteBackNode.StringValueOptionName, string.Empty));
                default:
                    throw new InvalidOperationException($"unsupported write-back value type: {valueType}");
            }
        }

        private static T GetPortValue<T>(Node node,string portName,T fallback)
        {
            IPort port = node?.GetInputPortByName(portName);

            if (port != null && port.TryGetValue(out T value))
                return value;

            return fallback;
        }


        //explicitly for v2CriteriaNode
        private static string GetRequiredPortText(Node node,string portName,string label)
        {
            string value = GetPortValue(node,portName,string.Empty);

            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"{label} is missing");

            return value.Trim();
        }

        private static T GetOptionValue<T>(Node node, string optionName, T fallback)
        {
            INodeOption option = node?.GetNodeOptionByName(optionName);
            if (option != null && option.TryGetValue(out T value)) return value;
            return fallback;
        }
    }
}