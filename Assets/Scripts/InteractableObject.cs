using FuzzyGraph.Runtime;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interact";
    [SerializeField] private bool interactOnce = true;

    [Header("Optional FuzzyGraph2 Requirement")]
    [Tooltip("If set, this object can only be interacted with when this PersistentContext bool exists and is true.")]
    [SerializeField] private string requiredTrueKey;

    [Header("Result")]
    [Tooltip("Optional object to disable after interaction.")]
    [SerializeField] private GameObject objectToDisable;

    [Tooltip("Optional additional actions called when the player interacts.")]
    [SerializeField] private UnityEvent onInteract;

    private bool hasInteracted;

    public string InteractionPrompt => interactionPrompt;

    public bool CanInteract => !hasInteracted && isActiveAndEnabled && gameObject.activeInHierarchy && MeetsContextRequirement();

    private void Reset()
    {
        Collider interactionCollider = GetComponent<Collider>();
        interactionCollider.isTrigger = true;
    }

    public void Interact(GameObject interactor)
    {
        if (!CanInteract) return;

        Debug.Log($"{interactor.name} interacted with {name}.", this);

        onInteract?.Invoke();

        if (interactOnce)
            hasInteracted = true;

        if (objectToDisable != null)
            objectToDisable.SetActive(false);
    }

    private bool MeetsContextRequirement()
    {
        // empty = ordinary interactable.
        if (string.IsNullOrWhiteSpace(requiredTrueKey)) return true;

        if (FG2GameServices.Instance == null) return false;

        PersistentContext context = FG2GameServices.Instance.Context;
        bool found = context.TryGet(requiredTrueKey.Trim(), out FuzzyValue value);

        return found && value.type == FuzzyValueType.Bool && value.boolVal;
    }
}