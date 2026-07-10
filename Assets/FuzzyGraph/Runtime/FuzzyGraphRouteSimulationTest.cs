using System.Collections.Generic;
using UnityEngine;

namespace FuzzyGraph.Runtime
{
    public class FuzzyGraphRouteSimulationTest : MonoBehaviour
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

            Debug.Log("========== FUZZYGRAPH ROUTE SIMULATION ==========");

            RunQuietRoute();
            RunAlertedNoDisguiseRoute();
            RunAlertedDisguiseRoute();
        }

        private void RunQuietRoute()
        {
            PersistentContext context = new PersistentContext();

            context.Set("Player.HasFruit",FuzzyValue.FromBool(true));

            context.Set("Player.FoundPitRoute",FuzzyValue.FromBool(true));

            context.Set("Guard.Alerted",FuzzyValue.FromBool(false));

            RunExitEvent("ROUTE A — Quiet exit",context);
        }

        private void RunAlertedNoDisguiseRoute()
        {
            PersistentContext context = new PersistentContext();

            context.Set("Player.HasFruit",FuzzyValue.FromBool(true));

            context.Set("Player.FoundPitRoute",FuzzyValue.FromBool(true));

            context.Set("Guard.Alerted",FuzzyValue.FromBool(true));

            context.Set("Player.HasGuardDisguise",FuzzyValue.FromBool(false));

            RunExitEvent("ROUTE B — Alerted, no disguise",context);
        }

        private void RunAlertedDisguiseRoute()
        {
            PersistentContext context = new PersistentContext();

            context.Set("Player.HasFruit",FuzzyValue.FromBool(true));

            context.Set("Player.FoundPitRoute",FuzzyValue.FromBool(true));

            context.Set("Guard.Alerted",FuzzyValue.FromBool(true));

            context.Set("Player.HasGuardDisguise",FuzzyValue.FromBool(true));

            RunExitEvent("ROUTE C — Alerted, wearing disguise",context);
        }

        private void RunExitEvent(string routeName,PersistentContext context)
        {
            Debug.Log("");
            Debug.Log($"===== {routeName} =====");

            QueryBuilder queryBuilder = new QueryBuilder(context);

            WorldStateQuery query = queryBuilder.Build();

            MatchResult result = graph.Evaluate(query,"ExitVault");

            if (!result.hasMatch)
            {
                Debug.Log("No matching exit rule found.");
                return;
            }

            Debug.Log($"Winning Rule: {result.rule.ruleID}");

            Debug.Log($"Score: {result.scoreFloat:0.##}");

            foreach (RuntimeCriteria criteria in result.rule.Criteria)
            {
                float membership =criteria.GetMembership(query);

                float contribution =criteria.WeightedContribution(query);

                Debug.Log(
                    $"Criterion: {criteria.fieldKey} | " +
                    $"Membership: {membership:0.##} | " +
                    $"Weight: {criteria.weight:0.##} | " +
                    $"Contribution: {contribution:0.##}");
            }

            foreach (RuntimeConsequence consequence in result.rule.Consequences)
            {
                Debug.Log(
                    $"Consequence: {consequence.consequenceType} | " +
                    $"{consequence.payLoad}");
            }

            int appliedCount = WriteBackProcessor.ApplyAll(context,result.rule.WriteBacks);

            Debug.Log($"Write-backs applied: {appliedCount}");

            LogPersistentContext(context);
        }

        private static void LogPersistentContext(PersistentContext context)
        {
            Debug.Log("Persistent Context:");

            foreach (KeyValuePair<string, FuzzyValue> pair in context.Values)
            {
                Debug.Log($"{pair.Key} = {pair.Value}");
            }
        }
    }
}