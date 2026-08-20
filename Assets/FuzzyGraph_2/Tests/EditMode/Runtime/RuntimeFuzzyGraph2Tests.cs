using System;
using NUnit.Framework;
using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;
using UnityEngine;

namespace FuzzyGraph2.Tests.EditMode
{
    public class RuntimeFuzzyGraph2Tests
    {
        private RuntimeFuzzyGraph2 _graph;

        private static CompiledFuzzyEvent CreateCompoundVendorEvent()
        {
            CompiledFuzzyEvent compiledEvent = CreateVendorEvent();

            compiledEvent.expressions.Add(
                new CompiledFuzzyExpression
                {
                    kind = CompiledExpressionKind.BoolEquals,
                    variableId = "Guard.Charmed",
                    expectedBool = true
                }
            );

            CompiledFuzzyExpression andExpression = new CompiledFuzzyExpression
            {
                kind = CompiledExpressionKind.And
            };

            andExpression.childExpressionIndices.Add(0);
            andExpression.childExpressionIndices.Add(1);

            compiledEvent.expressions.Add(andExpression);

            compiledEvent.rules[0].antecedentExpressionIndex = 2;

            return compiledEvent;
        }

        [SetUp]
        public void SetUp()
        {
            _graph = ScriptableObject.CreateInstance<RuntimeFuzzyGraph2>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_graph != null)
            {
                UnityEngine.Object.DestroyImmediate(_graph);
            }
        }

        private static CompiledFuzzyEvent CreateVendorEvent()
        {
            CompiledFuzzyVariable proximity = new CompiledFuzzyVariable
            {
                id = "Player.DistanceToCarpet",
                displayName = "Distance To Carpet",
                minimum = 0f,
                maximum = 1f
            };

            proximity.sets.Add(
                new CompiledFuzzySet
                {
                    name = "Near",
                    shape = FuzzySetShape.High,
                    first = 0.4f,
                    second = 0.8f
                }
            );

            CompiledFuzzyExpression nearExpression = new CompiledFuzzyExpression
            {
                kind = CompiledExpressionKind.FuzzyIs,
                variableId = "Player.DistanceToCarpet",
                setName = "Near"
            };

            CompiledSugenoRule revealRule = new CompiledSugenoRule
            {
                ruleId = "Vendor.RevealSecret",
                antecedentExpressionIndex = 0,
                consequent = 90f
            };

            CompiledOutputBand revealBand = new CompiledOutputBand
            {
                minimumInclusive = 70f,
                outcomeId = "Vendor.RevealsSecret"
            };

            revealBand.consequences.Add(
                new RuntimeConsequence
                {
                    consequenceType = ConsequenceType.FireEvent,
                    targetKey = "DialogueGraph",
                    payLoad = "VendorSecretRevealed"
                }
            );

            revealBand.writeBacks.Add(
                new RuntimeWriteBack
                {
                    targetKey = "Player.FoundPitRoute",
                    operation = WriteBackOperation.Set,
                    val = FuzzyValue.FromBool(true)
                }
            );

            return new CompiledFuzzyEvent
            {
                eventId = "InspectBazaarCarpet",
                variables = new System.Collections.Generic.List<CompiledFuzzyVariable>
                {
                    proximity
                },
                expressions = new System.Collections.Generic.List<CompiledFuzzyExpression>
                {
                    nearExpression
                },
                rules = new System.Collections.Generic.List<CompiledSugenoRule> { revealRule },
                outputBands = new System.Collections.Generic.List<CompiledOutputBand>
                {
                    revealBand
                },
                fallbackBand = new CompiledOutputBand
                {
                    minimumInclusive = 0f,
                    outcomeId = "Vendor.DefaultResponse"
                }
            };
        }

        [Test]
        public void Graph_StoresEventGroupedCompiledData()
        {
            CompiledFuzzyEvent vendorEvent = CreateVendorEvent();

            _graph.SetCompiledEvents(new[] { vendorEvent });

            Assert.AreEqual(1, _graph.Events.Count);

            CompiledFuzzyEvent storedEvent = _graph.Events[0];

            Assert.AreEqual("InspectBazaarCarpet", storedEvent.eventId);

            Assert.AreEqual(1, storedEvent.variables.Count);
            Assert.AreEqual(1, storedEvent.expressions.Count);
            Assert.AreEqual(1, storedEvent.rules.Count);
            Assert.AreEqual(1, storedEvent.outputBands.Count);

            Assert.AreEqual("Vendor.DefaultResponse", storedEvent.fallbackBand.outcomeId);
        }

        [Test]
        public void GetEvent_ReturnsMatchingEvent()
        {
            CompiledFuzzyEvent vendorEvent = CreateVendorEvent();

            _graph.SetCompiledEvents(new[] { vendorEvent });

            CompiledFuzzyEvent result = _graph.GetEvent("InspectBazaarCarpet");

            Assert.AreSame(vendorEvent, result);
        }

        [Test]
        public void TryGetEvent_IsCaseSensitive()
        {
            _graph.SetCompiledEvents(new[] { CreateVendorEvent() });

            bool found = _graph.TryGetEvent("inspectbazaarcarpet", out _);

            Assert.IsFalse(found);
        }

