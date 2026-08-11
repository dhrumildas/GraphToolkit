using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEngine;

namespace FuzzyGraph2.Editor
{
    // Small editor-only dump of the graph as it is currently wired.
    public static class FG2GraphExporter
    {
        private const string MenuPath = "Tools/FuzzyGraph2/Export Selected Graph Report";

        [MenuItem(MenuPath)]
        private static void Export()
        {
            string assetPath = GetSelectedGraphPath();

            if (string.IsNullOrEmpty(assetPath))
            {
                EditorUtility.DisplayDialog(
                    "FuzzyGraph2 Export",
                    "Select a .fuzzygraph2 file in the Project window first.",
                    "OK");
                return;
            }

            FuzzyGraph2Asset graph = GraphDatabase.LoadGraph<FuzzyGraph2Asset>(assetPath);

            if (graph == null)
            {
                EditorUtility.DisplayDialog(
                    "FuzzyGraph2 Export",
                    "Could not load the selected graph.",
                    "OK");
                return;
            }

            List<Node> nodes = graph.GetNodes().OfType<Node>().ToList();
            Dictionary<Node, string> ids = MakeNodeIds(nodes);
            List<FG2ExportLink> links = ReadLinks(nodes, ids);

            FG2ExportReport report = new FG2ExportReport
            {
                graphName = Path.GetFileNameWithoutExtension(assetPath),
                assetPath = assetPath,
                exportedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                compileCheck = GetCompileCheck(graph)
            };

            foreach (Node node in nodes)
            {
                report.nodes.Add(new FG2ExportNode
                {
                    id = ids[node],
                    type = node.GetType().Name,
                    data = DescribeNode(node)
                });
            }

            report.links = links;

            string folder = Path.GetDirectoryName(assetPath)?.Replace("\\", "/") + "/Exports";
            Directory.CreateDirectory(folder);

            string baseName = Path.GetFileNameWithoutExtension(assetPath) + "_GraphReport";
            string mdPath = $"{folder}/{baseName}.md";
            string jsonPath = $"{folder}/{baseName}.json";

            File.WriteAllText(mdPath, BuildMarkdown(report, graph, ids), Encoding.UTF8);
            File.WriteAllText(jsonPath, JsonUtility.ToJson(report, true), Encoding.UTF8);

            AssetDatabase.Refresh();

            TextAsset markdownAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(mdPath);
            if (markdownAsset != null)
            {
                Selection.activeObject = markdownAsset;
                EditorGUIUtility.PingObject(markdownAsset);
            }

            Debug.Log(
                $"[FuzzyGraph2] graph export complete\n" +
                $"{mdPath}\n{jsonPath}");
        }

        [MenuItem(MenuPath, true)]
        private static bool CanExport()
        {
            return !string.IsNullOrEmpty(GetSelectedGraphPath());
        }

        private static string GetSelectedGraphPath()
        {
            if (Selection.activeObject == null)
                return string.Empty;

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            return path.EndsWith(
                "." + FuzzyGraph2Asset.AssetExtension,
                StringComparison.OrdinalIgnoreCase)
                    ? path
                    : string.Empty;
        }

        private static string GetCompileCheck(FuzzyGraph2Asset graph)
        {
            RuntimeFuzzyGraph2 runtime = null;

            try
            {
                runtime = FG2Compiler.Compile(
                    graph,
                    out FuzzyGraph2CompileReport report);

                return report.ToLogLine("export check");
            }
            catch (Exception e)
            {
                return "Compile failed: " + e.Message;
            }
            finally
            {
                if (runtime != null)
                    UnityEngine.Object.DestroyImmediate(runtime);
            }
        }

        private static Dictionary<Node, string> MakeNodeIds(List<Node> nodes)
        {
            Dictionary<Node, string> ids = new Dictionary<Node, string>();

            int e = 1;
            int r = 1;
            int c = 1;
            int o = 1;
            int w = 1;
            int n = 1;

            foreach (Node node in nodes)
            {
                if (node is EventNode) ids[node] = $"E{e++:00}";
                else if (node is RuleNode) ids[node] = $"R{r++:00}";
                else if (node is CriterionNode) ids[node] = $"C{c++:00}";
                else if (node is ConsequenceNode) ids[node] = $"O{o++:00}";
                else if (node is WriteBackNode) ids[node] = $"W{w++:00}";
                else ids[node] = $"N{n++:00}";
            }

            return ids;
        }

