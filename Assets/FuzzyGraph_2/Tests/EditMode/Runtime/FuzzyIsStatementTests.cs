using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;
using System.Collections.Generic;
namespace FuzzyGraph2.Tests.EditMode
{
    public class FuzzyIsStatementTests
    {
        private const float Tolerance = 0.0001f;

        private static FuzzyVariableDefinition CreateRelationshipVariable()
        {
            return new FuzzyVariableDefinition(
                id: "Guard.Relationship",
                displayName: "Guard Relationship",
                minimum: 0f,
                maximum: 100f,
                sets: new[]
                {
                    FuzzySetDefinition.Low("Hostile", full: 20f, end: 50f),
                    FuzzySetDefinition.Range(
                        "Neutral",
                        start: 30f,
                        fullStart: 45f,
                        fullEnd: 55f,
                        end: 70f
                    ),
                    FuzzySetDefinition.High("Friendly", start: 50f, full: 80f)
                }
            );
        }

        private static FuzzyVariableDefinition CreateSuspicionVariable()
        {
            return new FuzzyVariableDefinition(
                id: "Guard.Suspicion",
                displayName: "Guard Suspicion",
                minimum: 0f,
                maximum: 100f,
                sets: new[] { FuzzySetDefinition.High("High", start: 20f, full: 70f) }
            );
        }

        [Test]
        public void AndExpression_UsesProductAnd()
        {
            FuzzyVariableDefinition relationship = CreateRelationshipVariable();

            FuzzyVariableDefinition suspicion = CreateSuspicionVariable();

            IFuzzyExpression expression = FuzzyExpression.And(
                new FuzzyIsStatement(relationship, relationship.GetSet("Friendly")),
                new FuzzyIsStatement(suspicion, suspicion.GetSet("High"))
            );

            TestValueSource source = new TestValueSource()
                .Set("Guard.Relationship", 68f)
                .Set("Guard.Suspicion", 55f);

            float result = expression.Evaluate(source);

            // Friendly = 0.60
            // Suspicion High = 0.70
            // AND = 0.60 × 0.70 = 0.42

            Assert.AreEqual(0.42f, result, Tolerance);
        }

        [Test]
        public void OrExpression_UsesProbabilisticSum()
        {
            FuzzyVariableDefinition relationship = CreateRelationshipVariable();

            FuzzyVariableDefinition suspicion = CreateSuspicionVariable();

            IFuzzyExpression expression = FuzzyExpression.Or(
                new FuzzyIsStatement(relationship, relationship.GetSet("Friendly")),
                new FuzzyIsStatement(suspicion, suspicion.GetSet("High"))
            );

            TestValueSource source = new TestValueSource()
                .Set("Guard.Relationship", 68f)
                .Set("Guard.Suspicion", 55f);

            float result = expression.Evaluate(source);

            // OR = 0.60 + 0.70 - (0.60 × 0.70)
            // OR = 0.88

            Assert.AreEqual(0.88f, result, Tolerance);
        }

        [Test]
        public void NotExpression_InvertsMembership()
        {
            FuzzyVariableDefinition suspicion = CreateSuspicionVariable();

            IFuzzyExpression expression = FuzzyExpression.Not(
                new FuzzyIsStatement(suspicion, suspicion.GetSet("High"))
            );

            TestValueSource source = new TestValueSource().Set("Guard.Suspicion", 55f);

            float result = expression.Evaluate(source);

            Assert.AreEqual(0.3f, result, Tolerance);
        }

        [Test]
        public void NestedExpression_ReturnsExpectedMembership()
        {
            FuzzyVariableDefinition relationship = CreateRelationshipVariable();

            FuzzyVariableDefinition suspicion = CreateSuspicionVariable();

            FuzzyIsStatement friendly = new FuzzyIsStatement(
                relationship,
                relationship.GetSet("Friendly")
            );

            FuzzyIsStatement suspicious = new FuzzyIsStatement(suspicion, suspicion.GetSet("High"));

            IFuzzyExpression expression = FuzzyExpression.And(
                friendly,
                FuzzyExpression.Or(suspicious, FuzzyExpression.Not(suspicious))
            );

            TestValueSource source = new TestValueSource()
                .Set("Guard.Relationship", 68f)
                .Set("Guard.Suspicion", 55f);

            float result = expression.Evaluate(source);

            // Suspicious = 0.70
            // NOT Suspicious = 0.30
            // OR = 0.70 + 0.30 - 0.21 = 0.79
            // AND Friendly 0.60 = 0.474

            Assert.AreEqual(0.474f, result, Tolerance);
        }

