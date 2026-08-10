using FuzzyGraph.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class ProximitySensor : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform player;

    [FormerlySerializedAs("flowerTarget")]
    [SerializeField] private Transform target;

    [Header("Distance Mapping")]
    [Tooltip("At or below this distance, proximity is 1.")]
    [SerializeField, Min(0f)] private float nearDistance = 1f;

    [Tooltip("At or beyond this distance, proximity is 0.")]
    [SerializeField, Min(0.01f)] private float farDistance = 8f;

    [Header("Target Behaviour")]
    [Tooltip("If true, proximity resets to zero when the target GameObject is disabled. Useful for things such as picked-up flowers.")]
    [SerializeField] private bool resetWhenTargetInactive = false;

    [Header("FuzzyGraph Keys")]
    [SerializeField] private string currentProximityKey = "Player.Proximity";
    [SerializeField] private string peakProximityKey = "Player.PeakProximity";

    [Header("Runtime Values")]
    [SerializeField, Range(0f, 1f)] private float currentProximity;
    [SerializeField, Range(0f, 1f)] private float peakProximity;

    public float CurrentProximity => currentProximity;
    public float PeakProximity => peakProximity;

    private void Start()
    {
        ResolvePlayer();
        PublishValues();
    }

    private void Update()
    {
        ResolvePlayer();

        if (player == null || target == null || FG2GameServices.Instance == null) return;

        if (resetWhenTargetInactive && !target.gameObject.activeInHierarchy)
        {
            currentProximity = 0f;
            peakProximity = 0f;

            PublishValues();
            return;
        }

        float distance = Vector3.Distance(player.position, target.position);

        currentProximity = 1f - Mathf.InverseLerp(nearDistance, farDistance, distance);
        currentProximity = Mathf.Clamp01(currentProximity);

        peakProximity = Mathf.Max(peakProximity, currentProximity);

        PublishValues();
    }

    public void ResetPeak()
    {
        peakProximity = currentProximity;
        PublishValues();

        Debug.Log($"[ProximitySensor] Peak reset to {peakProximity:0.00}.", this);
    }

    [ContextMenu("Log Proximity")]
    public void LogProximity()
    {
        Debug.Log($"[ProximitySensor] Current: {currentProximity:0.00} | Peak: {peakProximity:0.00}", this);
    }

    private void PublishValues()
    {
        if (FG2GameServices.Instance == null) return;

        QueryBuilder queryBuilder = FG2GameServices.Instance.QueryBuilder;

        if (!string.IsNullOrWhiteSpace(currentProximityKey))
        {
            queryBuilder.SetLive(currentProximityKey.Trim(), FuzzyValue.FromFloat(currentProximity));
        }

        if (!string.IsNullOrWhiteSpace(peakProximityKey))
        {
            queryBuilder.SetLive(peakProximityKey.Trim(), FuzzyValue.FromFloat(peakProximity));
        }
    }

    private void ResolvePlayer()
    {
        if (player != null) return;

        TPP_Controller controller = FindAnyObjectByType<TPP_Controller>();
        if (controller != null) player = controller.transform;
    }

    private void OnDisable()
    {
        if (FG2GameServices.Instance == null) return;

        QueryBuilder queryBuilder = FG2GameServices.Instance.QueryBuilder;

        if (!string.IsNullOrWhiteSpace(currentProximityKey))
        {
            queryBuilder.RemoveLive(currentProximityKey.Trim());
        }

        if (!string.IsNullOrWhiteSpace(peakProximityKey))
        {
            queryBuilder.RemoveLive(peakProximityKey.Trim());
        }
    }

    private void OnValidate()
    {
        if (farDistance <= nearDistance)
        {
            farDistance = nearDistance + 0.01f;
        }
    }
}