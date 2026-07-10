using UnityEngine;
using System.Collections.Generic;
using System;

//REMEMBER
//criterion contribution = membership x weight
//rule score = sum of all contributions

namespace FuzzyGraph.Runtime
{
    public class FuzzyGraphRuntimeTest : MonoBehaviour
    {
        private void Start()
        {
            WorldStateQuery query = new WorldStateQuery();
            PersistentContext pc = new PersistentContext();
            QueryBuilder qb = new QueryBuilder(pc);

            SetInitialStage(qb);

            query.Set("Player.HasStolenItem", FuzzyValue.FromBool(true));
            query.Set("Guard.Relationship", FuzzyValue.FromInt(2));
            query.Set("Player.LiedBefore", FuzzyValue.FromBool(true));

            List<RuntimeRule> rules = CreateRules();
            RunFirstEvent(rules, qb, pc);
            RunSecondEvent(rules, qb);

            MatchResult result = RuleEvaluator.Evaluate(rules, query, "talkToGuard");

            if(!result.hasMatch)
            {
                Debug.Log("No matching rule found");
                return;
            }

            Debug.Log($"Winning Rule: {result.rule.ruleID}");
            Debug.Log($"Score: {result.scoreFloat:0.##}");

            foreach(RuntimeCriteria criteria in result.rule.Criteria)
            {
                float membership = criteria.GetMembership(query);

                float contribution = criteria.WeightedContribution(query);

                Debug.Log(
                    $"Criterion : {criteria.fieldKey} | " +
                    $"Mode : {criteria.evalMode} | " +
                    $"Membership : {membership} | " +
                    $"Weight : {criteria.weight:0.##} | " +
                    $"Contribution : {contribution:0.##} | ");
            }

            foreach(RuntimeConsequence consequence in result.rule.Consequences)
            {
                Debug.Log($"Consequence: {consequence.consequenceType} | {consequence.payLoad}");
            }
        }

        private void RunSecondEvent(List<RuntimeRule> rules, QueryBuilder qb)
        {
            //build again after first event
            //this query contains persistent narrative memory

            WorldStateQuery secondQuery = qb.Build();
            MatchResult secondResult = RuleEvaluator.Evaluate(rules, secondQuery, "talkToGuardAgain");
            LogResult("SECOND EVENT", secondResult, secondQuery);
        }

        private void RunFirstEvent(List<RuntimeRule> rules, QueryBuilder qb, PersistentContext pc)
        {
            WorldStateQuery firstQuery = qb.Build();
            MatchResult firstResult = RuleEvaluator.Evaluate(rules, firstQuery, "talkToGuard");
            LogResult("FIRST EVENT", firstResult, firstQuery);

            if (!firstResult.hasMatch) return;

            int appliedCount = WriteBackProcessor.ApplyAll(pc, firstResult.rule.WriteBacks);

            Debug.Log($"[FIRST EVENT] Write-backs applied: {appliedCount}");

            LogPersistentContext(pc);
        }

        private void LogPersistentContext(PersistentContext pc)
        {
            Debug.Log("----- PERSISTENT CONTEXT -----");
            foreach (KeyValuePair<string, FuzzyValue> pair in pc.Values)
                Debug.Log($"{pair.Key} = {pair.Value}");
            Debug.Log("------------------------------");
        }

        private void LogResult(string label, MatchResult result, WorldStateQuery query)
        {
            if (!result.hasMatch)
            {
                Debug.Log($"[{label}] No matching rule found.");
                return;
            }

            Debug.Log(
                $"[{label}] Winning Rule: " +
                $"{result.rule.ruleID}");

            Debug.Log(
                $"[{label}] Score: " +
                $"{result.scoreFloat:0.##}");

            foreach (RuntimeCriteria criteria in result.rule.Criteria)
            {
                float membership = criteria.GetMembership(query);

                float contribution = criteria.WeightedContribution(query);

                Debug.Log(
                    $"[{label}] Criterion: {criteria.fieldKey} | " +
                    $"Membership: {membership:0.##} | " +
                    $"Weight: {criteria.weight:0.##} | " +
                    $"Contribution: {contribution:0.##}");
            }

            foreach (RuntimeConsequence consequence in result.rule.Consequences)
            {
                Debug.Log(
                    $"[{label}] Consequence: " +
                    $"{consequence.consequenceType} | " +
                    $"{consequence.payLoad}");
            }
        }

        private List<RuntimeRule> CreateRules()
        {
            return new List<RuntimeRule>
            {
                CreateGenericFirstMeetingRule(),
                CreateSuspiciousGuardRule(),
                CreateGenericReturnRule(),
                CreateGuardRemembersRule()
            };
        }

        private RuntimeRule CreateGuardRemembersRule()
        {
            return new RuntimeRule
            {
                ruleID = "guard_remembers_previous_confrontation",
                eventID = "talkToGuardAgain",
                priority = 10,

                Criteria = new List<RuntimeCriteria>
                {
                    new RuntimeCriteria
                    {
                        fieldKey = "Guard.Suspicious",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 5f
                    },

                    new RuntimeCriteria
                    {
                        fieldKey = "Player.WasConfronted",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 4f
                    },

                    new RuntimeCriteria
                    {
                        fieldKey = "Guard.Suspicion",
                        criteriaOperator = CriteriaOperator.GreaterThanOrEqual,
                        expectedVal = FuzzyValue.FromInt(25),
                        weight = 3f
                    }
                },

                Consequences = new List<RuntimeConsequence>
                {
                    new RuntimeConsequence
                    {
                        consequenceType = ConsequenceType.Dialogue,
                        payLoad = "I remember you. We are not finished."
                    }
                }
            };
        }

