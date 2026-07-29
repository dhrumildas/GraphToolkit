using System;
using System.Collections.Generic;
namespace FuzzyGraph2.Runtime
{
    public sealed class FuzzyIsStatement : IFuzzyExpression
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

        //overload
        public float Evaluate(IFuzzyValueSource source)
        {
            if(source  == null)
                throw new ArgumentNullException(nameof(source));

            if(!source.TryGetFloat(Variable.Id, out float rawValue))
            {
                throw new KeyNotFoundException($"No numeric val found for fuzzy var {Variable.Id}");
            }
            return Evaluate(rawValue);
        }

        public override string ToString()
        {
            return $"{Variable.DisplayName} IS {Set.Name}";
        }
    }
}
