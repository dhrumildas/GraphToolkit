using System;
using System.Collections.Generic;
using UnityEngine;

namespace FuzzyGraph.Runtime
{
    public static class RuleEvaluator
    {
        public static MatchResult Evaluate(
            IEnumerable<RuntimeRule> rules,
            WorldStateQuery query,
            string eventId)
        {
            RuntimeRule bestRule = null;
            float bestScore = -1f;
            int bestPriority = int.MinValue;

            foreach (RuntimeRule rule in rules)
            {
                //ignore rules from diff events
                if (!string.IsNullOrEmpty(rule.eventID) && rule.eventID != eventId)
                    continue;

                float score = CountMatchingCriteria(rule, query);

                //score for rule to qualify
                if (score <= 0f)
                    continue;

                //bool isBetter = score > bestScore || score == bestScore && rule.priority > bestPriority;

                bool sameScore = Mathf.Approximately(score, bestScore);

                bool higherScore = score > bestScore && !sameScore;

                bool higherPriority = sameScore && rule.priority > bestPriority;

                //bool hasHigherScore = score > bestScore;

                //bool hasHigherPriority = score == bestScore && rule.priority > bestPriority;

                if(higherScore ||  higherPriority)
                {
                    bestRule = rule;
                    bestScore = score;
                    bestPriority = rule.priority;
                }

                //if (isBetter)
                //{
                //    bestRule = rule;
                //    bestScore = score;
                //    bestPriority = rule.priority;
                //}
            }

            return new MatchResult
            {
                rule = bestRule,
                scoreFloat = bestScore
            };
        }

        private static float CountMatchingCriteria(RuntimeRule rule, WorldStateQuery query)
        {
            float score = 0f;
            foreach (RuntimeCriteria criteria in rule.Criteria)
            {
                //if (criteria.Matches(query))
                //    score += criteria.weight;
                
                score += criteria.WeightedContribution(query);
                //adding the idea : membership x weight
            }
            return score;
        }
    }
}