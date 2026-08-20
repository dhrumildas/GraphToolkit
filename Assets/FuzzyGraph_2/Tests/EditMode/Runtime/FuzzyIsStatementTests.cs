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
            private readonly Dictionary<string, float> _floatValues = new Dictionary<string, float>();

            private readonly Dictionary<string, bool> _boolValues = new Dictionary<string, bool>();

            private readonly Dictionary<string, string> _stringValues = new Dictionary<string, string>();

            public TestValueSource Set(string variableId, float value)
            {
                _floatValues[variableId] = value;
                return this;
            }

            public TestValueSource SetBool(string variableId, bool value)
            {
                _boolValues[variableId] = value;
                return this;
            }

            public TestValueSource SetId(string variableId,string value)
            {
                _stringValues[variableId] = value;
                return this;
            }

            public bool TryGetFloat(string variableId, out float value)
            {
                return _floatValues.TryGetValue(variableId, out value);
            }

            public bool TryGetBool(string variableId, out bool value)
            {
                return _boolValues.TryGetValue(variableId, out value);
            }

            public bool TryGetString(string variableId, out string value)
            {
                return _stringValues.TryGetValue(variableId,out value);
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

        [Test]
        public void BoolExpression_MatchingTrueValue_ReturnsOne()
        {
            IFuzzyExpression expression =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            TestValueSource source =
                new TestValueSource()
                    .SetBool("Guard.Charmed", true);

            float result = expression.Evaluate(source);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void BoolExpression_NonMatchingTrueValue_ReturnsZero()
        {
            IFuzzyExpression expression =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            TestValueSource source =
                new TestValueSource()
                    .SetBool("Guard.Charmed", false);

            float result = expression.Evaluate(source);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void BoolExpression_ExpectedFalse_ReturnsOne()
        {
            IFuzzyExpression expression =
                FuzzyExpression.BoolEquals(
                    "Player.FoundPitRoute",
                    false);

            TestValueSource source =
                new TestValueSource()
                    .SetBool(
                        "Player.FoundPitRoute",
                        false);

            float result = expression.Evaluate(source);

            Assert.AreEqual(1f, result, Tolerance);
        }

        [Test]
        public void BoolExpression_CanCombineWithFuzzyMembership()
        {
            FuzzyVariableDefinition relationship =
                CreateRelationshipVariable();

            IFuzzyExpression friendly =
                new FuzzyIsStatement(
                    relationship,
                    relationship.GetSet("Friendly"));

            IFuzzyExpression charmed =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            IFuzzyExpression expression =
                FuzzyExpression.And(
                    friendly,
                    charmed);

            TestValueSource source =
                new TestValueSource()
                    .Set("Guard.Relationship", 68f)
                    .SetBool("Guard.Charmed", true);

            float result = expression.Evaluate(source);

            // Friendly membership:
            // (68 - 50) / (80 - 50) = 0.60
            //
            // Guard.Charmed IS true = 1.00
            //
            // Product AND:
            // 0.60 × 1.00 = 0.60

            Assert.AreEqual(0.6f, result, Tolerance);
        }

        [Test]
        public void BoolExpression_FalseRequiredConditionMakesAndZero()
        {
            FuzzyVariableDefinition relationship =
                CreateRelationshipVariable();

            IFuzzyExpression friendly =
                new FuzzyIsStatement(
                    relationship,
                    relationship.GetSet("Friendly"));

            IFuzzyExpression charmed =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            IFuzzyExpression expression =
                FuzzyExpression.And(
                    friendly,
                    charmed);

            TestValueSource source =
                new TestValueSource()
                    .Set("Guard.Relationship", 68f)
                    .SetBool("Guard.Charmed", false);

            float result = expression.Evaluate(source);

            Assert.AreEqual(0f, result, Tolerance);
        }

        [Test]
        public void BoolExpression_MissingValue_ThrowsException()
        {
            IFuzzyExpression expression =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            TestValueSource source =
                new TestValueSource();

            Assert.Throws<KeyNotFoundException>(() =>
                expression.Evaluate(source));
        }

        [Test]
        public void BoolExpression_ToString_ReturnsReadableDescription()
        {
            IFuzzyExpression expression =
                FuzzyExpression.BoolEquals(
                    "Guard.Charmed",
                    true);

            Assert.AreEqual(
                "Guard.Charmed IS TRUE",
                expression.ToString());
        }

        [Test]
        public void BoolExpression_InvalidVariableId_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                FuzzyExpression.BoolEquals(
                    "   ",
                    true));
        }

        [Test]
        public void IdEquals_MatchingId_ReturnsOne()
        {
            IFuzzyExpression expression =
                FuzzyExpression.IdEquals(
                    "Dialogue.LastGuardChoice",
                    "lie");

            TestValueSource source =
                new TestValueSource()
                    .SetId(
                        "Dialogue.LastGuardChoice",
                        "lie");

            Assert.AreEqual(
                1f,
                expression.Evaluate(source),
                Tolerance);
        }

        [Test]
        public void IdEquals_DifferentId_ReturnsZero()
        {
            IFuzzyExpression expression =
                FuzzyExpression.IdEquals(
                    "Dialogue.LastGuardChoice",
                    "lie");

            TestValueSource source =
                new TestValueSource()
                    .SetId(
                        "Dialogue.LastGuardChoice",
                        "truth");

            Assert.AreEqual(
                0f,
                expression.Evaluate(source),
                Tolerance);
        }

        [Test]
        public void NumberCompare_LessThan_ReturnsOne()
        {
            IFuzzyExpression expression =
                FuzzyExpression.NumberCompare(
                    "Dialogue.ResponseSeconds",
                    NumberComparison.LessThan,
                    3f);

            TestValueSource source =
                new TestValueSource()
                    .Set(
                        "Dialogue.ResponseSeconds",
                        2.5f);

            Assert.AreEqual(
                1f,
                expression.Evaluate(source),
                Tolerance);
        }

        [Test]
        public void NumberCompare_InclusiveRange_ReturnsOne()
        {
            IFuzzyExpression expression =
                FuzzyExpression.NumberCompare(
                    "Dialogue.ResponseSeconds",
                    NumberComparison.InclusiveRange,
                    3f,
                    7f);

            TestValueSource source =
                new TestValueSource()
                    .Set(
                        "Dialogue.ResponseSeconds",
                        5f);

            Assert.AreEqual(
                1f,
                expression.Evaluate(source),
                Tolerance);
        }
    }
}
