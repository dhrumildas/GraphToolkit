using FuzzyGraph.Runtime;
using System;
using UnityEngine;

public class FuzzyGraphGameService : MonoBehaviour
{
    public static FuzzyGraphGameService Instance { get; private set; }

    [Header("Compiled FuzzyGraph")]
    [SerializeField] private RuntimeFuzzyGraph graph;

    public PersistentContext Context { get; private set; }

    public QueryBuilder QueryBuilder { get; private set; }

    public RuntimeFuzzyGraph Graph => graph;

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

    public MatchResult RaiseEvent(
    string eventID,
    Transform consequenceSource)
    {
        if (string.IsNullOrWhiteSpace(eventID))
        {
            Debug.LogWarning(
                "Cannot raise an empty FuzzyGraph event ID.");

            return new MatchResult();
        }

        if (graph == null)
        {
            Debug.LogError(
                "No RuntimeFuzzyGraph is assigned.",
                this);

            return new MatchResult();
        }

        WorldStateQuery query = QueryBuilder.Build();

        MatchResult result =
            graph.Evaluate(query, eventID);

        if (!result.hasMatch)
        {
            Debug.Log(
                $"[FuzzyGraph] Event '{eventID}' " +
                "found no matching rule.");

            return result;
        }

        Debug.Log(
            $"[FuzzyGraph] Event: {eventID} | " +
            $"Winner: {result.rule.ruleID} | " +
            $"Score: {result.scoreFloat:0.##}");

        int writeBackCount =
            WriteBackProcessor.ApplyAll(
                Context,
                result.rule.WriteBacks);

        Debug.Log(
            $"[FuzzyGraph] Applied " +
            $"{writeBackCount} write-back(s).");

        foreach (RuntimeConsequence consequence
                 in result.rule.Consequences)
        {
            DispatchConsequence(
                consequence,
                consequenceSource);
        }

        LogPersistentContext();

        return result;
    }

    private void DispatchConsequence(RuntimeConsequence consequence, Transform consequenceSource)
    {
        {
            if (consequence == null)
                return;

            switch (consequence.consequenceType)
            {
                case ConsequenceType.Dialogue:
                    {
                        if (DialogueUI.Instance == null)
                        {
                            Debug.LogError(
                                "A Dialogue consequence was returned, " +
                                "but no DialogueUI exists.");

                            return;
                        }

                        // For Dialogue consequences, Target Key
                        // temporarily represents the speaker name.
                        string speakerName =
                            !string.IsNullOrWhiteSpace(
                                consequence.targetKey)
                                ? consequence.targetKey
                                : consequenceSource != null
                                    ? consequenceSource.name
                                    : "Unknown";

                        DialogueUI.Instance.ShowDialogue(
                            speakerName,
                            consequence.payLoad,
                            consequenceSource);

                        break;
                    }

                case ConsequenceType.FireEvent:
                    {
                        bool isFuzzyEventRequest =
                            string.Equals(
                                consequence.targetKey,
                                "FuzzyEvent",
                                StringComparison.OrdinalIgnoreCase);

                        if (isFuzzyEventRequest)
                        {
                            RaiseEvent(
                                consequence.payLoad,
                                consequenceSource);

                            break;
                        }

                        bool isDialogueGraphRequest =
                            string.Equals(
                                consequence.targetKey,
                                "DialogueGraph",
                                StringComparison.OrdinalIgnoreCase);

                        if (isDialogueGraphRequest)
                        {
                            if (DialogueGraphLibrary.Instance == null)
                            {
                                Debug.LogError(
                                    "FuzzyGraph requested a DialogueGraph, " +
                                    "but no DialogueGraphLibrary exists.",
                                    this);

                                break;
                            }

                            DialogueGraphLibrary.Instance.StartDialogue(
                                consequence.payLoad,
                                consequenceSource);

                            break;
                        }

                        Debug.Log(
                            $"[FuzzyGraph] FireEvent: " +
                            $"{consequence.targetKey} | " +
                            $"{consequence.payLoad}");

                        break;
                    }

                default:
                    {
                        Debug.Log(
                            $"[FuzzyGraph] Consequence: " +
                            $"{consequence.consequenceType} | " +
                            $"{consequence.payLoad}");

                        break;
                    }
            }
        }
    }

    [ContextMenu("Log Persistent Context")]
    public void LogPersistentContext()
    {
        Debug.Log("----- PERSISTENT CONTEXT -----");

        foreach (var pair in Context.Values)
        {
            Debug.Log($"{pair.Key} = {pair.Value}");
        }

        Debug.Log("------------------------------");
    }
}