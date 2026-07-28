using System;

namespace FuzzyGraph2.Runtime
{
    public sealed class FuzzyIsStatement
    {
        public FuzzyVariableDefinition Variable { get; }
        public FuzzySetDefinition Set { get; }

        public FuzzyIsStatement(FuzzyVariableDefinition variable, FuzzySetDefinition set)
        {
            Variable = variable ?? throw new ArgumentNullException(nameof(variable));

            Set = set ?? throw new ArgumentNullException(nameof(set));

            if (!variable.ContainsSet(set))
            {
                throw new ArgumentException(
                    $"Fuzzy set '{set.Name}' does not belong to variable '{variable.DisplayName}'.",
                    nameof(set)
                );
            }
        }

        public float Evaluate(float rawValue)
        {
            return Variable.Evaluate(Set, rawValue);
        }

        public override string ToString()
        {
            return $"{Variable.DisplayName} IS {Set.Name}";
        }
    }
}
