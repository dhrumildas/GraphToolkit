using System;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [UseWithGraph(typeof(OptionSandboxGraph))]
    public sealed class OptionTestContextNode : ContextNode
    {
    }
}