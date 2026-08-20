using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class DialogueGraphAuditExporter
{
    [MenuItem("Tools/Dialogue System/Export All Dialogue Graphs")]
    private static void ExportAll()
    {
        string[] paths = AssetDatabase
            .GetAllAssetPaths()
            .Where(p => p.EndsWith(
                ".dialoguegraph",
                StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p)
            .ToArray();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("# Dialogue Graph Audit");
        sb.AppendLine();
        sb.AppendLine("Generated: " + DateTime.Now);
        sb.AppendLine("Graphs found: " + paths.Length);
        sb.AppendLine();

        foreach (string path in paths)
            ExportGraph(path, sb);

        string outputPath = Path.Combine(
            Application.dataPath,
            "../DialogueGraph_All_Report.md");

        File.WriteAllText(
            outputPath,
            sb.ToString(),
            Encoding.UTF8);

        Debug.Log(
            "[Dialogue Audit] Exported " +
            paths.Length +
            " dialogue graphs:\n" +
            outputPath);

        EditorUtility.RevealInFinder(outputPath);
    }

    private static void ExportGraph(
        string path,
        StringBuilder sb)
    {
        UnityEngine.Object asset =
            AssetDatabase.LoadMainAssetAtPath(path);

        sb.AppendLine("========================================");
        sb.AppendLine(
            "## " + Path.GetFileNameWithoutExtension(path));
        sb.AppendLine("========================================");
        sb.AppendLine();
        sb.AppendLine("Path: `" + path + "`");
        sb.AppendLine();

        if (asset == null)
        {
            sb.AppendLine("Could not load imported dialogue data.");
            sb.AppendLine();
            return;
        }

        SerializedObject serialized =
            new SerializedObject(asset);

        SerializedProperty entry =
            serialized.FindProperty("EntryNodeID");

        SerializedProperty nodes =
            serialized.FindProperty("AllNodes");

        if (nodes == null || !nodes.isArray)
        {
            sb.AppendLine(
                "Imported asset does not expose AllNodes.");
            sb.AppendLine(
                "Main asset type: " + asset.GetType().FullName);
            sb.AppendLine();
            return;
        }

        string entryID =
            entry != null
                ? entry.stringValue
                : string.Empty;

        Dictionary<string, string> readableIDs =
            BuildReadableIDs(nodes);

        sb.AppendLine(
            "Entry: " +
            ResolveID(entryID, readableIDs));

        sb.AppendLine();
        sb.AppendLine("### Nodes");
        sb.AppendLine();

        for (int i = 0; i < nodes.arraySize; i++)
        {
            SerializedProperty node =
                nodes.GetArrayElementAtIndex(i);

            string nodeID =
                GetString(node, "NodeID");

            string speaker =
                GetString(node, "SpeakerName");

            string dialogue =
                GetString(node, "DialogueText");

            string nextID =
                GetString(node, "NextID");

            SerializedProperty choices =
                node.FindPropertyRelative("Choices");

            string readable =
                readableIDs.TryGetValue(
                    nodeID,
                    out string id)
                    ? id
                    : "N??";

            bool hasChoices =
                choices != null &&
                choices.isArray &&
                choices.arraySize > 0;

            sb.AppendLine(
                "#### " +
                readable +
                (hasChoices
                    ? " - Choice"
                    : " - Dialogue"));

            sb.AppendLine();
            sb.AppendLine(
                "- Runtime Node ID: `" + nodeID + "`");

            sb.AppendLine(
                "- Speaker: " +
                FormatValue(speaker));

            sb.AppendLine(
                "- Dialogue: " +
                FormatValue(dialogue));

            if (!hasChoices)
            {
                sb.AppendLine(
                    "- Next: " +
                    ResolveID(nextID, readableIDs));
            }
            else
            {
                sb.AppendLine("- Choices:");

                for (int c = 0;
                     c < choices.arraySize;
                     c++)
                {
                    SerializedProperty choice =
                        choices.GetArrayElementAtIndex(c);

                    string text =
                        GetString(
                            choice,
                            "ChoiceText");

                    string fuzzyEvent =
                        GetString(
                            choice,
                            "FuzzyEventID");

                    string requiredBool =
                        GetString(
                            choice,
                            "ReqBoolKey");

                    string destination =
                        GetString(
                            choice,
                            "DestinationNodeID");

                    sb.AppendLine(
                        "  - Choice " + c + ": " +
                        FormatValue(text));

                    sb.AppendLine(
                        "    - Fuzzy Event ID: " +
                        FormatValue(fuzzyEvent));

                    sb.AppendLine(
                        "    - Required Bool Key: " +
                        FormatValue(requiredBool));

                    sb.AppendLine(
                        "    - Destination: " +
                        ResolveID(
                            destination,
                            readableIDs));
                }
            }

            sb.AppendLine();
        }

        sb.AppendLine("### Flow");
        sb.AppendLine();

        for (int i = 0; i < nodes.arraySize; i++)
        {
            SerializedProperty node =
                nodes.GetArrayElementAtIndex(i);

            string nodeID =
                GetString(node, "NodeID");

            string from =
                ResolveID(nodeID, readableIDs);

            SerializedProperty choices =
                node.FindPropertyRelative("Choices");

            bool hasChoices =
                choices != null &&
                choices.isArray &&
                choices.arraySize > 0;

            if (hasChoices)
            {
                for (int c = 0;
                     c < choices.arraySize;
                     c++)
                {
                    SerializedProperty choice =
                        choices.GetArrayElementAtIndex(c);

                    string text =
                        GetString(
                            choice,
                            "ChoiceText");

                    string destination =
                        GetString(
                            choice,
                            "DestinationNodeID");

                    sb.AppendLine(
                        "- " +
                        from +
                        " --[" +
                        text +
                        "]--> " +
                        ResolveID(
                            destination,
                            readableIDs));
                }
            }
            else
            {
                string nextID =
                    GetString(node, "NextID");

                if (!string.IsNullOrWhiteSpace(nextID))
                {
                    sb.AppendLine(
                        "- " +
                        from +
                        " --> " +
                        ResolveID(
                            nextID,
                            readableIDs));
                }
            }
        }

        sb.AppendLine();
        sb.AppendLine();
    }

    private static Dictionary<string, string>
        BuildReadableIDs(
            SerializedProperty nodes)
    {
        Dictionary<string, string> result =
            new Dictionary<string, string>();

        int dialogueIndex = 1;
        int choiceIndex = 1;

        for (int i = 0; i < nodes.arraySize; i++)
        {
            SerializedProperty node =
                nodes.GetArrayElementAtIndex(i);

            string nodeID =
                GetString(node, "NodeID");

            SerializedProperty choices =
                node.FindPropertyRelative("Choices");

            bool hasChoices =
                choices != null &&
                choices.isArray &&
                choices.arraySize > 0;

            string readable =
                hasChoices
                    ? "C" + choiceIndex++.ToString("00")
                    : "D" + dialogueIndex++.ToString("00");

            if (!string.IsNullOrWhiteSpace(nodeID))
                result[nodeID] = readable;
        }

        return result;
    }

    private static string GetString(
        SerializedProperty parent,
        string propertyName)
    {
        SerializedProperty property =
            parent.FindPropertyRelative(propertyName);

        return property != null
            ? property.stringValue
            : string.Empty;
    }

    private static string ResolveID(
        string runtimeID,
        Dictionary<string, string> ids)
    {
        if (string.IsNullOrWhiteSpace(runtimeID))
            return "<END>";

        if (ids.TryGetValue(
                runtimeID,
                out string readable))
            return readable;

        return "<END/EXTERNAL>";
    }

    private static string FormatValue(
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "<none>";

        value = value
            .Replace("\r", " ")
            .Replace("\n", " ");

        return "`" + value + "`";
    }
}