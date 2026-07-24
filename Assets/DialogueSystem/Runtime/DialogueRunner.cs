using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FuzzyGraph.Runtime;

public class DialogueRunner : MonoBehaviour
{
    public static DialogueRunner Instance { get; private set; }

    public static bool IsDialogueOpen =>
        Instance != null && Instance.isDialogueOpen;

    public static bool BlocksWorldInteraction =>
        Instance != null &&
        (
            Instance.isDialogueOpen ||
            Time.frameCount <= Instance.blockWorldInteractionUntilFrame
        );

    private int blockWorldInteractionUntilFrame = -1;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Choice UI")]
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private Transform choiceButtonContainer;

    private readonly Dictionary<string, RuntimeDialogueNode> nodeLookup = new();

    private RuntimeDialogueGraph currentGraph;
    private RuntimeDialogueNode currentNode;

    private bool isDialogueOpen;
    private int openedFrame;
    private ConversationFacing activeFacing;
    private Transform currentConversationActor;
    private TPP_Controller playerController;
    //private CursorLockMode previousCursorLockMode;
    //private bool previousCursorVisible;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isDialogueOpen ||
            currentNode == null ||
            Keyboard.current == null)
        {
            return;
        }

        // Prevent the E press that opened the conversation
        // from advancing it immediately.
        if (Time.frameCount == openedFrame)
            return;

        // Choices are selected with their generated buttons.
        if (currentNode.Choices.Count > 0)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
            AdvanceDialogue();
    }

    public void BeginDialogue(RuntimeDialogueGraph dialogueGraph,Transform conversationActor = null)
    {
        if (isDialogueOpen)
            return;

        if (dialogueGraph == null)
        {
            Debug.LogError("DialogueRunner received no RuntimeDialogueGraph.",this);
            return;
        }

        if (string.IsNullOrWhiteSpace(dialogueGraph.EntryNodeID))
        {
            Debug.LogError($"Dialogue graph '{dialogueGraph.name}' has no entry node.", dialogueGraph);

            return;
        }

        BuildNodeLookup(dialogueGraph);

        if (!nodeLookup.ContainsKey(dialogueGraph.EntryNodeID))
        {
            Debug.LogError($"Dialogue graph '{dialogueGraph.name}' has an invalid entry node.",dialogueGraph);

            return;
        }

        currentGraph = dialogueGraph;
        isDialogueOpen = true;
        openedFrame = Time.frameCount;

        //StoreAndUnlockCursor();
        UnlockCursorForDialogue();
        currentConversationActor = conversationActor;
        BeginConversationFacing(conversationActor);
        ShowNode(dialogueGraph.EntryNodeID);
    }

    private void BeginConversationFacing(Transform conversationActor)
    {
        if (activeFacing != null)
        {
            activeFacing.StopFacing();
            activeFacing = null;
        }

        if (conversationActor == null)
            return;

        activeFacing =
            conversationActor.GetComponentInParent<ConversationFacing>();

        TPP_Controller player =
            FindAnyObjectByType<TPP_Controller>();

        if (activeFacing == null)
        {
            Debug.LogWarning(
                $"{conversationActor.name} has no ConversationFacing component.",
                conversationActor);

            return;
        }

        if (player == null)
        {
            Debug.LogWarning(
                "No TPP_Controller was found for conversation facing.",
                this);

            return;
        }

        activeFacing.BeginFacing(player.transform);
    }

    public void EndDialogue()
    {
        currentConversationActor = null;

        if (!isDialogueOpen)
            return;

        blockWorldInteractionUntilFrame = Time.frameCount + 1;

        isDialogueOpen = false;
        currentGraph = null;
        currentNode = null;

        ClearChoiceButtons();

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (activeFacing != null)
        {
            activeFacing.StopFacing();
            activeFacing = null;
        }

        //RestoreCursor();
        LockCursorForGameplay();
    }

    private void AdvanceDialogue()
    {
        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        if (!string.IsNullOrWhiteSpace(currentNode.NextID))
        {
            ShowNode(currentNode.NextID);
            return;
        }

        EndDialogue();
    }

    private void ShowNode(string nodeID)
    {
        if (string.IsNullOrWhiteSpace(nodeID) ||
            !nodeLookup.TryGetValue(nodeID, out RuntimeDialogueNode nextNode))
        {
            EndDialogue();
            return;
        }

        currentNode = nextNode;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (speakerNameText != null)
        {
            speakerNameText.SetText(
                string.IsNullOrWhiteSpace(currentNode.SpeakerName)
                    ? string.Empty
                    : currentNode.SpeakerName);
        }

        if (dialogueText != null)
        {
            dialogueText.SetText(
                currentNode.DialogueText ?? string.Empty);
        }

        ClearChoiceButtons();

        if (currentNode.Choices.Count > 0)
            CreateChoiceButtons(currentNode.Choices);
    }

    private void CreateChoiceButtons(
    List<ChoiceData> choices)
    {
        if (choiceButtonPrefab == null ||
            choiceButtonContainer == null)
        {
            Debug.LogError(
                "DialogueRunner has missing choice UI references.",
                this);

            return;
        }

        foreach (ChoiceData choice in choices)
        {
            if (!IsChoiceAvailable(choice))
                continue;

            Button button = Instantiate(
                choiceButtonPrefab,
                choiceButtonContainer);

            TMP_Text buttonText =
                button.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                buttonText.SetText(
                    choice.ChoiceText ?? string.Empty);
            }

            string destinationNodeID =
                choice.DestinationNodeID;

            string fuzzyEventID =
                choice.FuzzyEventID;

            button.onClick.AddListener(() =>
            {
                if (!string.IsNullOrWhiteSpace(
                        fuzzyEventID))
                {
                    if (FuzzyGraphGameService.Instance == null)
                    {
                        Debug.LogError(
                            "Dialogue choice attempted to " +
                            "raise a FuzzyGraph event, but " +
                            "no FuzzyGraphGameService exists.",
                            this);

                        return;
                    }

                    var result =
                        FuzzyGraphGameService.Instance
                            .RaiseEvent(
                                fuzzyEventID,
                                currentConversationActor);

                    if (!result.hasMatch)
                    {
                        Debug.LogWarning(
                            $"Dialogue choice event " +
                            $"'{fuzzyEventID}' found no rule.",
                            this);

                        return;
                    }
                }

                if (!string.IsNullOrWhiteSpace(
                        destinationNodeID))
                {
                    ShowNode(destinationNodeID);
                }
                else
                {
                    EndDialogue();
                }
            });
        }
    }

    private bool IsChoiceAvailable(
    ChoiceData choice)
    {
        if (choice == null)
            return false;

        if (string.IsNullOrWhiteSpace(
                choice.ReqBoolKey))
        {
            return true;
        }

        if (FuzzyGraphGameService.Instance == null)
            return false;

        bool found =
            FuzzyGraphGameService.Instance.Context.TryGet(
                choice.ReqBoolKey,
                out FuzzyValue value);

        return found &&
               value.type == FuzzyValueType.Bool &&
               value.boolVal;
    }

    private void BuildNodeLookup(RuntimeDialogueGraph dialogueGraph)
    {
        nodeLookup.Clear();

        foreach (RuntimeDialogueNode node in dialogueGraph.AllNodes)
        {
            if (node == null || string.IsNullOrWhiteSpace(node.NodeID))
                continue;

            nodeLookup[node.NodeID] = node;
        }
    }

    private void ClearChoiceButtons()
    {
        if (choiceButtonContainer == null)
            return;

        for (int i = choiceButtonContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(
                choiceButtonContainer.GetChild(i).gameObject);
        }
    }

    //private void StoreAndUnlockCursor()
    //{
    //    previousCursorLockMode = Cursor.lockState;
    //    previousCursorVisible = Cursor.visible;

    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    //private void RestoreCursor()
    //{
    //    Cursor.lockState = previousCursorLockMode;
    //    Cursor.visible = previousCursorVisible;
    //}

    private void UnlockCursorForDialogue()
    {
        if (playerController == null)
            playerController = FindAnyObjectByType<TPP_Controller>();

        if (playerController != null)
        {
            playerController.SetCursorLocked(false);
            return;
        }

        // Fallback if no controller is found.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursorForGameplay()
    {
        if (playerController == null)
            playerController = FindAnyObjectByType<TPP_Controller>();

        if (playerController != null)
        {
            playerController.SetCursorLocked(true);
            return;
        }

        // Fallback if no controller is found.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}