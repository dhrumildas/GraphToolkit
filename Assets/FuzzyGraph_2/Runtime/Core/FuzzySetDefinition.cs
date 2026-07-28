using System;

namespace FuzzyGraph2.Runtime
{
    public sealed class FuzzySetDefinition
    {
        private readonly float _first;
        private readonly float _second;
        private readonly float _third;
        private readonly float _fourth;

        public string Name { get; }

        public FuzzySetShape Shape { get; }

        private FuzzySetDefinition(
            string name,
            FuzzySetShape shape,
            float first,
            float second,
            float third,
            float fourth
        )
        {
            Name = name;
            Shape = shape;

            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public static FuzzySetDefinition High(string name, float start, float full)
        {
            ValidateName(name);
            FuzzyMembership.ValidateIncreasingBounds(start, full);

            return new FuzzySetDefinition(name.Trim(), FuzzySetShape.High, start, full, 0f, 0f);
        }

        public static FuzzySetDefinition Low(string name, float full, float end)
        {
            ValidateName(name);
            FuzzyMembership.ValidateIncreasingBounds(full, end);

            return new FuzzySetDefinition(name.Trim(), FuzzySetShape.Low, full, end, 0f, 0f);
        }
        public static FuzzySetDefinition Range(
            string name,
            float start,
            float fullStart,
            float fullEnd,
            float end
        )
        {
            ValidateName(name);

            FuzzyMembership.ValidateRangeBounds(start, fullStart, fullEnd, end);

            return new FuzzySetDefinition(
                name.Trim(),
                FuzzySetShape.Range,
                start,
                fullStart,
                fullEnd,
                end
            );
        }

        public float Evaluate(float value)
        {
            switch (Shape)
            {
                case FuzzySetShape.Low:
                    return FuzzyMembership.Low(value, full: _first, end: _second);

                case FuzzySetShape.Range:
                    return FuzzyMembership.Range(
                        value,
                        start: _first,
                        fullStart: _second,
                        fullEnd: _third,
                        end: _fourth
                    );

                case FuzzySetShape.High:
                    return FuzzyMembership.High(value, start: _first, full: _second);

                default:
                    throw new InvalidOperationException($"Unsupported fuzzy-set shape: {Shape}.");
            }
        }

        internal void ValidateWithinDomain(float minimum, float maximum)
        {
            switch (Shape)
            {
                case FuzzySetShape.Low:
                case FuzzySetShape.High:
                    ValidateBoundary(_first, minimum, maximum);
                    ValidateBoundary(_second, minimum, maximum);
                    break;

                case FuzzySetShape.Range:
                    ValidateBoundary(_first, minimum, maximum);
                    ValidateBoundary(_second, minimum, maximum);
                    ValidateBoundary(_third, minimum, maximum);
                    ValidateBoundary(_fourth, minimum, maximum);
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported fuzzy-set shape: {Shape}.");
            }
        }

        private void ValidateBoundary(float boundary, float minimum, float maximum)
        {
            if (boundary < minimum || boundary > maximum)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(boundary),
                    boundary,
                    $"Fuzzy set '{Name}' contains a boundary outside "
                        + $"the variable domain [{minimum}, {maximum}]."
                );
            }
        }
        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A fuzzy set must have a readable name.", nameof(name));
            }
        }
    }
}
