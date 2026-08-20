using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class SignalRouter : MonoBehaviour
{
    [Serializable]
    private sealed class SignalBinding
    {
        [Tooltip("Stable signal ID authored in the FuzzyGraph2 consequence.")]
        public string signalID;

        [Tooltip("Scene reaction executed when this signal is raised.")]
        public UnityEvent reaction;
    }

    public static SignalRouter Instance { get; private set; }

    [Header("Gameplay Signal Bindings")]
    [SerializeField]
    private List<SignalBinding> bindings = new List<SignalBinding>();

    [Header("Debug")]
    [SerializeField]
    private string testSignalID = "Vendor.ShowNervous";

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError(
                "[FuzzyGraph2] More than one SignalRouter " +
                "is active in the scene.",
                this);

            return;
        }

        Instance = this;
    }

    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    // attempts to invoke the unityevent associated with a graph-authored gameplay signal id
    public bool TryDispatch(string signalID)
    {
        if (string.IsNullOrWhiteSpace(signalID))
        {
            Debug.LogWarning(
                "[FuzzyGraph2] Cannot dispatch an empty gameplay signal.",
                this);

            return false;
        }

        string requestedID = signalID.Trim();

        foreach (SignalBinding binding in bindings)
        {
            if (binding == null ||
                string.IsNullOrWhiteSpace(binding.signalID))
            {
                continue;
            }

            if (!string.Equals(
                    binding.signalID.Trim(),
                    requestedID,
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            Debug.Log(
                $"[FuzzyGraph2] Gameplay Signal: {requestedID}",
                this);

            binding.reaction?.Invoke();
            return true;
        }

        Debug.LogWarning(
            $"[FuzzyGraph2] No scene reaction is bound to " +
            $"gameplay signal '{requestedID}'.",
            this);

        return false;
    }

    [ContextMenu("Dispatch Test Signal")]
    private void DispatchTestSignal()
    {
        TryDispatch(testSignalID);
    }
}