using FuzzyGraph.Runtime;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform flowerTarget;

    [Header("Distance Mapping")]
    [Tooltip("At or below this distance, proximity is 1.")]
    [SerializeField, Min(0f)]
    private float nearDistance = 1f;

    [Tooltip("At or beyond this distance, proximity is 0.")]
    [SerializeField, Min(0.01f)]
    private float farDistance = 8f;

    [Header("FuzzyGraph Keys")]
    [SerializeField]
    private string currentProximityKey =
        "Player.FlowerProximity";

    [SerializeField]
    private string peakProximityKey =
        "Player.PeakFlowerProximity";

    [Header("Runtime Values")]
    [SerializeField, Range(0f, 1f)]
    private float currentProximity;

    [SerializeField, Range(0f, 1f)]
    private float peakProximity;

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

        if (player == null ||
            flowerTarget == null ||
            FuzzyGraphGameService.Instance == null)
        {
            return;
        }

        // Picking the flowers disables their GameObject.
        // This also immediately removes the blush input.
        if (!flowerTarget.gameObject.activeInHierarchy)
        {
            currentProximity = 0f;
            peakProximity = 0f;

            PublishValues();
            return;
        }

        float distance = Vector3.Distance(
            player.position,
            flowerTarget.position);

        currentProximity =
            1f - Mathf.InverseLerp(
                nearDistance,
                farDistance,
                distance);

        currentProximity =
            Mathf.Clamp01(currentProximity);

        peakProximity =
            Mathf.Max(
                peakProximity,
                currentProximity);

        PublishValues();
    }

    /// <summary>
    /// Begins a new observation period after the guard
    /// has evaluated the previous peak proximity.
    /// </summary>
    public void ResetPeak()
    {
        peakProximity = currentProximity;
        PublishValues();

        Debug.Log(
            $"[FlowerProximity] Peak reset to " +
            $"{peakProximity:0.00}.",
            this);
    }

    [ContextMenu("Log Flower Proximity")]
    public void LogProximity()
    {
        Debug.Log(
            $"[FlowerProximity] Current: " +
            $"{currentProximity:0.00} | " +
            $"Peak: {peakProximity:0.00}",
            this);
    }

    private void PublishValues()
    {
        if (FuzzyGraphGameService.Instance == null)
            return;

        QueryBuilder queryBuilder =
            FuzzyGraphGameService.Instance.QueryBuilder;

        queryBuilder.SetLive(
            currentProximityKey,
            FuzzyValue.FromFloat(currentProximity));

        queryBuilder.SetLive(
            peakProximityKey,
            FuzzyValue.FromFloat(peakProximity));
    }

    private void ResolvePlayer()
    {
        if (player != null)
            return;

        TPP_Controller controller =
            FindAnyObjectByType<TPP_Controller>();

        if (controller != null)
            player = controller.transform;
    }

    private void OnDisable()
    {
        if (FuzzyGraphGameService.Instance == null)
            return;

        QueryBuilder queryBuilder =
            FuzzyGraphGameService.Instance.QueryBuilder;

        queryBuilder.RemoveLive(currentProximityKey);
        queryBuilder.RemoveLive(peakProximityKey);
    }

    private void OnValidate()
    {
        if (farDistance <= nearDistance)
            farDistance = nearDistance + 0.01f;
    }
}