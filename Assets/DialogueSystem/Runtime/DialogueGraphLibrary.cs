using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphLibrary : MonoBehaviour
{
    public static DialogueGraphLibrary Instance { get; private set; }

    [Serializable]
    private class DialogueEntry
    {
        [Tooltip("Stable ID used by FuzzyGraph consequences.")]
        public string dialogueID;

        public RuntimeDialogueGraph dialogueGraph;
    }

    [SerializeField]
    private List<DialogueEntry> dialogues = new();

    private readonly Dictionary<string, RuntimeDialogueGraph> lookup =
        new(StringComparer.OrdinalIgnoreCase);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        BuildLookup();
    }

    public bool StartDialogue(
        string dialogueID,
        Transform conversationActor)
    {
        if (string.IsNullOrWhiteSpace(dialogueID))
        {
            Debug.LogError(
                "Cannot start a dialogue with an empty ID.",
                this);

            return false;
        }

        if (!lookup.TryGetValue(
                dialogueID,
                out RuntimeDialogueGraph dialogueGraph))
        {
            Debug.LogError(
                $"No dialogue graph is registered with ID " +
                $"'{dialogueID}'.",
                this);

            return false;
        }

        if (DialogueRunner.Instance == null)
        {
            Debug.LogError(
                "No DialogueRunner exists in the scene.",
                this);

            return false;
        }

        DialogueRunner.Instance.BeginDialogue(
            dialogueGraph,
            conversationActor);

        return true;
    }

    private void BuildLookup()
    {
        lookup.Clear();

        foreach (DialogueEntry entry in dialogues)
        {
            if (entry == null ||
                string.IsNullOrWhiteSpace(entry.dialogueID) ||
                entry.dialogueGraph == null)
            {
                continue;
            }

            if (!lookup.TryAdd(
                    entry.dialogueID,
                    entry.dialogueGraph))
            {
                Debug.LogWarning(
                    $"Duplicate dialogue ID: " +
                    $"'{entry.dialogueID}'.",
                    this);
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}