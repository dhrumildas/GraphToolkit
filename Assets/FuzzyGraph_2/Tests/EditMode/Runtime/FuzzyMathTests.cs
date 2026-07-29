using NUnit.Framework;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzyMathTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void ClampMembership_ValueBelowZero_ReturnsZero()
        {
            float result = FuzzyMath.Clamp(-0.5f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void ClampMembership_ValueAboveOne_ReturnsOne()
        {
            float result = FuzzyMath.Clamp(1.5f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void ClampMembership_ValueInsideRange_RemainsUnchanged()
        {
            float result = FuzzyMath.Clamp(0.65f);

            Assert.AreEqual(0.65f, result, Tolerance);
        }

        [Test]
        public void And_MultipliesMembershipValues()
        {
            float result = FuzzyMath.AND(0.7f, 0.6f);

            Assert.AreEqual(0.42f, result, Tolerance);
        }

        [Test]
        public void And_WhenOneValueIsZero_ReturnsZero()
        {
            float result = FuzzyMath.AND(0.8f, 0f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void Or_UsesProbabilisticSum()
        {
            float result = FuzzyMath.OR(0.7f, 0.6f);

            Assert.AreEqual(0.88f, result, Tolerance);
        }

        [Test]
        public void Or_WhenOneValueIsOne_ReturnsOne()
        {
            float result = FuzzyMath.OR(0.4f, 1f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void Not_InvertsMembership()
        {
            float result = FuzzyMath.NOT(0.7f);

            Assert.AreEqual(0.3f, result, Tolerance);
        }

        [Test]
        public void Smooth_AtZero_ReturnsZero()
        {
            float result = FuzzyMath.Smooth(0f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void Smooth_AtHalf_ReturnsHalf()
        {
            float result = FuzzyMath.Smooth(0.5f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Smooth_AtOne_ReturnsOne()
        {
            float result = FuzzyMath.Smooth(1f);

            Assert.AreEqual(1f, result, Tolerance);
        }
    }
}