using System;
using System.Collections.Generic;
namespace FuzzyGraph2.Runtime
{

    public interface IFuzzyExpression
    {
        float Evaluate(IFuzzyValueSource source);
    }

    public enum NumberComparison
    {
        LessThan,
        LessThanOrEqual,
        Equal,
        GreaterThanOrEqual,
        GreaterThan,
        InclusiveRange
    }

    public static class FuzzyExpression
    {
        public static IFuzzyExpression BoolEquals(string varID, bool expectedVal)
        {
            if (string.IsNullOrWhiteSpace(varID))
                throw new ArgumentException("Bool expression is required for varID.", nameof(varID));

            return new BoolEqualsExpression(varID.Trim(), expectedVal);
        }

        public static IFuzzyExpression Exists(string variableId)
        {
            if (string.IsNullOrWhiteSpace(variableId))
            {
                throw new ArgumentException(
                    "an existence expression needs a variable id",
                    nameof(variableId));
            }

            return new ExistsExpression(variableId.Trim(), shouldExist: true);
        }

        public static IFuzzyExpression DoesNotExist(string variableId)
        {
            if (string.IsNullOrWhiteSpace(variableId))
            {
                throw new ArgumentException(
                    "an existence expression needs a variable id",
                    nameof(variableId));
            }

            return new ExistsExpression(variableId.Trim(), shouldExist: false);
        }

        public static IFuzzyExpression IdEquals(
    string variableId,
    string expectedId)
        {
            if (string.IsNullOrWhiteSpace(variableId))
            {
                throw new ArgumentException(
                    "an id expression needs a variable id",
                    nameof(variableId));
            }

            if (string.IsNullOrWhiteSpace(expectedId))
            {
                throw new ArgumentException(
                    "an id expression needs an expected id",
                    nameof(expectedId));
            }

            return new IdEqualsExpression(
                variableId.Trim(),
                expectedId.Trim());
        }

        public static IFuzzyExpression NumberCompare(
            string variableId,
            NumberComparison comparison,
            float valueA,
            float valueB = 0f)
        {
            if (string.IsNullOrWhiteSpace(variableId))
            {
                throw new ArgumentException(
                    "a number expression needs a variable id",
                    nameof(variableId));
            }

            return new NumberCompareExpression(
                variableId.Trim(),
                comparison,
                valueA,
                valueB);
        }

        public static IFuzzyExpression And(
            params IFuzzyExpression[] expressions)
        {
            ValidateMultipleExpressions(expressions, "AND");

            return new AndExpression(expressions);
        }

        public static IFuzzyExpression Or(
            params IFuzzyExpression[] expressions)
        {
            ValidateMultipleExpressions(expressions, "OR");

            return new OrExpression(expressions);
        }

        public static IFuzzyExpression Not(
            IFuzzyExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return new NotExpression(expression);
        }

        private static void ValidateMultipleExpressions(
            IFuzzyExpression[] expressions,
            string operatorName)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }

            if (expressions.Length < 2)
            {
                throw new ArgumentException(
                    $"{operatorName} requires at least two expressions.",
                    nameof(expressions));
            }

