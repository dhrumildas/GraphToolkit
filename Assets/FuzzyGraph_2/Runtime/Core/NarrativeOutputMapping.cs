using System;
using System.Collections.Generic;

namespace FuzzyGraph2.Runtime
{

    public sealed class NarrativeOutputMapping
    {

        public sealed class Band
        {
            public float MinimumInclusive { get; }

            public string OutcomeId { get; }

            public Band(
                float minimumInclusive,
                string outcomeId)
            {
                if (float.IsNaN(minimumInclusive) ||
                    float.IsInfinity(minimumInclusive))
                {
                    throw new ArgumentException(
                        "A narrative-output threshold must be finite.",
                        nameof(minimumInclusive));
                }

                if (string.IsNullOrWhiteSpace(outcomeId))
                {
                    throw new ArgumentException(
                        "A narrative-output band requires an outcome ID.",
                        nameof(outcomeId));
                }

                MinimumInclusive = minimumInclusive;
                OutcomeId = outcomeId.Trim();
            }
        }

        private readonly List<Band> _bands;

        public IReadOnlyList<Band> Bands => _bands;

        public NarrativeOutputMapping(
            IEnumerable<Band> bands)
        {
            if (bands == null)
            {
                throw new ArgumentNullException(nameof(bands));
            }

            _bands = new List<Band>();

            foreach (Band band in bands)
            {
                if (band == null)
                {
                    throw new ArgumentException(
                        "The output mapping contains a null band.",
                        nameof(bands));
                }

                _bands.Add(band);
            }

            if (_bands.Count == 0)
            {
                throw new ArgumentException(
                    "An output mapping requires at least one band.",
                    nameof(bands));
            }

            _bands.Sort(
                (left, right) =>
                    left.MinimumInclusive.CompareTo(
                        right.MinimumInclusive));

            for (int index = 1;
                 index < _bands.Count;
                 index++)
            {
                if (_bands[index - 1].MinimumInclusive ==
                    _bands[index].MinimumInclusive)
                {
                    throw new ArgumentException(
                        $"More than one narrative outcome begins at " +
                        $"{_bands[index].MinimumInclusive}.",
                        nameof(bands));
                }
            }
        }

        public string Map(float output)
        {
            if (float.IsNaN(output) ||
                float.IsInfinity(output))
            {
                throw new ArgumentException(
                    "A narrative output must be finite.",
                    nameof(output));
            }

            Band selectedBand = null;

            foreach (Band band in _bands)
            {
                if (output < band.MinimumInclusive)
                    break;

                selectedBand = band;
            }

            if (selectedBand == null)
            {
                throw new InvalidOperationException(
                    $"No narrative-output band accepts value {output}.");
            }

            return selectedBand.OutcomeId;
        }
    }
}