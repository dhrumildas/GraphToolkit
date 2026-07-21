using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    public static bool IsDialogueOpen =>
        Instance != null && Instance.isOpen;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text continueText;

    private bool isOpen;
    private int openedFrame;

    private ConversationFacing activeFacing;

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
        if (!isOpen || Keyboard.current == null)
            return;

        // Prevent the E press that opened the dialogue
        // from immediately closing it on the same frame.
        if (Time.frameCount == openedFrame)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
            CloseDialogue();
    }

    public void ShowDialogue(
        string speaker,
        string line,
        Transform speakerTransform)
    {
        if (dialoguePanel == null ||
            speakerText == null ||
            dialogueText == null)
        {
            Debug.LogError(
                "DialogueUI has missing Inspector references.",
                this);

            return;
        }

        if (string.IsNullOrWhiteSpace(line))
        {
            Debug.LogWarning(
                "DialogueUI received an empty dialogue line.",
                this);

            return;
        }

        speakerText.SetText(
            string.IsNullOrWhiteSpace(speaker)
                ? "Unknown"
                : speaker);

        dialogueText.SetText(line);

        if (continueText != null)
            continueText.SetText("E — Continue");

        dialoguePanel.SetActive(true);

        isOpen = true;
        openedFrame = Time.frameCount;

        BeginSpeakerFacing(speakerTransform);
    }

    public void CloseDialogue()
    {
        if (!isOpen)
            return;

        isOpen = false;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (activeFacing != null)
        {
            activeFacing.StopFacing();
            activeFacing = null;
        }
    }

    private void BeginSpeakerFacing(Transform speakerTransform)
    {
        if (activeFacing != null)
            activeFacing.StopFacing();

        activeFacing = null;

        if (speakerTransform == null)
            return;

        activeFacing =
            speakerTransform.GetComponentInParent<ConversationFacing>();

        TPP_Controller player =
            FindAnyObjectByType<TPP_Controller>();

        if (activeFacing != null && player != null)
            activeFacing.BeginFacing(player.transform);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}