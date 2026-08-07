using UnityEngine;

// raises a stable fuzzygraph2 event id. telemetry is refreshed before resolution.
public sealed class EventInvoker : MonoBehaviour
{
    [Header("FuzzyGraph2 Event")]
    [SerializeField]
    private string eventID = "Inspect";

    [Tooltip("The actor or object that should perform the selected dialogue or visual consequence.")]
    [SerializeField]
    private Transform consequenceSource;

    [Header("Telemetry")]
    [Tooltip("Telemetry components that must publish their latest values immediately before this event is resolved.")]
    [SerializeField]
    private DistanceTelemetry[] distanceTelemetry;

    private void Reset()
    {
        consequenceSource = transform;
    }

    // can be called by interaction components, triggers, unityevents or dialogue choices
    public void Raise()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("[FuzzyGraph2] Events can only be raised in Play Mode.", this);
            return;
        }

        if (FG2GameServices.Instance == null)
        {
            Debug.LogError("[FuzzyGraph2] No FG2GameServices instance exists.", this);
            return;
        }

        if (string.IsNullOrWhiteSpace(eventID))
        {
            Debug.LogError("[FuzzyGraph2] Event ID is empty.", this);
            return;
        }

        foreach (DistanceTelemetry telemetry in distanceTelemetry)
        {
            if (telemetry != null) telemetry.Publish();
        }

        Transform source = consequenceSource != null ? consequenceSource : transform;
        FG2GameServices.Instance.RaiseEvent(eventID.Trim(), source);
    }

    [ContextMenu("Raise FuzzyGraph2 Event")]
    private void RaiseFromInspector()
    {
        Raise();
    }
}