        private static List<FG2ExportLink> ReadLinks(
            List<Node> nodes,
            Dictionary<Node, string> ids)
        {
            List<FG2ExportLink> links = new List<FG2ExportLink>();

            foreach (Node fromNode in nodes)
            {
                foreach (IPort fromPort in fromNode.GetOutputPorts())
                {
                    List<IPort> connected = new List<IPort>();
                    fromPort.GetConnectedPorts(connected);

                    foreach (IPort toPort in connected)
                    {
                        INode connectedNode = toPort.GetNode();

                        if (!(connectedNode is Node toNode))
                            continue;

                        if (!ids.ContainsKey(toNode))
                            continue;

                        links.Add(new FG2ExportLink
                        {
                            fromNode = ids[fromNode],
                            fromPort = fromPort.Name,
                            toNode = ids[toNode],
                            toPort = toPort.Name
                        });
                    }
                }
            }

            return links;
        }

        private static List<string> DescribeNode(Node node)
        {
            List<string> data = new List<string>();

            if (node is EventNode eventNode)
            {
                Add(data, "Event ID", Opt(eventNode, EventNode.EventIdOptionName, ""));
                return data;
            }

            if (node is RuleNode ruleNode)
            {
                Add(data, "Rule ID", Opt(ruleNode, RuleNode.RuleIdOptionName, ""));
                Add(data, "Consequent", F(Opt(ruleNode, RuleNode.ConsequentOptionName, 0f)));
                return data;
            }

            if (node is CriterionNode criterion)
            {
                CriterionMode mode = Opt(
                    criterion,
                    CriterionNode.ModeOptionName,
                    CriterionMode.FuzzyNumber);

                Add(data, "Mode", mode.ToString());

                if (mode == CriterionMode.And ||
                    mode == CriterionMode.Or ||
                    mode == CriterionMode.Not)
                {
                    return data;
                }

                Add(data, "Variable ID",
                    Opt(criterion, CriterionNode.VariableIdOptionName, ""));

                if (mode == CriterionMode.FuzzyNumber)
                {
                    FuzzySetShape shape = Opt(
                        criterion,
                        CriterionNode.ShapeOptionName,
                        FuzzySetShape.Low);

                    Add(data, "Set",
                        Opt(criterion, CriterionNode.SetNameOptionName, ""));

                    Add(data, "Shape", shape.ToString());
                    Add(data, "Minimum",
                        F(Opt(criterion, CriterionNode.MinimumOptionName, 0f)));
                    Add(data, "Maximum",
                        F(Opt(criterion, CriterionNode.MaximumOptionName, 1f)));
                    Add(data, "Point A",
                        F(Opt(criterion, CriterionNode.FirstOptionName, 0f)));
                    Add(data, "Point B",
                        F(Opt(criterion, CriterionNode.SecondOptionName, 0f)));

                    if (shape == FuzzySetShape.Range)
                    {
                        Add(data, "Point C",
                            F(Opt(criterion, CriterionNode.ThirdOptionName, 0f)));
                        Add(data, "Point D",
                            F(Opt(criterion, CriterionNode.FourthOptionName, 0f)));
                    }
                }
                else if (mode == CriterionMode.BoolEquals)
                {
                    Add(data, "Expected Bool",
                        Opt(criterion, CriterionNode.ExpectedBoolOptionName, true).ToString());
                }
                else if (mode == CriterionMode.IdEquals)
                {
                    Add(data, "Expected ID",
                        Opt(criterion, CriterionNode.ExpectedIdOptionName, ""));
                }
                else if (mode == CriterionMode.NumberCompare)
                {
                    NumberComparison comparison = Opt(
                        criterion,
                        CriterionNode.ComparisonOptionName,
                        NumberComparison.LessThanOrEqual);

                    Add(data, "Comparison", comparison.ToString());
                    Add(data, "Compare A",
                        F(Opt(criterion, CriterionNode.ComparisonValueOptionName, 0f)));

                    if (comparison == NumberComparison.InclusiveRange)
                    {
                        Add(data, "Compare B",
                            F(Opt(criterion, CriterionNode.ComparisonValue2OptionName, 0f)));
                    }
                }

                return data;
            }

            if (node is ConsequenceNode consequence)
            {
                bool fireEvent = Opt(
                    consequence,
                    ConsequenceNode.RunActionOptionName,
                    true);

                Add(data, "Outcome ID",
                    Opt(consequence, ConsequenceNode.OutcomeIdOptionName, ""));
                Add(data, "Minimum",
                    F(Opt(consequence, ConsequenceNode.MinimumOptionName, 0f)));
                Add(data, "Fallback",
                    Opt(consequence, ConsequenceNode.FallbackOptionName, false).ToString());
                Add(data, "Fire Event", fireEvent.ToString());

                if (fireEvent)
                {
                    Add(data, "Target",
                        Opt(consequence, ConsequenceNode.TargetOptionName, ""));
                    Add(data, "Payload",
                        Opt(consequence, ConsequenceNode.PayloadOptionName, ""));
                }

                return data;
            }

            if (node is WriteBackNode writeBack)
            {
                WriteBackOperation operation = Opt(
                    writeBack,
                    WriteBackNode.OperationOptionName,
                    WriteBackOperation.Set);

                FuzzyValueType valueType = Opt(
                    writeBack,
                    WriteBackNode.ValueTypeOptionName,
                    FuzzyValueType.Bool);

                Add(data, "Target Key",
                    Opt(writeBack, WriteBackNode.TargetKeyOptionName, ""));
                Add(data, "Operation", operation.ToString());
                Add(data, "Value Type", valueType.ToString());

                if (operation != WriteBackOperation.Toggle)
                {
                    if (valueType == FuzzyValueType.Bool)
                        Add(data, "Value",
                            Opt(writeBack, WriteBackNode.BoolValueOptionName, false).ToString());

                    else if (valueType == FuzzyValueType.Int)
                        Add(data, "Value",
                            Opt(writeBack, WriteBackNode.IntValueOptionName, 0).ToString());

                    else if (valueType == FuzzyValueType.Float)
                        Add(data, "Value",
                            F(Opt(writeBack, WriteBackNode.FloatValueOptionName, 0f)));

                    else if (valueType == FuzzyValueType.String)
                        Add(data, "Value",
                            Opt(writeBack, WriteBackNode.StringValueOptionName, ""));
                }
            }

            return data;
        }

