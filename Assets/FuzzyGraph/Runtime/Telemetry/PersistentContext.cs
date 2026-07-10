using System.Collections.Generic;

namespace FuzzyGraph.Runtime
{
    public class PersistentContext
    // stores narrative facts that should remain available for later evaluations
    {
        private readonly Dictionary<string, FuzzyValue> values = new();

        public void Set(string key, FuzzyValue val)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            values[key] = val;
        }

        public bool TryGet(string key, out FuzzyValue val)
        {
            return values.TryGetValue(key, out val);
        }

        public bool Contains(string key)
        {
            return values.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return values.Remove(key);
        }

        public void Clear()
        {
            values.Clear();
        }

        public IReadOnlyDictionary<string, FuzzyValue> Values => values;
    }
}