        private RuntimeRule CreateGenericReturnRule()
        {
            return new RuntimeRule
            {
                ruleID = "suspicious_guard_response",
                eventID = "talkToGuard",
                priority = 10,

                Criteria = new List<RuntimeCriteria>
                {
                    new RuntimeCriteria
                    {
                        fieldKey = "Player.HasStolenItem",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 5f,
                        evalMode = FuzzyEvaluationMode.Crisp
                    },
                    new RuntimeCriteria
                    {
                        fieldKey = "Guard.Relationship",
                        criteriaOperator = CriteriaOperator.LessThanOrEqual,
                        expectedVal = FuzzyValue.FromInt(3),
                        weight = 3f,
                        evalMode = FuzzyEvaluationMode.FuzzyLow,
                        fuzzyMin = 2f,
                        fuzzyMax = 8f
                    },
                    new RuntimeCriteria
                    {
                        fieldKey = "Player.LiedBefore",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 4f,
                        evalMode = FuzzyEvaluationMode.Crisp
                    }
                },

                Consequences = new List<RuntimeConsequence>
                {
                    new RuntimeConsequence
                    {
                        consequenceType = ConsequenceType.Dialogue,
                        payLoad = "I know what you did. Do not lie to me again."
                    }
                },

                WriteBacks = new List<RuntimeWriteBack>
                {
                    new RuntimeWriteBack
                    {
                        targetKey = "Guard.Suspicious",
                        operation = WriteBackOperation.Set,
                        val = FuzzyValue.FromBool(true)
                    },
                    new RuntimeWriteBack
                    {
                        targetKey = "Player.WasConfronted",
                        operation = WriteBackOperation.Set,
                        val = FuzzyValue.FromBool(true)
                    },
                    new RuntimeWriteBack
                    {
                        targetKey = "Guard.Suspicion",
                        operation = WriteBackOperation.Add,
                        val = FuzzyValue.FromInt(25)
                    }
                }
            };
        }

        private RuntimeRule CreateSuspiciousGuardRule()
        {
            return new RuntimeRule
            {
                ruleID = "suspicious_guard_response",
                eventID = "talkToGuard",
                priority = 10,

                Criteria = new List<RuntimeCriteria>
                {
                    new RuntimeCriteria
                    {
                        fieldKey = "Player.HasStolenItem",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 5f,
                        evalMode = FuzzyEvaluationMode.Crisp
                    },
                    new RuntimeCriteria
                    {
                        fieldKey = "Guard.Relationship",
                        criteriaOperator = CriteriaOperator.LessThanOrEqual,
                        expectedVal = FuzzyValue.FromInt(3),
                        weight = 3f,
                        evalMode = FuzzyEvaluationMode.FuzzyLow,
                        fuzzyMin = 2f,
                        fuzzyMax = 8f
                    },

                    new RuntimeCriteria
                    {
                        fieldKey = "Player.LiedBefore",
                        criteriaOperator = CriteriaOperator.Equals,
                        expectedVal = FuzzyValue.FromBool(true),
                        weight = 4f,
                        evalMode = FuzzyEvaluationMode.Crisp
                    }
                },

                Consequences = new List<RuntimeConsequence>
                {
                    new RuntimeConsequence
                    {
                        consequenceType =
                            ConsequenceType.Dialogue,

                        payLoad =
                            "I know what you did. Do not lie to me again."
                    }
                },

                WriteBacks = new List<RuntimeWriteBack>
                {
                    new RuntimeWriteBack
                    {
                        targetKey = "Guard.Suspicious",
                        operation = WriteBackOperation.Set,
                        val = FuzzyValue.FromBool(true)
                    },

                    new RuntimeWriteBack
                    {
                        targetKey = "Player.WasConfronted",
                        operation = WriteBackOperation.Set,
                        val = FuzzyValue.FromBool(true)
                    },

                    new RuntimeWriteBack
                    {
                        targetKey = "Guard.Suspicion",
                        operation = WriteBackOperation.Add,
                        val = FuzzyValue.FromInt(25)
                    }
                }
            };
        }

        private RuntimeRule CreateGenericFirstMeetingRule()
        {
            return new RuntimeRule
            {
                ruleID = "generic_guard_response",
                eventID = "talkToGuard",
                priority = 0,

                Criteria = new List<RuntimeCriteria>
                {
                    new RuntimeCriteria
                    {
                        fieldKey = "Guard.Relationship",
                        criteriaOperator = CriteriaOperator.Exists,
                        weight = 1f
                    }
                },

                Consequences = new List<RuntimeConsequence>
                {
                    new RuntimeConsequence
                    {
                        consequenceType = ConsequenceType.Dialogue,
                        payLoad = "The guard gives you a cautious greeting."
                    }
                }
            };
        }

        private void SetInitialStage(QueryBuilder qb)
        {
            qb.SetLive("Player.HasStolenItem", FuzzyValue.FromBool(true));
            qb.SetLive("Guard.Relationship", FuzzyValue.FromInt(5));
            qb.SetLive("Player.LiedBefore", FuzzyValue.FromBool(true));

        }
    }
}
