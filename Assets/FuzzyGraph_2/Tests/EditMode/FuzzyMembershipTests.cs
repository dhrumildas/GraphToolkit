using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzyMembershipTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void High_ValueBelowStart_ReturnsZero()
        {
            float result = FuzzyMembership.High(
                val: 20f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void High_ValueAtStart_ReturnsZero()
        {
            float result = FuzzyMembership.High(
                val: 35f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void High_ValueHalfway_ReturnsHalf()
        {
            float result = FuzzyMembership.High(
                val: 60f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void High_ValueInsideTransition_ReturnsExpectedMembership()
        {
            float result = FuzzyMembership.High(
                val: 70f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(0.7f, result, Tolerance);
        }

        [Test]
        public void High_ValueAtFull_ReturnsOne()
        {
            float result = FuzzyMembership.High(
                val: 85f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void High_ValueAboveFull_ReturnsOne()
        {
            float result = FuzzyMembership.High(
                val: 100f,
                start: 35f,
                full: 85f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void High_FullEqualsStart_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzyMembership.High(
                    val: 50f,
                    start: 50f,
                    full: 50f));
        }

        [Test]
        public void High_FullBelowStart_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzyMembership.High(
                    val: 50f,
                    start: 80f,
                    full: 20f));
        }

        [Test]
        public void Low_ValueBelowFull_ReturnsOne()
        {
            float result = FuzzyMembership.Low(
                val: 10f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void Low_ValueAtFull_ReturnsOne()
        {
            float result = FuzzyMembership.Low(
                val: 20f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void Low_ValueHalfway_ReturnsHalf()
        {
            float result = FuzzyMembership.Low(
                val: 50f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Low_ValueInsideTransition_ReturnsExpectedMembership()
        {
            float result = FuzzyMembership.Low(
                val: 32f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(0.8f, result, Tolerance);
        }

        [Test]
        public void Low_ValueAtEnd_ReturnsZero()
        {
            float result = FuzzyMembership.Low(
                val: 80f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void Low_ValueAboveEnd_ReturnsZero()
        {
            float result = FuzzyMembership.Low(
                val: 100f,
                full: 20f,
                end: 80f);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void Low_EndEqualsFull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzyMembership.Low(
                    val: 50f,
                    full: 50f,
                    end: 50f));
        }

        [Test]
        public void Low_EndBelowFull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzyMembership.Low(
                    val: 50f,
                    full: 80f,
                    end: 20f));
        }
    }
}