            for (int index = 0; index < expressions.Length; index++)
            {
                if (expressions[index] == null)
                {
                    throw new ArgumentException(
                        $"{operatorName} contains a null expression " +
                        $"at index {index}.",
                        nameof(expressions));
                }
            }
        }

        private sealed class AndExpression : IFuzzyExpression
        {
            private readonly IFuzzyExpression[] _expressions;

            public AndExpression(IFuzzyExpression[] expressions)
            {
                _expressions =
                    (IFuzzyExpression[])expressions.Clone();
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                float result = 1f;

                foreach (IFuzzyExpression expression in _expressions)
                {
                    result = FuzzyMath.AND(
                        result,
                        expression.Evaluate(source));

                    if (result <= 0f)
                        return 0f;
                }

                return result;
            }

            public override string ToString()
            {
                return JoinExpressions(" AND ", _expressions);
            }
        }

        private sealed class OrExpression : IFuzzyExpression
        {
            private readonly IFuzzyExpression[] _expressions;

            public OrExpression(IFuzzyExpression[] expressions)
            {
                _expressions =
                    (IFuzzyExpression[])expressions.Clone();
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                float result = 0f;

                foreach (IFuzzyExpression expression in _expressions)
                {
                    result = FuzzyMath.OR(
                        result,
                        expression.Evaluate(source));

                    if (result >= 1f)
                        return 1f;
                }

                return result;
            }

            public override string ToString()
            {
                return JoinExpressions(" OR ", _expressions);
            }
        }

        private sealed class NotExpression : IFuzzyExpression
        {
            private readonly IFuzzyExpression _expression;

            public NotExpression(IFuzzyExpression expression)
            {
                _expression = expression;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                return FuzzyMath.NOT(
                    _expression.Evaluate(source));
            }

            public override string ToString()
            {
                return $"NOT ({_expression})";
            }
        }

        private sealed class ExistsExpression : IFuzzyExpression
        {
            private readonly string _variableId;
            private readonly bool _shouldExist;

            public ExistsExpression(string variableId, bool shouldExist)
            {
                _variableId = variableId;
                _shouldExist = shouldExist;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                bool exists =
                    source.TryGetBool(_variableId, out _) ||
                    source.TryGetFloat(_variableId, out _) ||
                    source.TryGetString(_variableId, out _);

                return exists == _shouldExist ? 1f : 0f;
            }

            public override string ToString()
            {
                return _shouldExist
                    ? $"{_variableId} EXISTS"
                    : $"{_variableId} DOES NOT EXIST";
            }
        }

        private sealed class BoolEqualsExpression : IFuzzyExpression
        {
            private readonly string _variableId;
            private readonly bool _expectedValue;

            public BoolEqualsExpression(string variableId, bool expectedValue)
            {
                _variableId = variableId;
                _expectedValue = expectedValue;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (!source.TryGetBool(_variableId, out bool actualValue))
                {
                    throw new KeyNotFoundException($"No Boolean value was found for '{_variableId}'.");
                }

                return actualValue == _expectedValue ? 1f : 0f;
            }

            public override string ToString()
            {
                string expectedText = _expectedValue ? "TRUE" : "FALSE";

                return $"{_variableId} IS {expectedText}";
            }
        }

        private sealed class IdEqualsExpression : IFuzzyExpression
        {
            private readonly string _variableId;
            private readonly string _expectedId;

            public IdEqualsExpression(string variableId, string expectedId)
            {
                _variableId = variableId;
                _expectedId = expectedId;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));

                if (!source.TryGetString(_variableId, out string actualId))
                {
                    throw new KeyNotFoundException($"no id value was found for '{_variableId}'");
                }

                return string.Equals(actualId, _expectedId, StringComparison.Ordinal) ? 1f : 0f;
            }

            public override string ToString()
            {
                return $"{_variableId} IS {_expectedId}";
            }
        }

        private sealed class NumberCompareExpression : IFuzzyExpression
        {
            private readonly string _variableId;
            private readonly NumberComparison _comparison;
            private readonly float _valueA;
            private readonly float _valueB;

            public NumberCompareExpression(
                string variableId,
                NumberComparison comparison,
                float valueA,
                float valueB
            )
            {
                _variableId = variableId;
                _comparison = comparison;
                _valueA = valueA;
                _valueB = valueB;
            }

            public float Evaluate(IFuzzyValueSource source)
            {
                if (source == null)
                    throw new ArgumentNullException(nameof(source));

                if (!source.TryGetFloat(_variableId, out float actualValue))
                {
                    throw new KeyNotFoundException($"no number value was found for '{_variableId}'");
                }

                bool matched;

                switch (_comparison)
                {
                    case NumberComparison.LessThan:
                        matched = actualValue < _valueA;
                        break;

                    case NumberComparison.LessThanOrEqual:
                        matched = actualValue <= _valueA;
                        break;

                    case NumberComparison.Equal:
                        matched = Math.Abs(actualValue - _valueA) <= 0.0001f;
                        break;

                    case NumberComparison.GreaterThanOrEqual:
                        matched = actualValue >= _valueA;
                        break;

                    case NumberComparison.GreaterThan:
                        matched = actualValue > _valueA;
                        break;

                    case NumberComparison.InclusiveRange:
                        {
                            float lower = _valueA <= _valueB ? _valueA : _valueB;

                            float upper = _valueA <= _valueB ? _valueB : _valueA;

                            matched = actualValue >= lower && actualValue <= upper;
                            break;
                        }

                    default:
                        throw new InvalidOperationException(
                            $"unsupported number comparison: {_comparison}"
                        );
                }

                return matched ? 1f : 0f;
            }

            public override string ToString()
            {
                if (_comparison == NumberComparison.InclusiveRange)
                {
                    return $"{_variableId} IN " + $"[{_valueA}, {_valueB}]";
                }

                return $"{_variableId} {_comparison} {_valueA}";
            }
        }


        private static string JoinExpressions(
            string separator,
            IFuzzyExpression[] expressions)
        {
            string[] descriptions =
                new string[expressions.Length];

            for (int index = 0;
                 index < expressions.Length;
                 index++)
            {
                descriptions[index] =
                    expressions[index].ToString();
            }

            return $"({string.Join(separator, descriptions)})";
        }
    }
}