        private static string BuildMarkdown(
            FG2ExportReport report,
            FuzzyGraph2Asset graph,
            Dictionary<Node, string> ids)
        {
            StringBuilder md = new StringBuilder();

            md.AppendLine($"# {report.graphName} - FuzzyGraph2 graph report");
            md.AppendLine();
            md.AppendLine($"Source: `{report.assetPath}`");
            md.AppendLine($"Exported: {report.exportedAt}");
            md.AppendLine();
            md.AppendLine("## Compile check");
            md.AppendLine();
            md.AppendLine($"`{report.compileCheck}`");
            md.AppendLine();

            md.AppendLine("## Event overview");
            md.AppendLine();

            foreach (EventNode eventNode in graph.GetNodes().OfType<EventNode>())
            {
                string eventId = Opt(
                    eventNode,
                    EventNode.EventIdOptionName,
                    "(missing)");

                md.AppendLine($"### {eventId} ({ids[eventNode]})");
                md.AppendLine();

                List<RuleNode> rules =
                    ConnectedFromOutput<RuleNode>(
                        eventNode,
                        EventNode.RulesPortName).ToList();

                md.AppendLine("Rules:");
                if (rules.Count == 0)
                {
                    md.AppendLine("- none");
                }
                else
                {
                    foreach (RuleNode rule in rules)
                    {
                        string ruleId = Opt(
                            rule,
                            RuleNode.RuleIdOptionName,
                            "(missing)");

                        float consequent = Opt(
                            rule,
                            RuleNode.ConsequentOptionName,
                            0f);

                        List<CriterionNode> roots =
                            ConnectedFromInput<CriterionNode>(
                                rule,
                                RuleNode.CriteriaPortName).ToList();

                        string rootText = roots.Count == 0
                            ? "no criterion"
                            : string.Join(", ", roots.Select(x => ids[x]));

                        md.AppendLine(
                            $"- `{ids[rule]}` {ruleId} | z={F(consequent)} | root {rootText}");
                    }
                }

                md.AppendLine();
                md.AppendLine("Consequences:");

                List<ConsequenceNode> outcomes =
                    ConnectedFromOutput<ConsequenceNode>(
                        eventNode,
                        EventNode.ConsequencesPortName).ToList();

                if (outcomes.Count == 0)
                {
                    md.AppendLine("- none");
                }
                else
                {
                    foreach (ConsequenceNode outcome in outcomes)
                    {
                        string outcomeId = Opt(
                            outcome,
                            ConsequenceNode.OutcomeIdOptionName,
                            "(missing)");

                        float minimum = Opt(
                            outcome,
                            ConsequenceNode.MinimumOptionName,
                            0f);

                        bool fallback = Opt(
                            outcome,
                            ConsequenceNode.FallbackOptionName,
                            false);

                        md.AppendLine(
                            $"- `{ids[outcome]}` {outcomeId} | min={F(minimum)} | fallback={fallback}");

                        foreach (WriteBackNode writeBack in
                                 ConnectedFromOutput<WriteBackNode>(
                                     outcome,
                                     ConsequenceNode.WriteBacksPortName))
                        {
                            md.AppendLine(
                                $"  - write-back `{ids[writeBack]}`");
                        }
                    }
                }

                md.AppendLine();
            }

            md.AppendLine("## Every node");
            md.AppendLine();

            foreach (FG2ExportNode node in report.nodes)
            {
                md.AppendLine($"### {node.id} - {node.type}");
                md.AppendLine();

                if (node.data.Count == 0)
                {
                    md.AppendLine("- no stored data");
                }
                else
                {
                    foreach (string line in node.data)
                        md.AppendLine("- " + line);
                }

                md.AppendLine();
            }

            md.AppendLine("## Every connection");
            md.AppendLine();

            foreach (FG2ExportLink link in report.links)
            {
                md.AppendLine(
                    $"- `{link.fromNode}.{link.fromPort}` -> " +
                    $"`{link.toNode}.{link.toPort}`");
            }

            return md.ToString();
        }

