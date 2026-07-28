using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzyIsStatementTests
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
        public void Statement_PreservesVariableAndSet()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzySetDefinition friendly =
                variable.GetSet("Friendly");

            FuzzyIsStatement statement =
                new FuzzyIsStatement(variable, friendly);

            Assert.AreSame(variable, statement.Variable);
            Assert.AreSame(friendly, statement.Set);
        }

        [Test]
        public void Statement_ToString_ReturnsReadableDescription()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzyIsStatement statement =
                new FuzzyIsStatement(
                    variable,
                    variable.GetSet("Friendly"));

            Assert.AreEqual(
                "Guard Relationship IS Friendly",
                statement.ToString());
        }

        [Test]
        public void Statement_Evaluate_ReturnsExpectedMembership()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzyIsStatement statement =
                new FuzzyIsStatement(
                    variable,
                    variable.GetSet("Friendly"));

            float result = statement.Evaluate(65f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [TestCase(-20f, 0f)]
        [TestCase(120f, 1f)]
        public void Statement_Evaluate_ClampsValueToVariableDomain(
            float rawValue,
            float expected)
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            FuzzyIsStatement statement =
                new FuzzyIsStatement(
                    variable,
                    variable.GetSet("Friendly"));

            float result = statement.Evaluate(rawValue);

            Assert.AreEqual(expected, result, Tolerance);
        }

        [Test]
        public void Statement_NullVariable_ThrowsArgumentNullException()
        {
            FuzzySetDefinition set =
                FuzzySetDefinition.High(
                    "Friendly",
                    start: 50f,
                    full: 80f);

            Assert.Throws<ArgumentNullException>(() =>
                new FuzzyIsStatement(
                    variable: null,
                    set: set));
        }

        [Test]
        public void Statement_NullSet_ThrowsArgumentNullException()
        {
            FuzzyVariableDefinition variable =
                CreateRelationshipVariable();

            Assert.Throws<ArgumentNullException>(() =>
                new FuzzyIsStatement(
                    variable,
                    set: null));
        }

        [Test]
        public void Statement_SetFromAnotherVariable_ThrowsException()
        {
            FuzzyVariableDefinition relationship =
                CreateRelationshipVariable();

            FuzzySetDefinition suspicious =
                FuzzySetDefinition.High(
                    "Suspicious",
                    start: 40f,
                    full: 80f);

            FuzzyVariableDefinition suspicion =
                new FuzzyVariableDefinition(
                    id: "Guard.Suspicion",
                    displayName: "Guard Suspicion",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[] { suspicious });

            Assert.Throws<ArgumentException>(() =>
                new FuzzyIsStatement(
                    relationship,
                    suspicion.GetSet("Suspicious")));
        }
    }
}