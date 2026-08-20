using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;
using NUnit.Framework;
using System;

namespace FuzzyGraph2.Tests.EditMode
{
    public class SugenoNarrativeResolverTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void Resolve_VendorScenario_SelectsRevealAndWritesPitRoute()
        {
            FuzzyVariableDefinition proximity =
                new FuzzyVariableDefinition(
                    id: "Vendor.CarpetProximity",
                    displayName: "Carpet Proximity",
                    minimum: 0f,
                    maximum: 1f,
                    sets: new[]
                    {
                        FuzzySetDefinition.Low(
                            "Low",
                            full: 0f,
                            end: 0.5f),

                        FuzzySetDefinition.Range(
                            "Medium",
                            start: 0.2f,
                            fullStart: 0.4f,
                            fullEnd: 0.6f,
                            end: 0.8f),

                        FuzzySetDefinition.High(
                            "High",
                            start: 0.4f,
                            full: 0.8f)
                    });

            FuzzyVariableDefinition perspiration =
                new FuzzyVariableDefinition(
                    id: "Vendor.Perspiration",
                    displayName: "Vendor Perspiration",
                    minimum: 0f,
                    maximum: 1f,
                    sets: new[]
                    {
                        FuzzySetDefinition.High(
                            "High",
                            start: 0.25f,
                            full: 0.75f)
                    });

            FuzzyVariableDefinition blindness =
                new FuzzyVariableDefinition(
                    id: "Vendor.BlindnessEvidence",
                    displayName: "Blindness Evidence",
                    minimum: 0f,
                    maximum: 1f,
                    sets: new[]
                    {
                        FuzzySetDefinition.Low(
                            "Low",
                            full: 0f,
                            end: 0.9f),

                        FuzzySetDefinition.High(
                            "High",
                            start: 0.4f,
                            full: 0.8f)
                    });

            SugenoRule revealRule =
                new SugenoRule(
                    "Vendor.RevealSecret",
                    FuzzyExpression.And(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("High")),

                        new FuzzyIsStatement(
                            perspiration,
                            perspiration.GetSet("High")),

                        new FuzzyIsStatement(
                            blindness,
                            blindness.GetSet("High"))),
                    consequent: 90f);

