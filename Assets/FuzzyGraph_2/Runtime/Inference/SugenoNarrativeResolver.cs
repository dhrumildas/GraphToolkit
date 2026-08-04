using System;
using System.Collections.Generic;
using FuzzyGraph.Runtime;

namespace FuzzyGraph2.Runtime
{
    public sealed class SugenoNarrativeResult
    {
        public SugenoInferenceResult Inference { get; }

        public bool UsedFallback { get; }

        public NarrativeOutputMapping.Band SelectedBand { get; }

        public string OutcomeId => SelectedBand.OutcomeId;

        public IReadOnlyList<RuntimeConsequence> Consequences =>
            SelectedBand.Consequences;

        public IReadOnlyList<RuntimeWriteBack> WriteBacks =>
            SelectedBand.WriteBacks;

        internal SugenoNarrativeResult(
            SugenoInferenceResult inference,
            NarrativeOutputMapping.Band selectedBand,
            bool usedFallback)
        {
            Inference = inference ??
                throw new ArgumentNullException(nameof(inference));

            SelectedBand = selectedBand ??
                throw new ArgumentNullException(nameof(selectedBand));
            
            UsedFallback = usedFallback;
        }
    }
    public static class SugenoNarrativeResolver
    {
        public static SugenoNarrativeResult Resolve(
            IEnumerable<SugenoRule> rules,
            NarrativeOutputMapping outputMapping,
            WorldStateQuery query)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            if (outputMapping == null)
            {
                throw new ArgumentNullException(
                    nameof(outputMapping));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(query);

            SugenoInferenceResult inference =
                SugenoEvaluator.Evaluate(
                    rules,
                    source);

            if (!inference.HasOutput)
            {
                NarrativeOutputMapping.Band fallbackBand =
                    outputMapping.GetFallbackBand();

                return new SugenoNarrativeResult(
                    inference,
                    fallbackBand,
                    usedFallback: true);
            }

            NarrativeOutputMapping.Band selectedBand =
                outputMapping.MapBand(inference.Output);

            return new SugenoNarrativeResult(
                inference,
                selectedBand,
                usedFallback:false);
        }
    }
}