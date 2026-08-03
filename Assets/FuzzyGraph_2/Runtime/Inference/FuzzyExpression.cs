using System;
using System.Collections.Generic;
namespace FuzzyGraph2.Runtime
{

    public interface IFuzzyExpression
    {
        float Evaluate(IFuzzyValueSource source);
    }

    public static class FuzzyExpression
    {
        public static IFuzzyExpression BoolEquals(string varID, bool expectedVal)
        {
            if(string.IsNullOrWhiteSpace(varID))
                throw new ArgumentException("Bool expression is required for varID.",nameof(varID));

            return new BoolEqualsExpression(varID.Trim(), expectedVal);
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

        private sealed class BoolEqualsExpression : IFuzzyExpression
        {
            private readonly string _variableId;
            private readonly bool _expectedValue;

            public BoolEqualsExpression(string variableId,bool expectedValue)
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

                if (!source.TryGetBool(_variableId,out bool actualValue))
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