using System;
using FuzzyGraph.Runtime;

namespace FuzzyGraph2.Runtime
{
    public sealed class WorldStateQueryValueSource : IFuzzyValueSource
    {
        private readonly WorldStateQuery _query;

        public WorldStateQueryValueSource(WorldStateQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public bool TryGetBool(string variableId, out bool value)
        {
            value = false;

            if(string.IsNullOrWhiteSpace(variableId))
                return false;

            if(!_query.TryGet(variableId, out FuzzyValue fuzzyValue))
                return false;

            if(fuzzyValue.type != FuzzyValueType.Bool)
                return false;

            value = fuzzyValue.boolVal;
            return true;
        }

        public bool TryGetFloat(string variableId, out float value)
        {
            value = 0f;

            if (string.IsNullOrWhiteSpace(variableId))
                return false;

            if (!_query.TryGet(variableId, out FuzzyValue fuzzyValue))
            {
                return false;
            }

            switch (fuzzyValue.type)
            {
                case FuzzyValueType.Float:
                    value = fuzzyValue.floatVal;
                    return true;

                case FuzzyValueType.Int:
                    value = fuzzyValue.intVal;
                    return true;

                default:
                    return false;
            }
        }


    }
}
