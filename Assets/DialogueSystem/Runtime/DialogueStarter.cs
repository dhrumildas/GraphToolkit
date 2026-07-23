using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField]
    private RuntimeDialogueGraph dialogueGraph;

    [Tooltip("Usually the Guard root transform.")]
    [SerializeField]
    private Transform conversationActor;

    private void Reset()
    {
        conversationActor = transform;
    }

    public void StartDialogue()
    {
        if (DialogueRunner.Instance == null)
        {
            Debug.LogError(
                "No DialogueRunner exists in the scene.",
                this);

            return;
        }

        if (dialogueGraph == null)
        {
            Debug.LogError(
                "No RuntimeDialogueGraph is assigned.",
                this);

            return;
        }

        Transform actor =
            conversationActor != null
                ? conversationActor
                : transform;

        DialogueRunner.Instance.BeginDialogue(
            dialogueGraph,
            actor);
    }
}