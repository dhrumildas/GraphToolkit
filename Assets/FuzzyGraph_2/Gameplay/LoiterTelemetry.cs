using UnityEngine;
using FuzzyGraph.Runtime;

public sealed class LoiterTelemetry : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform observer;
    [SerializeField] private Transform target;

    [Header("Telemetry")]
    [SerializeField] private string variableID = "Player.LoiterTimeNearCarpet";

    [Tooltip("Player must be within this broad area for loiter time to accumulate.")]
    [SerializeField] private float trackingRadius = 6f;

    [Tooltip("Maximum time sent to FuzzyGraph2.")]
    [SerializeField] private float maximumTime = 15f;

    [Header("Debug")]
    [SerializeField] private float currentLoiterTime;
    [SerializeField] private float currentDistance;

    public float CurrentLoiterTime => currentLoiterTime;
    public float CurrentDistance => currentDistance;
    public bool IsTracking => currentDistance <= trackingRadius;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (observer == null || target == null)
            return;

        currentDistance = Vector3.Distance(
            observer.position,
            target.position);

        if (currentDistance <= trackingRadius)
        {
            currentLoiterTime += Time.deltaTime;
        }
        else
        {
            currentLoiterTime = 0f;
        }

        currentLoiterTime = Mathf.Clamp(
            currentLoiterTime,
            0f,
            maximumTime);

        if (FG2GameServices.Instance == null)
            return;

        FG2GameServices.Instance.QueryBuilder.SetLive(
            variableID,
            FuzzyValue.FromFloat(currentLoiterTime));
    }

    private void OnDisable()
    {
        if (FG2GameServices.Instance == null)
            return;

        FG2GameServices.Instance.QueryBuilder.RemoveLive(variableID);
    }
}