using System.Collections.Generic;
namespace FuzzyGraph.Runtime
{
    public class QueryBuilder
    {
        private readonly PersistentContext persistentContext;
        private readonly Dictionary<string, FuzzyValue> liveValues = new();
        
        public QueryBuilder(PersistentContext persistentContext)
        {
            this.persistentContext = persistentContext;
        }

        public void SetLive(string key, FuzzyValue value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            liveValues[key] = value;
        }

        public bool RemoveLive(string key)
        {
            return liveValues.Remove(key);
        }

        public void ClearLive()
        {
            liveValues.Clear();
        }

        public WorldStateQuery Build()
        {
            WorldStateQuery query  = new WorldStateQuery();

            //persistent narrative memory piece added first
            if(persistentContext != null)
            {
                foreach(KeyValuePair<string, FuzzyValue> pair in persistentContext.Values)
                {
                    query.Set(pair.Key, pair.Value);
                }
            }

            //current live game scene is added next
            //live values override persistent value with the same key

            foreach(KeyValuePair<string, FuzzyValue> pair in liveValues)
            {
                query.Set(pair.Key, pair.Value);
            }
            return query;
        }
    }
}