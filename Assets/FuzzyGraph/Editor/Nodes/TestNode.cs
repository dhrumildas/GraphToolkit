using Unity.GraphToolkit.Editor;
using System;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    [Serializable]
    public class TestNode : Node
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("In").Build();
            context.AddOutputPort("Out").Build();
        }
    }
}
