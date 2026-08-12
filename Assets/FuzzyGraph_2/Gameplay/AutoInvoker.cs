using UnityEngine;

public sealed class AutoInvoker : MonoBehaviour
{
    [Header("FuzzyGraph2 Event")]
    [SerializeField] private string eventID = "AmbientSocialPressure";

    [SerializeField] private Transform consequenceSource;

    [Header("Timing")]
    [Min(0.1f)]
    [SerializeField] private float interval = 1f;

    [SerializeField] private bool runAutomatically = true;

    private float timer;

    private void Update()
    {
        if (!runAutomatically) return;

        if (DialogueRunner.BlocksWorldInteraction) return;

        if (FG2GameServices.Instance == null) return;

        timer += Time.deltaTime;

        if (timer < interval) return;

        timer = 0f;

        FG2GameServices.Instance.RaiseEvent(eventID, consequenceSource);
    }

    [ContextMenu("Raise Event Now")]
    public void RaiseNow()
    {
        if (FG2GameServices.Instance == null)
        {
            Debug.LogWarning("[FuzzyGraph2] Cannot raise automatic event because FG2GameServices is unavailable.", this);
            return;
        }

        FG2GameServices.Instance.RaiseEvent(eventID, consequenceSource);
    }
}