            SugenoRule nervousRule =
                new SugenoRule(
                    "Vendor.BecomesNervous",
                    FuzzyExpression.And(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("Medium")),

                        new FuzzyIsStatement(
                            perspiration,
                            perspiration.GetSet("High"))),
                    consequent: 60f);

            SugenoRule concealRule =
                new SugenoRule(
                    "Vendor.ConcealsSecret",
                    FuzzyExpression.Or(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("Low")),

                        new FuzzyIsStatement(
                            blindness,
                            blindness.GetSet("Low"))),
                    consequent: 15f);

            RuntimeWriteBack pitRouteWriteBack =
                new RuntimeWriteBack
                {
                    targetKey = "Player.FoundPitRoute",
                    operation = WriteBackOperation.Set,
                    val = FuzzyValue.FromBool(true)
                };

            RuntimeConsequence consequence =
                new RuntimeConsequence
                {
                    consequenceType = ConsequenceType.FireEvent,
                    targetKey = "DialogueGraph",
                    payLoad = "VendorSecretRevealed"
                };

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                        new NarrativeOutputMapping.Band(
                            0f,
                            "Vendor.ConcealsSecret"),

                        new NarrativeOutputMapping.Band(
                            40f,
                            "Vendor.BecomesNervous"),

                        new NarrativeOutputMapping.Band(
                            70f,
                            "Vendor.RevealsSecret",
                            consequences: new[] { consequence },
                            writeBacks: new[] { pitRouteWriteBack })
                    });

            WorldStateQuery query =
                new WorldStateQuery();

            query.Set(
                "Vendor.CarpetProximity",
                FuzzyValue.FromFloat(0.7f));

            query.Set(
                "Vendor.Perspiration",
                FuzzyValue.FromFloat(0.585f));

            query.Set(
                "Vendor.BlindnessEvidence",
                FuzzyValue.FromFloat(0.81f));

            SugenoNarrativeResult result =
                SugenoNarrativeResolver.Resolve(
                    new[]
                    {
                        revealRule,
                        nervousRule,
                        concealRule
                    },
                    mapping,
                    query);

            Assert.AreEqual(
                71.28f,
                result.Inference.Output,
                Tolerance);

            Assert.AreEqual(
                "Vendor.RevealsSecret",
                result.OutcomeId);

            Assert.AreEqual(1, result.Consequences.Count);
            Assert.AreEqual(1, result.WriteBacks.Count);

            PersistentContext context =
                new PersistentContext();

            WriteBackProcessor.ApplyAll(
                context,
                result.WriteBacks);

            Assert.IsTrue(
                context.TryGet(
                    "Player.FoundPitRoute",
                    out FuzzyValue storedValue));

            Assert.IsTrue(storedValue.boolVal);
        }

        private sealed class ConstantExpression : IFuzzyExpression
        {
            private readonly float _value;

            public ConstantExpression(float value)
            {
                _value = value;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                return _value;
            }
        }

        [Test]
        public void Resolve_NoActiveRule_UsesAuthoredFallback()
        {
            SugenoRule inactiveRule =
                new SugenoRule(
                    id: "Inactive",
                    antecedent: new ConstantExpression(0f),
                    consequent: 90f);

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    bands: new[]
                    {
                new NarrativeOutputMapping.Band(
                    0f,
                    "NormalOutcome")
                    },
                    fallbackBand:
                        new NarrativeOutputMapping.Band(
                            0f,
                            "FallbackOutcome"));

            SugenoNarrativeResult result =
                SugenoNarrativeResolver.Resolve(
                    new[] { inactiveRule },
                    mapping,
                    new WorldStateQuery());

            Assert.IsTrue(result.UsedFallback);
            Assert.IsFalse(result.Inference.HasOutput);

            Assert.AreEqual(
                "FallbackOutcome",
                result.OutcomeId);
        }

        [Test]
        public void Resolve_MissingValue_UsesFallbackAndRecordsDiagnostic()
        {
            SugenoRule missingValueRule =
                new SugenoRule(
                    id: "RequiresCharmed",
                    antecedent:
                        FuzzyExpression.BoolEquals(
                            "Guard.Charmed",
                            true),
                    consequent: 90f);

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    bands: new[]
                    {
                new NarrativeOutputMapping.Band(
                    0f,
                    "NormalOutcome")
                    },
                    fallbackBand:
                        new NarrativeOutputMapping.Band(
                            0f,
                            "MissingStateFallback"));

            SugenoNarrativeResult result =
                SugenoNarrativeResolver.Resolve(
                    new[] { missingValueRule },
                    mapping,
                    new WorldStateQuery());

            Assert.IsTrue(result.UsedFallback);

            Assert.AreEqual(
                "MissingStateFallback",
                result.OutcomeId);

            Assert.AreEqual(
                1,
                result.Inference.Diagnostics.Count);

            Assert.AreSame(
                missingValueRule,
                result.Inference.Diagnostics[0].Rule);

            StringAssert.Contains(
                "Guard.Charmed",
                result.Inference.Diagnostics[0].Message);
        }

        [Test]
        public void Resolve_MissingRuleValue_DoesNotBlockValidRule()
        {
            SugenoRule missingRule =
                new SugenoRule(
                    id: "MissingRule",
                    antecedent:
                        FuzzyExpression.BoolEquals(
                            "Missing.Flag",
                            true),
                    consequent: 90f);

            SugenoRule validRule =
                new SugenoRule(
                    id: "ValidRule",
                    antecedent:
                        new ConstantExpression(1f),
                    consequent: 20f);

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    bands: new[]
                    {
                new NarrativeOutputMapping.Band(
                    0f,
                    "LowOutcome"),

                new NarrativeOutputMapping.Band(
                    50f,
                    "HighOutcome")
                    },
                    fallbackBand:
                        new NarrativeOutputMapping.Band(
                            0f,
                            "FallbackOutcome"));

            SugenoNarrativeResult result =
                SugenoNarrativeResolver.Resolve(
                    new[]
                    {
                missingRule,
                validRule
                    },
                    mapping,
                    new WorldStateQuery());

            Assert.IsFalse(result.UsedFallback);
            Assert.IsTrue(result.Inference.HasOutput);

            Assert.AreEqual(
                20f,
                result.Inference.Output,
                0.0001f);

            Assert.AreEqual(
                "LowOutcome",
                result.OutcomeId);

            Assert.AreEqual(
                1,
                result.Inference.Diagnostics.Count);
        }

        [Test]
        public void Resolve_NoRuleAndNoFallback_ThrowsConfigurationError()
        {
            SugenoRule inactiveRule =
                new SugenoRule(
                    id: "Inactive",
                    antecedent: new ConstantExpression(0f),
                    consequent: 90f);

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                new NarrativeOutputMapping.Band(
                    0f,
                    "NormalOutcome")
                    });

            InvalidOperationException exception =
                Assert.Throws<InvalidOperationException>(() =>
                    SugenoNarrativeResolver.Resolve(
                        new[] { inactiveRule },
                        mapping,
                        new WorldStateQuery()));

            StringAssert.Contains(
                "fallback",
                exception.Message.ToLowerInvariant());
        }
    }
}