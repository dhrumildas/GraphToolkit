using UnityEngine;
using System.Collections.Generic;

namespace FuzzyGraph.Runtime
{
    public class RuntimeFuzzyGraph : ScriptableObject
    {
        public List<RuntimeRule> rules = new();     // to store runtime rules
        public MatchResult Evaluate(WorldStateQuery query, string eventID)
        {
            return RuleEvaluator.Evaluate(rules, query, eventID);   //sends rules to the RuleEvaluator
        }
    }
}
