using System;
using System.Collections.Generic;
using NUnit.Framework;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzyVariableDefinitionTests
    {
        private const float Tolerance = 0.0001f;

        private static FuzzyVariableDefinition
            CreateRelationshipVariable()
        {
            return new FuzzyVariableDefinition(
                id: "Guard.Relationship",
                displayName: "Guard Relationship",
                minimum: 0f,
                maximum: 100f,
                sets: new[]
                {
                    FuzzySetDefinition.Low(
                        "Hostile",
                        full: 20f,
                        end: 50f),

                    FuzzySetDefinition.Range(
                        "Neutral",
                        start: 30f,
                        fullStart: 45f,
                        fullEnd: 55f,
                        end: 70f),

                    FuzzySetDefinition.High(
                        "Friendly",
                        start: 50f,
                        full: 80f)
                });
        }

        [Test]
        public void Variable_PreservesIdentityDomainAndSets()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            Assert.AreEqual(
                "Guard.Relationship",
                variable.Id);

            Assert.AreEqual(
                "Guard Relationship",
                variable.DisplayName);

            Assert.AreEqual(0f, variable.Minimum, Tolerance);
            Assert.AreEqual(100f, variable.Maximum, Tolerance);
            Assert.AreEqual(3, variable.Sets.Count);
        }

        [Test]
        public void Variable_GetSetAndEvaluate_ReturnsMembership()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzySetDefinition friendly =
                variable.GetSet("Friendly");

            float result = variable.Evaluate(
                friendly,
                rawValue: 65f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Variable_GetSet_IsCaseInsensitive()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzySetDefinition result =
                variable.GetSet("friendly");

            Assert.AreEqual("Friendly", result.Name);
        }

        [TestCase(-20f, 0f)]
        [TestCase(140f, 100f)]
        public void Variable_ClampToDomain_ReturnsNearestBoundary(
            float input,
            float expected)
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            float result = variable.ClampToDomain(input);

            Assert.AreEqual(expected, result, Tolerance);
        }

        [Test]
        public void Variable_ForeignSet_ThrowsArgumentException()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzySetDefinition foreignSet =
                FuzzySetDefinition.High(
                    "Foreign",
                    start: 20f,
                    full: 80f);

            Assert.Throws<ArgumentException>(() =>
                variable.Evaluate(foreignSet, 50f));
        }

        [Test]
        public void Variable_DuplicateSetNames_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: "Guard Relationship",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[]
                    {
                        FuzzySetDefinition.Low(
                            "Friendly",
                            full: 20f,
                            end: 50f),

                        FuzzySetDefinition.High(
                            "friendly",
                            start: 50f,
                            full: 80f)
                    }));
        }

        [Test]
        public void Variable_EmptySets_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: "Guard Relationship",
                    minimum: 0f,
                    maximum: 100f,
                    sets: Array.Empty<FuzzySetDefinition>()));
        }

        [Test]
        public void Variable_NullSet_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: "Guard Relationship",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new FuzzySetDefinition[]
                    {
                        null
                    }));
        }

        [TestCase(-10f, 80f, FuzzySetShape.High)]
        [TestCase(20f, 120f, FuzzySetShape.Low)]
        public void Variable_SetOutsideDomain_ThrowsException(
            float first,
            float second,
            FuzzySetShape shape)
        {
            FuzzySetDefinition set =
                shape == FuzzySetShape.High
                    ? FuzzySetDefinition.High(
                        "Test",
                        first,
                        second)
                    : FuzzySetDefinition.Low(
                        "Test",
                        first,
                        second);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new FuzzyVariableDefinition(
                    id: "Test.Value",
                    displayName: "Test Value",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[] { set }));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("Guard Relationship")]
        public void Variable_InvalidId_ThrowsArgumentException(
            string invalidId)
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: invalidId,
                    displayName: "Guard Relationship",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[]
                    {
                        FuzzySetDefinition.High(
                            "Friendly",
                            50f,
                            80f)
                    }));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Variable_InvalidDisplayName_ThrowsArgumentException(
            string invalidDisplayName)
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: invalidDisplayName,
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[]
                    {
                        FuzzySetDefinition.High(
                            "Friendly",
                            50f,
                            80f)
                    }));
        }

        [TestCase(100f, 100f)]
        [TestCase(100f, 0f)]
        public void Variable_InvalidDomain_ThrowsArgumentException(
            float minimum,
            float maximum)
        {
            Assert.Throws<ArgumentException>(() =>
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: "Guard Relationship",
                    minimum: minimum,
                    maximum: maximum,
                    sets: new[]
                    {
                        FuzzySetDefinition.High(
                            "Friendly",
                            50f,
                            80f)
                    }));
        }
    }
}