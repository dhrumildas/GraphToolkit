using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;
using FuzzyGraph.Runtime;
namespace FuzzyGraph2.Tests.EditMode
{
    public class NarrativeOutputMappingTests
    {
        private static NarrativeOutputMapping
            CreateVendorMapping()
        {
            return new NarrativeOutputMapping(
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
                        "Vendor.RevealsSecret")
                });
        }

        [Test]
        public void Map_VendorSugenoOutput_ReturnsRevealOutcome()
        {
            NarrativeOutputMapping mapping =
                CreateVendorMapping();

            string result = mapping.Map(71.28f);

            Assert.AreEqual(
                "Vendor.RevealsSecret",
                result);
        }

        [TestCase(0f, "Vendor.ConcealsSecret")]
        [TestCase(39.999f, "Vendor.ConcealsSecret")]
        [TestCase(40f, "Vendor.BecomesNervous")]
        [TestCase(69.999f, "Vendor.BecomesNervous")]
        [TestCase(70f, "Vendor.RevealsSecret")]
        public void Map_UsesInclusiveMinimumThresholds(
            float output,
            string expected)
        {
            NarrativeOutputMapping mapping =
                CreateVendorMapping();

            string result = mapping.Map(output);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Constructor_SortsBandsByThreshold()
        {
            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                        new NarrativeOutputMapping.Band(
                            70f,
                            "High"),

                        new NarrativeOutputMapping.Band(
                            0f,
                            "Low"),

                        new NarrativeOutputMapping.Band(
                            40f,
                            "Medium")
                    });

            Assert.AreEqual(
                "Low",
                mapping.Map(20f));

            Assert.AreEqual(
                "Medium",
                mapping.Map(50f));

            Assert.AreEqual(
                "High",
                mapping.Map(80f));
        }

        [Test]
        public void Constructor_DuplicateThreshold_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new NarrativeOutputMapping(
                    new[]
                    {
                        new NarrativeOutputMapping.Band(
                            0f,
                            "First"),

                        new NarrativeOutputMapping.Band(
                            0f,
                            "Second")
                    }));
        }

        [Test]
        public void Map_OutputBelowFirstThreshold_ThrowsException()
        {
            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                        new NarrativeOutputMapping.Band(
                            10f,
                            "Available")
                    });

            Assert.Throws<InvalidOperationException>(() =>
                mapping.Map(5f));
        }

        [TestCase(float.NaN)]
        [TestCase(float.PositiveInfinity)]
        public void Map_InvalidOutput_ThrowsException(
            float invalidOutput)
        {
            NarrativeOutputMapping mapping =
                CreateVendorMapping();

            Assert.Throws<ArgumentException>(() =>
                mapping.Map(invalidOutput));
        }

        [Test]
        public void Band_InvalidOutcomeId_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new NarrativeOutputMapping.Band(
                    0f,
                    "   "));
        }

        [Test]
        public void MapBand_ReturnsVendorRevealEffects()
        {
            RuntimeConsequence dialogueConsequence =
                new RuntimeConsequence
                {
                    consequenceType = ConsequenceType.FireEvent,
                    targetKey = "DialogueGraph",
                    payLoad = "VendorSecretRevealed"
                };

            RuntimeWriteBack pitRouteWriteBack =
                new RuntimeWriteBack
                {
                    targetKey = "Player.FoundPitRoute",
                    operation = WriteBackOperation.Set,
                    val = FuzzyValue.FromBool(true)
                };

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                new NarrativeOutputMapping.Band(
                    minimumInclusive: 0f,
                    outcomeId: "Vendor.ConcealsSecret"),

                new NarrativeOutputMapping.Band(
                    minimumInclusive: 40f,
                    outcomeId: "Vendor.BecomesNervous"),

                new NarrativeOutputMapping.Band(
                    minimumInclusive: 70f,
                    outcomeId: "Vendor.RevealsSecret",
                    consequences: new[]
                    {
                        dialogueConsequence
                    },
                    writeBacks: new[]
                    {
                        pitRouteWriteBack
                    })
                    });

            NarrativeOutputMapping.Band selectedBand =
                mapping.MapBand(71.28f);

            Assert.AreEqual(
                "Vendor.RevealsSecret",
                selectedBand.OutcomeId);

            Assert.AreEqual(
                1,
                selectedBand.Consequences.Count);

            Assert.AreEqual(
                1,
                selectedBand.WriteBacks.Count);

            Assert.AreSame(
                dialogueConsequence,
                selectedBand.Consequences[0]);

            Assert.AreSame(
                pitRouteWriteBack,
                selectedBand.WriteBacks[0]);
        }

        [Test]
        public void SelectedBand_WriteBacksUseExistingProcessor()
        {
            RuntimeWriteBack pitRouteWriteBack =
                new RuntimeWriteBack
                {
                    targetKey = "Player.FoundPitRoute",
                    operation = WriteBackOperation.Set,
                    val = FuzzyValue.FromBool(true)
                };

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    new[]
                    {
                new NarrativeOutputMapping.Band(
                    minimumInclusive: 0f,
                    outcomeId: "Vendor.ConcealsSecret"),

                new NarrativeOutputMapping.Band(
                    minimumInclusive: 70f,
                    outcomeId: "Vendor.RevealsSecret",
                    writeBacks: new[]
                    {
                        pitRouteWriteBack
                    })
                    });

            NarrativeOutputMapping.Band selectedBand =
                mapping.MapBand(71.28f);

            PersistentContext context =
                new PersistentContext();

            int appliedCount =
                WriteBackProcessor.ApplyAll(
                    context,
                    selectedBand.WriteBacks);

            Assert.AreEqual(1, appliedCount);

            bool found =
                context.TryGet(
                    "Player.FoundPitRoute",
                    out FuzzyValue storedValue);

            Assert.IsTrue(found);
            Assert.AreEqual(FuzzyValueType.Bool, storedValue.type);
            Assert.IsTrue(storedValue.boolVal);
        }

        [Test]
        public void Band_NullConsequence_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new NarrativeOutputMapping.Band(
                    minimumInclusive: 0f,
                    outcomeId: "Test",
                    consequences: new RuntimeConsequence[]
                    {
                null
                    }));
        }

        [Test]
        public void Band_NullWriteBack_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new NarrativeOutputMapping.Band(
                    minimumInclusive: 0f,
                    outcomeId: "Test",
                    writeBacks: new RuntimeWriteBack[]
                    {
                null
                    }));
        }

        [Test]
        public void GetFallbackBand_ReturnsAuthoredFallback()
        {
            NarrativeOutputMapping.Band fallback =
                new NarrativeOutputMapping.Band(
                    minimumInclusive: 0f,
                    outcomeId: "Vendor.DefaultResponse");

            NarrativeOutputMapping mapping =
                new NarrativeOutputMapping(
                    bands: new[]
                    {
                new NarrativeOutputMapping.Band(
                    0f,
                    "Vendor.Normal")
                    },
                    fallbackBand: fallback);

            Assert.IsTrue(mapping.HasFallback);
            Assert.AreSame(
                fallback,
                mapping.GetFallbackBand());
        }
    }
}