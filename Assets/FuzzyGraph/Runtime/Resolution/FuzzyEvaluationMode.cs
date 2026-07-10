namespace FuzzyGraph.Runtime
{
    public enum FuzzyEvaluationMode
    {
        Crisp,      //normal true false comparison
        FuzzyLow,   //lower values for stronger memberships
        FuzzyHigh,  //higher values for stronger memberships
        FuzzyRange  //preferred range produces stronger memberships
    }
}