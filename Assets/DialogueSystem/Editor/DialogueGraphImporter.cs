using UnityEngine;
using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

[ScriptedImporter(1, DialogueGraph.AssetExtension)]
public class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DialogueGraph editorGraph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);
        RuntimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();
        var nodeIDMap = new Dictionary<INode, string>();

        foreach(var node in editorGraph.GetNodes())
            nodeIDMap[node] = Guid.NewGuid().ToString();

        var startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if(startNode != null)
        {
            var entryPoint = startNode.GetOutputPorts().FirstOrDefault()?.FirstConnectedPort;

            if (entryPoint != null)
                runtimeGraph.EntryNodeID = nodeIDMap[entryPoint.GetNode()];
        }

        foreach(var iNode in editorGraph.GetNodes())
        {
            if(iNode is StartNode || iNode is EndNode)
                continue;

            var runtimeNode = new RuntimeDialogueNode { NodeID = nodeIDMap[iNode] };
            if(iNode is DialogueNode dialogueNode)
                ProcessDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            else if (iNode is ChoiceNode choiceNode)
                ProcessChoiceNode(choiceNode, runtimeNode, nodeIDMap);

            runtimeGraph.AllNodes.Add(runtimeNode);
        }

        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }

    private void ProcessDialogueNode(DialogueNode node, RuntimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var nextNodePort = node.GetOutputPortByName("out")?.FirstConnectedPort;
        if (nextNodePort != null)
            runtimeNode.NextID = nodeIDMap[nextNodePort.GetNode()];
    }

    private void ProcessChoiceNode(ChoiceNode node, RuntimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var choiceOutputPorts = node.GetOutputPorts().Where(p => p.Name.StartsWith("Choice "));
        foreach (var outputPort in choiceOutputPorts)
        {
            var index = outputPort.Name.Substring("Choice ".Length);
            var textPort = node.GetInputPortByName($"Choice Text {index}");

            var choiceData = new ChoiceData
            {
                ChoiceText = GetPortValue<string>(textPort),
                DestinationNodeID = outputPort.FirstConnectedPort != null ? nodeIDMap[outputPort.FirstConnectedPort.GetNode()] : null,
            };

            runtimeNode.Choices.Add(choiceData);
        }
    }

    private T GetPortValue<T>(IPort port)
    {
        if (port == null)
            return default;
        if(port.IsConnected)
        {
            if(port.FirstConnectedPort.GetNode() is IVariableNode variableNode)
            {
                variableNode.Variable.TryGetDefaultValue(out T value);
                return value;
            }
        }

        port.TryGetValue(out T fallbackValue);
        return fallbackValue;
    }
}
