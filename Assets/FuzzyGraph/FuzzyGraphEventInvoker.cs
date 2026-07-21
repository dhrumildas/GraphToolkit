using UnityEngine;

public class FuzzyGraphEventInvoker : MonoBehaviour
{
    [SerializeField] private string eventID;

    [Tooltip(
        "The object that performs visual consequences. " +
        "For guard dialogue, assign the Guard root.")]
    [SerializeField] private Transform consequenceSource;

    private void Reset()
    {
        consequenceSource = transform;
    }

    public void Raise()
    {
        if (FuzzyGraphGameService.Instance == null)
        {
            Debug.LogError(
                "No FuzzyGraphGameService exists.",
                this);

            return;
        }

        Transform source =
            consequenceSource != null
                ? consequenceSource
                : transform;

        FuzzyGraphGameService.Instance.RaiseEvent(
            eventID,
            source);
    }
}