        [Test]
        public void AndExpression_OneChild_ThrowsArgumentException()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            Assert.Throws<ArgumentException>(() => FuzzyExpression.And(statement));
        }

        [Test]
        public void AndExpression_NullChild_ThrowsArgumentException()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            Assert.Throws<ArgumentException>(() => FuzzyExpression.And(statement, null));
        }

        [Test]
        public void NotExpression_NullChild_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FuzzyExpression.Not(null));
        }

        [Test]
        public void Statement_PreservesVariableAndSet()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzySetDefinition friendly = variable.GetSet("Friendly");

            FuzzyIsStatement statement = new FuzzyIsStatement(variable, friendly);

            Assert.AreSame(variable, statement.Variable);
            Assert.AreSame(friendly, statement.Set);
        }

        [Test]
        public void Statement_ToString_ReturnsReadableDescription()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            Assert.AreEqual("Guard Relationship IS Friendly", statement.ToString());
        }

        [Test]
        public void Statement_Evaluate_ReturnsExpectedMembership()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            float result = statement.Evaluate(65f);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [TestCase(-20f, 0f)]
        [TestCase(120f, 1f)]
        public void Statement_Evaluate_ClampsValueToVariableDomain(float rawValue, float expected)
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            float result = statement.Evaluate(rawValue);

            Assert.AreEqual(expected, result, Tolerance);
        }

        [Test]
        public void Statement_NullVariable_ThrowsArgumentNullException()
        {
            FuzzySetDefinition set = FuzzySetDefinition.High("Friendly", start: 50f, full: 80f);

            Assert.Throws<ArgumentNullException>(
                () => new FuzzyIsStatement(variable: null, set: set)
            );
        }

        [Test]
        public void Statement_NullSet_ThrowsArgumentNullException()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            Assert.Throws<ArgumentNullException>(() => new FuzzyIsStatement(variable, set: null));
        }

        [Test]
        public void Statement_SetFromAnotherVariable_ThrowsException()
        {
            FuzzyVariableDefinition relationship = CreateRelationshipVariable();

            FuzzySetDefinition suspicious = FuzzySetDefinition.High(
                "Suspicious",
                start: 40f,
                full: 80f
            );

            FuzzyVariableDefinition suspicion = new FuzzyVariableDefinition(
                id: "Guard.Suspicion",
                displayName: "Guard Suspicion",
                minimum: 0f,
                maximum: 100f,
                sets: new[] { suspicious }
            );

            Assert.Throws<ArgumentException>(
                () => new FuzzyIsStatement(relationship, suspicion.GetSet("Suspicious"))
            );
        }

        private sealed class TestValueSource : IFuzzyValueSource
        {
            private readonly Dictionary<string, float> _values = new Dictionary<string, float>();

            public TestValueSource Set(string variableId, float value)
            {
                _values[variableId] = value;
                return this;
            }

            public bool TryGetFloat(string variableId, out float value)
            {
                return _values.TryGetValue(variableId, out value);
            }
        }

        [Test]
        public void Statement_EvaluateFromSource_ReturnsExpectedMembership()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            TestValueSource source = new TestValueSource().Set("Guard.Relationship", 65f);

            float result = statement.Evaluate(source);

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Statement_EvaluateFromSource_UsesStableVariableId()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            TestValueSource source = new TestValueSource().Set("Guard.Relationship", 80f);

            float result = statement.Evaluate(source);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void Statement_MissingSourceValue_ThrowsKeyNotFoundException()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            TestValueSource source = new TestValueSource();

            Assert.Throws<KeyNotFoundException>(() => statement.Evaluate(source));
        }

        [Test]
        public void Statement_NullValueSource_ThrowsArgumentNullException()
        {
            FuzzyVariableDefinition variable = CreateRelationshipVariable();

            FuzzyIsStatement statement = new FuzzyIsStatement(
                variable,
                variable.GetSet("Friendly")
            );

            Assert.Throws<ArgumentNullException>(() => statement.Evaluate(source: null));
        }
    }
}
