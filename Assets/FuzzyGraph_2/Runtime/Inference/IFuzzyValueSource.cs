namespace FuzzyGraph2.Runtime
{
    public interface IFuzzyValueSource
    {
        bool TryGetFloat(string variableId, out float value);

        bool TryGetBool(string variableId, out bool value);

        bool TryGetString(string variableId, out string value);
    }
}