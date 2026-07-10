using System;
namespace FuzzyGraph.Runtime
{
    [Serializable]
    public class RuntimeWriteBack
    {
        public string targetKey;
        public WriteBackOperation operation;
        public FuzzyValue val;
    }
}