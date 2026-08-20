using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class FG2SceneBindingAudit
{
    [MenuItem("Tools/FuzzyGraph2/Export Scene Bindings")]
    private static void ExportSceneBindings()
    {
        Scene scene = SceneManager.GetActiveScene();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("FUZZYGRAPH2 SCENE BINDING AUDIT");
        sb.AppendLine($"Scene: {scene.name}");
        sb.AppendLine($"Generated: {DateTime.Now}");
        sb.AppendLine();

        MonoBehaviour[] behaviours =
            UnityEngine.Object.FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include);

        DumpEventInvokers(sb, behaviours);
        DumpSignalRouters(sb, behaviours);
        DumpInteractables(sb, behaviours);

        string path = Path.Combine(
            Application.dataPath,
            $"../FG2_Bindings_{scene.name}.txt");

        File.WriteAllText(path, sb.ToString());

        Debug.Log($"[FG2 Audit] Exported:\n{path}");

        EditorUtility.RevealInFinder(path);
    }

    private static void DumpEventInvokers(
        StringBuilder sb,
        MonoBehaviour[] behaviours)
    {
        sb.AppendLine("========================================");
        sb.AppendLine("EVENT INVOKERS");
        sb.AppendLine("========================================");

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour == null ||
                behaviour.GetType().Name != "EventInvoker")
                continue;

            SerializedObject serialized =
                new SerializedObject(behaviour);

            SerializedProperty eventID =
                serialized.FindProperty("eventID");

            SerializedProperty source =
                serialized.FindProperty("consequenceSource");

            sb.AppendLine();
            sb.AppendLine(
                $"GameObject: {GetPath(behaviour.transform)}");

            sb.AppendLine(
                $"Event ID: " +
                $"{(eventID != null ? eventID.stringValue : "<unknown>")}");

            UnityEngine.Object sourceObject =
                source != null
                    ? source.objectReferenceValue
                    : null;

            sb.AppendLine(
                $"Consequence Source: " +
                $"{DescribeObject(sourceObject)}");
        }

        sb.AppendLine();
    }

    private static void DumpSignalRouters(
        StringBuilder sb,
        MonoBehaviour[] behaviours)
    {
        sb.AppendLine("========================================");
        sb.AppendLine("SIGNAL ROUTER BINDINGS");
        sb.AppendLine("========================================");

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour == null ||
                behaviour.GetType().Name != "SignalRouter")
                continue;

            sb.AppendLine();
            sb.AppendLine(
                $"Router: {GetPath(behaviour.transform)}");

            FieldInfo bindingsField =
                FindField(
                    behaviour.GetType(),
                    "bindings");

            if (bindingsField == null)
            {
                sb.AppendLine("  Could not read bindings.");
                continue;
            }

            IEnumerable bindings =
                bindingsField.GetValue(behaviour) as IEnumerable;

            if (bindings == null)
            {
                sb.AppendLine("  <no bindings>");
                continue;
            }

            foreach (object binding in bindings)
            {
                if (binding == null)
                    continue;

                Type bindingType =
                    binding.GetType();

                FieldInfo signalField =
                    FindField(
                        bindingType,
                        "signalID");

                FieldInfo reactionField =
                    FindField(
                        bindingType,
                        "reaction");

                string signalID =
                    signalField?.GetValue(binding) as string;

                UnityEventBase reaction =
                    reactionField?.GetValue(binding)
                    as UnityEventBase;

                sb.AppendLine();
                sb.AppendLine(
                    $"Signal: " +
                    $"{(string.IsNullOrWhiteSpace(signalID) ? "<empty>" : signalID)}");

                DumpUnityEvent(
                    sb,
                    reaction,
                    "  ");
            }
        }

        sb.AppendLine();
    }

    private static void DumpInteractables(
        StringBuilder sb,
        MonoBehaviour[] behaviours)
    {
        sb.AppendLine("========================================");
        sb.AppendLine("INTERACTABLE OBJECTS");
        sb.AppendLine("========================================");

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour == null ||
                behaviour.GetType().Name != "InteractableObject")
                continue;

            Type type =
                behaviour.GetType();

            FieldInfo promptField =
                FindField(type, "interactionPrompt");

            FieldInfo onceField =
                FindField(type, "interactOnce");

            FieldInfo requirementField =
                FindField(type, "requiredTrueKey");

            FieldInfo disableField =
                FindField(type, "objectToDisable");

            FieldInfo eventField =
                FindField(type, "onInteract");

            string prompt =
                promptField?.GetValue(behaviour) as string;

            string requirement =
                requirementField?.GetValue(behaviour) as string;

            bool interactOnce =
                onceField != null &&
                (bool)onceField.GetValue(behaviour);

            GameObject objectToDisable =
                disableField?.GetValue(behaviour)
                as GameObject;

            UnityEventBase onInteract =
                eventField?.GetValue(behaviour)
                as UnityEventBase;

            sb.AppendLine();
            sb.AppendLine(
                $"GameObject: {GetPath(behaviour.transform)}");

            sb.AppendLine(
                $"Prompt: {prompt}");

            sb.AppendLine(
                $"Interact Once: {interactOnce}");

            sb.AppendLine(
                $"Required True Key: " +
                $"{(string.IsNullOrWhiteSpace(requirement) ? "<none>" : requirement)}");

            sb.AppendLine(
                $"Object To Disable: " +
                $"{DescribeObject(objectToDisable)}");

            sb.AppendLine("On Interact:");

            DumpUnityEvent(
                sb,
                onInteract,
                "  ");
        }

        sb.AppendLine();
    }

    private static void DumpUnityEvent(
        StringBuilder sb,
        UnityEventBase unityEvent,
        string indent)
    {
        if (unityEvent == null)
        {
            sb.AppendLine($"{indent}<none>");
            return;
        }

        int count =
            unityEvent.GetPersistentEventCount();

        if (count == 0)
        {
            sb.AppendLine(
                $"{indent}<no persistent calls>");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            UnityEngine.Object target =
                unityEvent.GetPersistentTarget(i);

            string method =
                unityEvent.GetPersistentMethodName(i);

            sb.AppendLine(
                $"{indent}[{i}] " +
                $"{DescribeObject(target)} " +
                $"-> {method}()");
        }
    }

    private static FieldInfo FindField(
        Type type,
        string fieldName)
    {
        while (type != null)
        {
            FieldInfo field =
                type.GetField(
                    fieldName,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

            if (field != null)
                return field;

            type = type.BaseType;
        }

        return null;
    }

    private static string DescribeObject(
        UnityEngine.Object obj)
    {
        if (obj == null)
            return "<none>";

        Component component =
            obj as Component;

        if (component != null)
        {
            return
                $"{GetPath(component.transform)} " +
                $"({component.GetType().Name})";
        }

        GameObject gameObject =
            obj as GameObject;

        if (gameObject != null)
        {
            return
                $"{GetPath(gameObject.transform)} " +
                $"(GameObject)";
        }

        return
            $"{obj.name} ({obj.GetType().Name})";
    }

    private static string GetPath(
        Transform transform)
    {
        if (transform == null)
            return "<null>";

        string path =
            transform.name;

        Transform current =
            transform.parent;

        while (current != null)
        {
            path =
                current.name + "/" + path;

            current =
                current.parent;
        }

        return path;
    }
}