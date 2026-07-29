using NUnit.Framework;
using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;

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
    }
}