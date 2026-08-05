using System;
using NUnit.Framework;
using FuzzyGraph2.Runtime;
using System.Collections.Generic;
namespace FuzzyGraph2.Tests.EditMode
{
    public class SugenoEvaluatorTests
    {
        private const float Tolerance = 0.0001f;

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

        private sealed class EmptyValueSource : IFuzzyValueSource
        {
            public bool TryGetFloat(string variableId, out float value)
            {
                value = 0f;
                return false;
            }

            public bool TryGetBool(string variableId, out bool value)
            {
                value = false;
                return false;
            }

            public bool TryGetString(string variableId, out string value)
            {
                value = null;
                return false;
            }
        }

        private sealed class DictionaryValueSource : IFuzzyValueSource
        {
            private readonly Dictionary<string, float> _values =
                new Dictionary<string, float>();

            public DictionaryValueSource Set(
                string variableId,
                float value)
            {
                _values[variableId] = value;
                return this;
            }

            public bool TryGetFloat(string variableId,out float value)
            {
                return _values.TryGetValue(variableId, out value);
            }

            public bool TryGetBool(string variableId, out bool value)
            {
                value = false;
                return false;
            }

            public bool TryGetString(string variableId, out string value)
            {
                value= null;
                return false;
            }
        }

        [Test]
        public void Evaluate_VendorScenario_UsesRealFuzzyExpressions()
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

            FuzzyVariableDefinition blindnessEvidence =
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

            DictionaryValueSource source =
                new DictionaryValueSource()
                    .Set("Vendor.CarpetProximity", 0.7f)
                    .Set("Vendor.Perspiration", 0.585f)
                    .Set("Vendor.BlindnessEvidence", 0.81f);

            SugenoRule revealRule =
                new SugenoRule(
                    id: "Vendor.RevealSecret",
                    antecedent: FuzzyExpression.And(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("High")),

                        new FuzzyIsStatement(
                            perspiration,
                            perspiration.GetSet("High")),

                        new FuzzyIsStatement(
                            blindnessEvidence,
                            blindnessEvidence.GetSet("High"))),
                    consequent: 90f);

