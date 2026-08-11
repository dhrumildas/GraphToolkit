using UnityEngine;

public sealed class LoiterDialogue : MonoBehaviour
{
    [Header("Loiter Source")]
    [SerializeField] private LoiterTelemetry loiterTelemetry;

    [Min(0.1f)]
    [SerializeField] private float triggerTime = 10f;

    [Header("FuzzyGraph2")]
    [SerializeField] private bool raiseFuzzyEvent = false;
    [SerializeField] private string eventID = "EvaluateCarpetLoitering";
    [SerializeField] private Transform consequenceSource;

    [Header("Behaviour")]
    [SerializeField] private bool waitForDialogue = true;

    [Header("Debug")]
    [SerializeField] private bool firedThisVisit;

    private void Reset()
    {
        loiterTelemetry = GetComponent<LoiterTelemetry>();
        consequenceSource = transform;
    }

    private void Update()
    {
        if (loiterTelemetry == null) return;

        if (!loiterTelemetry.IsTracking)
        {
            firedThisVisit = false;
            return;
        }

        if (firedThisVisit) return;

        if (loiterTelemetry.CurrentLoiterTime < triggerTime) return;

        if (waitForDialogue && DialogueRunner.IsDialogueOpen) return;

        firedThisVisit = true;

        Debug.Log($"[LoiterDialogue] {triggerTime:0.#}s reached. Distance: {loiterTelemetry.CurrentDistance:0.##}m", this);

        if (!raiseFuzzyEvent) return;

        if (FG2GameServices.Instance == null)
        {
            Debug.LogWarning("[LoiterDialogue] FG2GameServices is unavailable.", this);
            return;
        }

        Transform source = consequenceSource != null ? consequenceSource : transform;
        FG2GameServices.Instance.RaiseEvent(eventID, source);
    }
}