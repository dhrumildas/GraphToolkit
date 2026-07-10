using System.Collections.Generic;
using UnityEngine;

namespace FuzzyGraph.Runtime
{
    public class FuzzyGraphRuntimeAssetTest : MonoBehaviour
    {
        [SerializeField]
        private RuntimeFuzzyGraph graph;

        private void Start()
        {

            if (graph == null)
            {
                Debug.LogError("No RuntimeFuzzyGraph assigned.");
                return;
            }

            PersistentContext persistentContext =
                new PersistentContext();

            QueryBuilder queryBuilder =
                new QueryBuilder(persistentContext);

            SetInitialLiveState(queryBuilder);

            RunFirstEvent(
                queryBuilder,
                persistentContext);

            RunSecondEvent(queryBuilder);
        }

        private static void SetInitialLiveState(
            QueryBuilder queryBuilder)
        {
            queryBuilder.SetLive(
                "Guard.Relationship",
                FuzzyValue.FromInt(5));
        }

        private void RunFirstEvent(
            QueryBuilder queryBuilder,
            PersistentContext persistentContext)
        {
            WorldStateQuery query =
                queryBuilder.Build();

            MatchResult result =
                graph.Evaluate(
                    query,
                    "talkToGuard");

            LogResult(
                "FIRST EVENT",
                result,
                query);

            if (!result.hasMatch)
            {
                return;
            }

            int appliedCount =
                WriteBackProcessor.ApplyAll(
                    persistentContext,
                    result.rule.WriteBacks);

            Debug.Log(
                $"[FIRST EVENT] Write-backs applied: {appliedCount}");

            LogPersistentContext(persistentContext);
        }

        private void RunSecondEvent(
            QueryBuilder queryBuilder)
        {
            WorldStateQuery query =
                queryBuilder.Build();

            MatchResult result =
                graph.Evaluate(
                    query,
                    "talkToGuardAgain");

            LogResult(
                "SECOND EVENT",
                result,
                query);
        }

        private static void LogResult(
            string label,
            MatchResult result,
            WorldStateQuery query)
        {
            if (!result.hasMatch)
            {
                Debug.Log($"[{label}] No matching rule found.");
                return;
            }

            Debug.Log(
                $"[{label}] Winning Rule: {result.rule.ruleID}");

            Debug.Log(
                $"[{label}] Score: {result.scoreFloat:0.##}");

            foreach (RuntimeCriteria criteria in result.rule.Criteria)
            {
                float membership =
                    criteria.GetMembership(query);

                float contribution =
                    criteria.WeightedContribution(query);

                Debug.Log(
                    $"[{label}] Criterion: {criteria.fieldKey} | " +
                    $"Membership: {membership:0.##} | " +
                    $"Weight: {criteria.weight:0.##} | " +
                    $"Contribution: {contribution:0.##}");
            }

            foreach (RuntimeConsequence consequence
                     in result.rule.Consequences)
            {
                Debug.Log(
                    $"[{label}] Consequence: " +
                    $"{consequence.consequenceType} | " +
                    $"{consequence.payLoad}");
            }

            foreach (RuntimeWriteBack writeBack
                     in result.rule.WriteBacks)
            {
                Debug.Log(
                    $"[{label}] Write-Back: " +
                    $"{writeBack.operation} | " +
                    $"{writeBack.targetKey} | " +
                    $"{writeBack.val}");
            }
        }

        private static void LogPersistentContext(
            PersistentContext context)
        {
            Debug.Log("----- PERSISTENT CONTEXT -----");

            foreach (
                KeyValuePair<string, FuzzyValue> pair
                in context.Values)
            {
                Debug.Log($"{pair.Key} = {pair.Value}");
            }

            Debug.Log("------------------------------");
        }
    }
}