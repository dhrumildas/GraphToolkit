using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private readonly List<InteractableObject> nearbyInteractables = new();

    private InteractableObject currentInteractable;

    private void Update()
    {
        if (DialogueUI.IsDialogueOpen)
        {
            currentInteractable = null;
            return;
        }

        RemoveInvalidInteractables();
        currentInteractable = FindClosestInteractable();

        if (currentInteractable == null || Keyboard.current == null)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            InteractableObject usedInteractable = currentInteractable;

            usedInteractable.Interact(gameObject);

            if (usedInteractable == null || !usedInteractable.CanInteract)
                nearbyInteractables.Remove(usedInteractable);

            currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactable = FindInteractable(other);

        if (interactable == null)
            return;

        if (!nearbyInteractables.Contains(interactable))
            nearbyInteractables.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableObject interactable = FindInteractable(other);

        if (interactable != null)
            nearbyInteractables.Remove(interactable);
    }

    private InteractableObject FindInteractable(Collider targetCollider)
    {
        InteractableObject interactable =
            targetCollider.GetComponent<InteractableObject>();

        if (interactable == null)
        {
            interactable =
                targetCollider.GetComponentInParent<InteractableObject>();
        }

        return interactable;
    }

    private InteractableObject FindClosestInteractable()
    {
        InteractableObject closest = null;
        float closestDistanceSquared = float.MaxValue;

        foreach (InteractableObject interactable in nearbyInteractables)
        {
            if (interactable == null || !interactable.CanInteract)
                continue;

            float distanceSquared =
                (interactable.transform.position - transform.position)
                .sqrMagnitude;

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                closest = interactable;
            }
        }

        return closest;
    }

    private void RemoveInvalidInteractables()
    {
        for (int i = nearbyInteractables.Count - 1; i >= 0; i--)
        {
            InteractableObject interactable = nearbyInteractables[i];

            if (interactable == null || !interactable.CanInteract)
                nearbyInteractables.RemoveAt(i);
        }
    }

    private void OnDisable()
    {
        nearbyInteractables.Clear();
        currentInteractable = null;
    }

    private void OnGUI()
    {
        if (DialogueUI.IsDialogueOpen)
            return;

        if (currentInteractable == null ||
            !currentInteractable.CanInteract)
        {
            return;
        }

        const float width = 300f;
        const float height = 40f;

        Rect promptArea = new Rect(
            (Screen.width - width) * 0.5f,
            Screen.height - 100f,
            width,
            height);

        GUI.Box(
            promptArea,
            $"E  —  {currentInteractable.InteractionPrompt}");
    }
}