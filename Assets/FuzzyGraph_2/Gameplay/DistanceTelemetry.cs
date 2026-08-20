using FuzzyGraph.Runtime;
using UnityEngine;

// measures raw distance to target and publishes to query builder
public sealed class DistanceTelemetry : MonoBehaviour
{
    [Header("Scene References")]
    [Tooltip("Usually the player.")]
    [SerializeField]
    private Transform observer;

    [Tooltip("The object whose distance is being measured.")]
    [SerializeField]
    private Transform target;

    [Header("FuzzyGraph2")]
    [SerializeField]
    private string variableID = "Player.DistanceToCarpet";

    [Tooltip("Publish the current distance continuously. The event invoker also forces a fresh publication before resolving.")]
    [SerializeField]
    private bool publishEveryFrame = true;

    [Header("Runtime")]
    [SerializeField]
    private float currentDistance;

    public float CurrentDistance => currentDistance;

    private void Start()
    {
        ResolveObserver();
        Publish();
    }

    private void Update()
    {
        if (publishEveryFrame)
            Publish();
    }

    // measures and publishes the current raw distance
    public bool Publish()
    {
        ResolveObserver();

        if (FG2GameServices.Instance == null) return false;

        if (observer == null)
        {
            Debug.LogError("[FuzzyGraph2] Distance telemetry has no observer.", this);
            return false;
        }

        if (target == null)
        {
            Debug.LogError("[FuzzyGraph2] Distance telemetry has no target.", this);
            return false;
        }

        if (string.IsNullOrWhiteSpace(variableID))
        {
            Debug.LogError("[FuzzyGraph2] Distance telemetry has an empty variable ID.", this);
            return false;
        }

        currentDistance = Vector3.Distance(observer.position, target.position);
        FG2GameServices.Instance.QueryBuilder.SetLive(variableID.Trim(), FuzzyValue.FromFloat(currentDistance));

        return true;
    }

    [ContextMenu("Log Current Distance")]
    private void LogCurrentDistance()
    {
        Debug.Log($"[FuzzyGraph2] {variableID} = {currentDistance:0.###}", this);
    }

    private void ResolveObserver()
    {
        if (observer != null) return;

        TPP_Controller playerController = FindAnyObjectByType<TPP_Controller>();
        if (playerController != null) observer = playerController.transform;
    }

    private void OnDisable()
    {
        if (FG2GameServices.Instance == null || string.IsNullOrWhiteSpace(variableID)) return;

        FG2GameServices.Instance.QueryBuilder.RemoveLive(variableID.Trim());
    }
}