using System;
using System.Collections.Generic;

namespace FuzzyGraph2.Runtime
{

    public sealed class FuzzyVariableDefinition
    {
        private readonly List<FuzzySetDefinition> _sets;
        private readonly Dictionary<string, FuzzySetDefinition> _setsByName;
        private readonly HashSet<FuzzySetDefinition> _setLookup;
        public string Id { get; }
        public string DisplayName { get; }
        public float Minimum { get; }
        public float Maximum { get; }
        public IReadOnlyList<FuzzySetDefinition> Sets => _sets;

        public FuzzyVariableDefinition(
            string id,
            string displayName,
            float minimum,
            float maximum,
            IEnumerable<FuzzySetDefinition> sets)
        {
            ValidateId(id);
            ValidateDisplayName(displayName);
            ValidateDomain(minimum, maximum);

            if (sets == null)
            {
                throw new ArgumentNullException(nameof(sets));
            }

            Id = id.Trim();
            DisplayName = displayName.Trim();
            Minimum = minimum;
            Maximum = maximum;

            _sets = new List<FuzzySetDefinition>();
            _setsByName = new Dictionary<string, FuzzySetDefinition>(StringComparer.OrdinalIgnoreCase);
            _setLookup = new HashSet<FuzzySetDefinition>();

            foreach (FuzzySetDefinition set in sets)
            {
                if (set == null)
                {
                    throw new ArgumentException("A fuzzy variable cannot contain a null set.",nameof(sets));
                }

                set.ValidateWithinDomain(minimum, maximum);

                if (_setsByName.ContainsKey(set.Name))
                {
                    throw new ArgumentException(
                        $"The fuzzy variable '{DisplayName}' contains " +
                        $"more than one set named '{set.Name}'.",
                        nameof(sets));
                }

                _sets.Add(set);
                _setsByName.Add(set.Name, set);
                _setLookup.Add(set);
            }

            if (_sets.Count == 0)
            {
                throw new ArgumentException("A fuzzy variable must contain at least one set.",nameof(sets));
            }
        }

        public FuzzySetDefinition GetSet(string setName)
        {
            if (string.IsNullOrWhiteSpace(setName))
            {
                throw new ArgumentException(
                    "A fuzzy set name is required.",
                    nameof(setName));
            }

            if (!_setsByName.TryGetValue(
                    setName.Trim(),
                    out FuzzySetDefinition set))
            {
                throw new KeyNotFoundException(
                    $"Variable '{DisplayName}' does not contain " +
                    $"a fuzzy set named '{setName}'.");
            }

            return set;
        }

        internal bool ContainsSet(FuzzySetDefinition set)
        {
            return set != null && _setLookup.Contains(set);
        }

        public float Evaluate(
            FuzzySetDefinition set,
            float rawValue)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            if (!ContainsSet(set))
            {
                throw new ArgumentException(
                    $"Fuzzy set '{set.Name}' does not belong to " +
                    $"variable '{DisplayName}'.",
                    nameof(set));
            }

            float domainValue = ClampToDomain(rawValue);

            return set.Evaluate(domainValue);
        }

        public float ClampToDomain(float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                throw new ArgumentException("A fuzzy variable cannot evaluate NaN or infinity.",nameof(value));
            }

            if (value < Minimum)
                return Minimum;

            if (value > Maximum)
                return Maximum;

            return value;
        }

        private static void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "A fuzzy variable must have a stable ID.",
                    nameof(id));
            }

            foreach (char character in id)
            {
                if (char.IsWhiteSpace(character))
                {
                    throw new ArgumentException(
                        "A fuzzy variable ID cannot contain whitespace.",
                        nameof(id));
                }
            }
        }

        private static void ValidateDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentException(
                    "A fuzzy variable must have a readable name.",
                    nameof(displayName));
            }
        }

        private static void ValidateDomain(float minimum,float maximum)
        {
            if (float.IsNaN(minimum) ||
                float.IsInfinity(minimum) ||
                float.IsNaN(maximum) ||
                float.IsInfinity(maximum))
            {
                throw new ArgumentException("A fuzzy variable domain must use finite values.");
            }

            if (maximum <= minimum)
            {
                throw new ArgumentException($"The maximum domain value ({maximum}) must be greater than the minimum ({minimum}).");
            }
        }
    }
}