        [Test]
        public void SetCompiledEvents_DuplicateIds_ThrowsException()
        {
            CompiledFuzzyEvent first = CreateVendorEvent();

            CompiledFuzzyEvent second = CreateVendorEvent();

            Assert.Throws<ArgumentException>(
                () => _graph.SetCompiledEvents(new[] { first, second })
            );
        }

        [Test]
        public void SetCompiledEvents_NullEvent_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () => _graph.SetCompiledEvents(new CompiledFuzzyEvent[] { null })
            );
        }

        [Test]
        public void JsonRoundTrip_PreservesCompiledRecords()
        {
            _graph.SetCompiledEvents(new[] { CreateVendorEvent() });
            string json = JsonUtility.ToJson(_graph);

            RuntimeFuzzyGraph2 restored = ScriptableObject.CreateInstance<RuntimeFuzzyGraph2>();

            try
            {
                // Restore the compiled event data before attempting resolution.
                JsonUtility.FromJsonOverwrite(json, restored);

                WorldStateQuery query = new WorldStateQuery();
                query.Set("Player.DistanceToCarpet", FuzzyValue.FromFloat(0.7f));

                SugenoNarrativeResult result = restored.Resolve("InspectBazaarCarpet", query);
                Assert.AreEqual("Vendor.RevealsSecret", result.OutcomeId);

                CompiledFuzzyEvent restoredEvent = restored.GetEvent("InspectBazaarCarpet");

                Assert.AreEqual("Player.DistanceToCarpet", restoredEvent.variables[0].id);
                Assert.AreEqual("Near", restoredEvent.variables[0].sets[0].name);
                Assert.AreEqual(CompiledExpressionKind.FuzzyIs, restoredEvent.expressions[0].kind);
                Assert.AreEqual(90f, restoredEvent.rules[0].consequent, 0.0001f);
                Assert.AreEqual("Player.FoundPitRoute", restoredEvent.outputBands[0].writeBacks[0].targetKey);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(restored);
            }
        }

        [Test]
        public void Resolve_CompiledVendorEvent_ReturnsExpectedOutcome()
        {
            _graph.SetCompiledEvents(new[] { CreateVendorEvent() });

            WorldStateQuery query = new WorldStateQuery();

            query.Set("Player.DistanceToCarpet", FuzzyValue.FromFloat(0.7f));

            SugenoNarrativeResult result = _graph.Resolve("InspectBazaarCarpet", query);

            Assert.IsFalse(result.UsedFallback);

            // Near membership is 0.75, but with one active rule
            // the weighted-average output remains its consequent: 90.
            Assert.AreEqual(90f, result.Inference.Output, 0.0001f);

            Assert.AreEqual("Vendor.RevealsSecret", result.OutcomeId);

            Assert.AreEqual(1, result.Consequences.Count);
            Assert.AreEqual(1, result.WriteBacks.Count);
        }

        [Test]
        public void Resolve_MissingValue_UsesFallbackAndRecordsDiagnostic()
        {
            _graph.SetCompiledEvents(new[] { CreateVendorEvent() });

            SugenoNarrativeResult result = _graph.Resolve(
                "InspectBazaarCarpet",
                new WorldStateQuery()
            );

            Assert.IsTrue(result.UsedFallback);

            Assert.AreEqual("Vendor.DefaultResponse", result.OutcomeId);

            Assert.AreEqual(1, result.Inference.Diagnostics.Count);

            StringAssert.Contains(
                "Player.DistanceToCarpet",
                result.Inference.Diagnostics[0].Message
            );
        }

        [Test]
        public void Resolve_CompiledAndExpression_UsesBooleanAndFuzzyInputs()
        {
            _graph.SetCompiledEvents(new[] { CreateCompoundVendorEvent() });

            WorldStateQuery matchingQuery = new WorldStateQuery();

            matchingQuery.Set("Player.DistanceToCarpet", FuzzyValue.FromFloat(0.7f));

            matchingQuery.Set("Guard.Charmed", FuzzyValue.FromBool(true));

            SugenoNarrativeResult matchingResult = _graph.Resolve(
                "InspectBazaarCarpet",
                matchingQuery
            );

            Assert.IsFalse(matchingResult.UsedFallback);

            Assert.AreEqual("Vendor.RevealsSecret", matchingResult.OutcomeId);

            WorldStateQuery failingQuery = new WorldStateQuery();

            failingQuery.Set("Player.DistanceToCarpet", FuzzyValue.FromFloat(0.7f));

            failingQuery.Set("Guard.Charmed", FuzzyValue.FromBool(false));

            SugenoNarrativeResult failingResult = _graph.Resolve(
                "InspectBazaarCarpet",
                failingQuery
            );

            Assert.IsTrue(failingResult.UsedFallback);

            Assert.AreEqual("Vendor.DefaultResponse", failingResult.OutcomeId);
        }

        [Test]
        public void Resolve_InvalidRuleExpressionIndex_ThrowsClearError()
        {
            CompiledFuzzyEvent compiledEvent = CreateVendorEvent();

            compiledEvent.rules[0].antecedentExpressionIndex = 99;

            _graph.SetCompiledEvents(new[] { compiledEvent });

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _graph.Resolve("InspectBazaarCarpet", new WorldStateQuery())
            );

            StringAssert.Contains("expression index", exception.Message.ToLowerInvariant());
        }
    }
}