        private static IEnumerable<T> ConnectedFromOutput<T>(
            Node node,
            string portName) where T : Node
        {
            return Connected<T>(node.GetOutputPortByName(portName));
        }

        private static IEnumerable<T> ConnectedFromInput<T>(
            Node node,
            string portName) where T : Node
        {
            return Connected<T>(node.GetInputPortByName(portName));
        }

        private static IEnumerable<T> Connected<T>(IPort port) where T : Node
        {
            if (port == null)
                return Enumerable.Empty<T>();

            List<IPort> ports = new List<IPort>();
            port.GetConnectedPorts(ports);

            return ports.Select(x => x.GetNode()).OfType<T>();
        }

        private static T Opt<T>(Node node, string optionName, T fallback)
        {
            INodeOption option = node.GetNodeOptionByName(optionName);

            if (option != null && option.TryGetValue<T>(out T value))
                return value;

            return fallback;
        }

        private static void Add(List<string> list, string name, string value)
        {
            list.Add($"{name}: {value}");
        }

        private static string F(float value)
        {
            return value.ToString("0.####");
        }
    }

    [Serializable]
    public sealed class FG2ExportReport
    {
        public string graphName;
        public string assetPath;
        public string exportedAt;
        public string compileCheck;

        public List<FG2ExportNode> nodes = new List<FG2ExportNode>();
        public List<FG2ExportLink> links = new List<FG2ExportLink>();
    }

    [Serializable]
    public sealed class FG2ExportNode
    {
        public string id;
        public string type;
        public List<string> data = new List<string>();
    }

    [Serializable]
    public sealed class FG2ExportLink
    {
        public string fromNode;
        public string fromPort;
        public string toNode;
        public string toPort;
    }
}
