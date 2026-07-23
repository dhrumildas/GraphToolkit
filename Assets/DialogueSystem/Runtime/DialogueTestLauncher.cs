using UnityEngine;

public class DialogueTestLauncher : MonoBehaviour
{
    [SerializeField] private RuntimeDialogueGraph dialogueGraph;

    private void Start()
    {
        if (DialogueRunner.Instance == null)
        {
            Debug.LogError(
                "No DialogueRunner exists in this scene.",
                this);

            return;
        }

        DialogueRunner.Instance.BeginDialogue(dialogueGraph);
    }
}