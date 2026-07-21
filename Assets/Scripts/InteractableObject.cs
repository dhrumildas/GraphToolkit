using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interact";
    [SerializeField] private bool interactOnce = true;

    [Header("Result")]
    [Tooltip("Optional object to disable after interaction.")]
    [SerializeField] private GameObject objectToDisable;

    [Tooltip("Optional additional actions called when the player interacts.")]
    [SerializeField] private UnityEvent onInteract;

    private bool hasInteracted;

    public string InteractionPrompt => interactionPrompt;

    public bool CanInteract =>
        !hasInteracted &&
        isActiveAndEnabled &&
        gameObject.activeInHierarchy;

    private void Reset()
    {
        Collider interactionCollider = GetComponent<Collider>();
        interactionCollider.isTrigger = true;
    }

    public void Interact(GameObject interactor)
    {
        if (!CanInteract)
            return;

        Debug.Log(
            $"{interactor.name} interacted with {name}.",
            this);

        onInteract?.Invoke();

        if (interactOnce)
            hasInteracted = true;

        if (objectToDisable != null)
            objectToDisable.SetActive(false);
    }
}