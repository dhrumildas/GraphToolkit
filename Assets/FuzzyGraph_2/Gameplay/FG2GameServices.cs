using System;
using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;
using UnityEngine;

public sealed class FG2GameServices : MonoBehaviour
{
    public static FG2GameServices Instance { get; private set; }

    [Header("Compiled FuzzyGraph2")]
    [SerializeField]
    private RuntimeFuzzyGraph2 graph;

    public RuntimeFuzzyGraph2 Graph => graph;
    public PersistentContext Context { get; private set; }
    public QueryBuilder QueryBuilder { get; private set; }
    public SugenoNarrativeResult LastResult { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Context = new PersistentContext();
        QueryBuilder = new QueryBuilder(Context);

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public SugenoNarrativeResult RaiseEvent(string eventID, Transform consequenceSource = null)
    {
        if (string.IsNullOrWhiteSpace(eventID))
        {
            Debug.LogWarning("[FuzzyGraph2] Cannot raise an empty event ID.", this);
            return null;
        }

        if (graph == null)
        {
            Debug.LogError("[FuzzyGraph2] No RuntimeFuzzyGraph2 is assigned.", this);
            return null;
        }

        string trimmedEventID = eventID.Trim();

        if (!graph.TryGetEvent(trimmedEventID, out CompiledFuzzyEvent _))
        {
            Debug.LogError($"[FuzzyGraph2] The compiled graph does not contain event '{trimmedEventID}'.", this);
            return null;
        }

        WorldStateQuery query = QueryBuilder.Build();
        SugenoNarrativeResult result;

        try
        {
            result = graph.Resolve(trimmedEventID, query);
        }
        catch (Exception exception)
        {
            Debug.LogError($"[FuzzyGraph2] Event '{trimmedEventID}' failed.", this);
            Debug.LogException(exception, this);
            return null;
        }

        LastResult = result;
        LogResolution(trimmedEventID, result);

        /*
         * FuzzyGraph2's intended order is:
         * consequence execution
         *          ↓
         * persistent write-backs
         */

        foreach (RuntimeConsequence consequence in result.Consequences)
        {
            DispatchConsequence(consequence, consequenceSource);
        }

        int appliedWriteBacks = WriteBackProcessor.ApplyAll(Context, result.WriteBacks);
        Debug.Log($"[FuzzyGraph2] Applied {appliedWriteBacks} write-back(s).", this);

        LogPersistentContext();

        return result;
    }

    private void DispatchConsequence(RuntimeConsequence consequence, Transform consequenceSource)
    {
        if (consequence == null) return;

        switch (consequence.consequenceType)
        {
            case ConsequenceType.Dialogue:
                if (DialogueUI.Instance == null)
                {
                    Debug.LogError("[FuzzyGraph2] A Dialogue consequence was selected, but no DialogueUI exists.", this);
                    return;
                }

                string speakerName = !string.IsNullOrWhiteSpace(consequence.targetKey)
                    ? consequence.targetKey
                    : consequenceSource != null ? consequenceSource.name : "Unknown";

                DialogueUI.Instance.ShowDialogue(speakerName, consequence.payLoad, consequenceSource);
                break;

            case ConsequenceType.FireEvent:
                DispatchFireEvent(consequence, consequenceSource);
                break;

            default:
                Debug.Log($"[FuzzyGraph2] Consequence: {consequence.consequenceType}" +
                    $" | Target: {consequence.targetKey} | Payload: {consequence.payLoad}", this);
                break;
        }
    }

    private void DispatchFireEvent(RuntimeConsequence consequence, Transform consequenceSource)
    {
        // 1. raise another fuzzygraph2 event
        bool isFuzzyGraph2Event = string.Equals(
            consequence.targetKey, "FuzzyEvent", StringComparison.OrdinalIgnoreCase);

        if (isFuzzyGraph2Event)
        {
            RaiseEvent(consequence.payLoad, consequenceSource);
            return;
        }

        // 2. send a non-dialogue signal to the current unity scene
        bool isGameplaySignal = string.Equals(
            consequence.targetKey, "GameplaySignal", StringComparison.OrdinalIgnoreCase);

        if (isGameplaySignal)
        {
            if (SignalRouter.Instance == null)
            {
                Debug.LogError(
                    "[FuzzyGraph2] A GameplaySignal was requested, " +
                    "but no SignalRouter exists in the scene.", this);
                return;
            }

            SignalRouter.Instance.TryDispatch(consequence.payLoad);
            return;
        }

        // 3. start an authored dialoguegraph
        bool isDialogueGraph = string.Equals(
            consequence.targetKey, "DialogueGraph", StringComparison.OrdinalIgnoreCase);

        if (isDialogueGraph)
        {
            if (DialogueGraphLibrary.Instance == null)
            {
                Debug.LogError(
                    "[FuzzyGraph2] A DialogueGraph was requested, " +
                    "but no DialogueGraphLibrary exists.", this);
                return;
            }

            DialogueGraphLibrary.Instance.StartDialogue(consequence.payLoad, consequenceSource);
            return;
        }

        // unknown fireevent targets are still reported rather than silently ignored
        Debug.LogWarning(
            $"[FuzzyGraph2] Unknown FireEvent target '{consequence.targetKey}' | " +
            $"Payload: {consequence.payLoad}", this);
    }

    private void LogResolution(string eventID, SugenoNarrativeResult result)
    {
        SugenoInferenceResult inference = result.Inference;
        string outputText = inference.HasOutput ? inference.Output.ToString("0.####") : "No numeric output";

        Debug.Log($"[FuzzyGraph2] Event: {eventID} | Output: {outputText} | " +
                  $"Outcome: {result.OutcomeId} | Fallback: {result.UsedFallback}", this);

        foreach (SugenoRuleActivation activation in inference.Activations)
        {
            Debug.Log($"[FuzzyGraph2] Rule: {activation.Rule.Id} | " +
                      $"Firing strength: {activation.FiringStrength:0.####} | " +
                      $"Consequent: {activation.Rule.Consequent:0.####} | " +
                      $"αz: {activation.WeightedConsequent:0.####}", this);
        }

        Debug.Log($"[FuzzyGraph2] Numerator: {inference.Numerator:0.####} | Denominator: {inference.Denominator:0.####}", this);

        foreach (SugenoRuleDiagnostic diagnostic in inference.Diagnostics)
        {
            Debug.LogWarning($"[FuzzyGraph2] Rule '{diagnostic.Rule.Id}' diagnostic: {diagnostic.Message}", this);
        }
    }

    [ContextMenu("Log Persistent Context")]
    public void LogPersistentContext()
    {
        if (Context == null) return;

        Debug.Log("----- FUZZYGRAPH2 PERSISTENT CONTEXT -----", this);

        foreach (var pair in Context.Values)
        {
            Debug.Log($"{pair.Key} = {pair.Value}", this);
        }

        Debug.Log("------------------------------------------", this);
    }
}