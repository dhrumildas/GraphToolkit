using System;
using System.Collections.Generic;

namespace FuzzyGraph2.Runtime
{
    public sealed class SugenoRule
    {
        public string Id { get; }

        public IFuzzyExpression Antecedent { get; }

        public float Consequent { get; }

        public SugenoRule(
            string id,
            IFuzzyExpression antecedent,
            float consequent)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "A Sugeno rule must have a readable ID.",
                    nameof(id));
            }

            Antecedent = antecedent ??
                throw new ArgumentNullException(nameof(antecedent));

            if (float.IsNaN(consequent) ||
                float.IsInfinity(consequent))
            {
                throw new ArgumentException(
                    "A Sugeno consequent must be a finite number.",
                    nameof(consequent));
            }

            Id = id.Trim();
            Consequent = consequent;
        }
    }
    public sealed class SugenoRuleActivation
    {
        public SugenoRule Rule { get; }

        public float FiringStrength { get; }

        public float WeightedConsequent { get; }

        internal SugenoRuleActivation(
            SugenoRule rule,
            float firingStrength)
        {
            Rule = rule;
            FiringStrength = firingStrength;
            WeightedConsequent =
                firingStrength * rule.Consequent;
        }
    }
    public sealed class SugenoInferenceResult
    {
        private readonly List<SugenoRuleActivation> _activations;

        public IReadOnlyList<SugenoRuleActivation> Activations =>
            _activations;

        public float Numerator { get; }

        public float Denominator { get; }

        public float Output { get; }

        public bool HasOutput => Denominator > 0f;

        internal SugenoInferenceResult(
            List<SugenoRuleActivation> activations,
            float numerator,
            float denominator)
        {
            _activations =
                new List<SugenoRuleActivation>(activations);

            Numerator = numerator;
            Denominator = denominator;

            Output = denominator > 0f
                ? numerator / denominator
                : 0f;
        }
    }
    public static class SugenoEvaluator
    {
        public static SugenoInferenceResult Evaluate(
            IEnumerable<SugenoRule> rules,
            IFuzzyValueSource source)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<SugenoRuleActivation> activations =
                new List<SugenoRuleActivation>();

            float numerator = 0f;
            float denominator = 0f;

            foreach (SugenoRule rule in rules)
            {
                if (rule == null)
                {
                    throw new ArgumentException(
                        "The Sugeno rule collection contains null.",
                        nameof(rules));
                }

                float firingStrength =
                    FuzzyMath.Clamp(
                        rule.Antecedent.Evaluate(source));

                if (firingStrength <= 0f)
                    continue;

                SugenoRuleActivation activation =
                    new SugenoRuleActivation(
                        rule,
                        firingStrength);

                activations.Add(activation);

                numerator += activation.WeightedConsequent;
                denominator += firingStrength;
            }

            return new SugenoInferenceResult(
                activations,
                numerator,
                denominator);
        }
    }
}