using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzySetDefinitionTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void HighSet_Evaluate_ReturnsExpectedMembership()
        {
            FuzzySetDefinition friendly =
                FuzzySetDefinition.High(
                    name: "Friendly",
                    start: 35f,
                    full: 85f);

            float result = friendly.Evaluate(70f);

            Assert.AreEqual(0.7f, result, Tolerance);
        }

        [Test]
        public void LowSet_Evaluate_ReturnsExpectedMembership()
        {
            FuzzySetDefinition hostile =
                FuzzySetDefinition.Low(
                    name: "Hostile",
                    full: 20f,
                    end: 80f);

            float result = hostile.Evaluate(32f);

            Assert.AreEqual(0.8f, result, Tolerance);
        }

        [Test]
        public void RangeSet_Evaluate_ReturnsExpectedMembership()
        {
            FuzzySetDefinition neutral =
                FuzzySetDefinition.Range(
                    name: "Neutral",
                    start: 20f,
                    fullStart: 40f,
                    fullEnd: 60f,
                    end: 80f);

            float result = neutral.Evaluate(70f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Definition_PreservesReadableNameAndShape()
        {
            FuzzySetDefinition friendly =
                FuzzySetDefinition.High(
                    name: "Friendly",
                    start: 35f,
                    full: 85f);

            Assert.AreEqual("Friendly", friendly.Name);
            Assert.AreEqual(FuzzySetShape.High, friendly.Shape);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Definition_InvalidName_ThrowsArgumentException(
            string invalidName)
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzySetDefinition.High(
                    name: invalidName,
                    start: 35f,
                    full: 85f));
        }

        [Test]
        public void HighSet_InvalidBounds_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzySetDefinition.High(
                    name: "Friendly",
                    start: 85f,
                    full: 35f));
        }

        [Test]
        public void LowSet_InvalidBounds_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzySetDefinition.Low(
                    name: "Hostile",
                    full: 80f,
                    end: 20f));
        }

        [Test]
        public void RangeSet_InvalidBounds_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzySetDefinition.Range(
                    name: "Neutral",
                    start: 20f,
                    fullStart: 60f,
                    fullEnd: 40f,
                    end: 80f));
        }
    }
}