            SugenoRule nervousRule =
                new SugenoRule(
                    id: "Vendor.BecomesNervous",
                    antecedent: FuzzyExpression.And(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("Medium")),

                        new FuzzyIsStatement(
                            perspiration,
                            perspiration.GetSet("High"))),
                    consequent: 60f);

            SugenoRule concealRule =
                new SugenoRule(
                    id: "Vendor.ConcealsSecret",
                    antecedent: FuzzyExpression.Or(
                        new FuzzyIsStatement(
                            proximity,
                            proximity.GetSet("Low")),

                        new FuzzyIsStatement(
                            blindnessEvidence,
                            blindnessEvidence.GetSet("Low"))),
                    consequent: 15f);

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    new[]
                    {
                revealRule,
                nervousRule,
                concealRule
                    },
                    source);

            Assert.AreEqual(3, result.Activations.Count);

            Assert.AreEqual(
                0.5025f,
                result.Activations[0].FiringStrength,
                Tolerance);

            Assert.AreEqual(
                0.335f,
                result.Activations[1].FiringStrength,
                Tolerance);

            Assert.AreEqual(
                0.1f,
                result.Activations[2].FiringStrength,
                Tolerance);

            Assert.AreEqual(
                66.825f,
                result.Numerator,
                Tolerance);

            Assert.AreEqual(
                0.9375f,
                result.Denominator,
                Tolerance);

            Assert.AreEqual(
                71.28f,
                result.Output,
                Tolerance);
        }

        [Test]
        public void Rule_PreservesConfiguration()
        {
            ConstantExpression antecedent =
                new ConstantExpression(0.5f);

            SugenoRule rule =
                new SugenoRule(
                    id: "Vendor.Reveal",
                    antecedent: antecedent,
                    consequent: 90f);

            Assert.AreEqual("Vendor.Reveal", rule.Id);
            Assert.AreSame(antecedent, rule.Antecedent);
            Assert.AreEqual(90f, rule.Consequent, Tolerance);
        }

        [Test]
        public void Evaluate_BlendsEveryActiveRule()
        {
            SugenoRule[] rules =
            {
                new SugenoRule(
                    "HighExposure",
                    new ConstantExpression(0.67f),
                    90f),

                new SugenoRule(
                    "MediumExposure",
                    new ConstantExpression(0.50f),
                    60f),

                new SugenoRule(
                    "LowExposure",
                    new ConstantExpression(0.10f),
                    15f)
            };

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    rules,
                    new EmptyValueSource());

            Assert.AreEqual(
                72.28346f,
                result.Output,
                Tolerance);
        }

        [Test]
        public void Evaluate_RecordsNumeratorAndDenominator()
        {
            SugenoRule[] rules =
            {
                new SugenoRule(
                    "HighExposure",
                    new ConstantExpression(0.67f),
                    90f),

                new SugenoRule(
                    "MediumExposure",
                    new ConstantExpression(0.50f),
                    60f),

                new SugenoRule(
                    "LowExposure",
                    new ConstantExpression(0.10f),
                    15f)
            };

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    rules,
                    new EmptyValueSource());

            Assert.AreEqual(
                91.8f,
                result.Numerator,
                Tolerance);

            Assert.AreEqual(
                1.27f,
                result.Denominator,
                Tolerance);

            Assert.AreEqual(3, result.Activations.Count);
            Assert.IsTrue(result.HasOutput);
        }

        [Test]
        public void Evaluate_RecordsEachRuleContribution()
        {
            SugenoRule rule =
                new SugenoRule(
                    "Reveal",
                    new ConstantExpression(0.75f),
                    80f);

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    new[] { rule },
                    new EmptyValueSource());

            SugenoRuleActivation activation =
                result.Activations[0];

            Assert.AreSame(rule, activation.Rule);

            Assert.AreEqual(
                0.75f,
                activation.FiringStrength,
                Tolerance);

            Assert.AreEqual(
                60f,
                activation.WeightedConsequent,
                Tolerance);
        }

        [Test]
        public void Evaluate_ZeroActivationIsIgnored()
        {
            SugenoRule[] rules =
            {
                new SugenoRule(
                    "Inactive",
                    new ConstantExpression(0f),
                    100f),

                new SugenoRule(
                    "Active",
                    new ConstantExpression(0.5f),
                    50f)
            };

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    rules,
                    new EmptyValueSource());

            Assert.AreEqual(1, result.Activations.Count);
            Assert.AreEqual(50f, result.Output, Tolerance);
        }

        [Test]
        public void Evaluate_NoActiveRules_ReturnsNoOutput()
        {
            SugenoRule rule =
                new SugenoRule(
                    "Inactive",
                    new ConstantExpression(0f),
                    90f);

            SugenoInferenceResult result =
                SugenoEvaluator.Evaluate(
                    new[] { rule },
                    new EmptyValueSource());

            Assert.IsFalse(result.HasOutput);
            Assert.AreEqual(0f, result.Output, Tolerance);
            Assert.AreEqual(0, result.Activations.Count);
        }

        [Test]
        public void Evaluate_NullRules_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                SugenoEvaluator.Evaluate(
                    rules: null,
                    source: new EmptyValueSource()));
        }

        [Test]
        public void Evaluate_NullSource_ThrowsArgumentNullException()
        {
            SugenoRule rule =
                new SugenoRule(
                    "Test",
                    new ConstantExpression(1f),
                    50f);

            Assert.Throws<ArgumentNullException>(() =>
                SugenoEvaluator.Evaluate(
                    new[] { rule },
                    source: null));
        }

        [Test]
        public void Evaluate_NullRule_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                SugenoEvaluator.Evaluate(
                    new SugenoRule[] { null },
                    new EmptyValueSource()));
        }

        [Test]
        public void Rule_InvalidConsequent_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new SugenoRule(
                    "Invalid",
                    new ConstantExpression(1f),
                    float.NaN));
        }
    }
}