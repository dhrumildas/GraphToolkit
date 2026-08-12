using UnityEngine;

public sealed class Pursuit2 : MonoBehaviour
{
    private enum SequenceState
    {
        Idle,
        WaitSearchDialogueClose,
        ApproachPlayer,
        WaitScoldDialogue,
        ReturnHome,
        WaitLeaveDialogueClose
    }

    [Header("Characters")]
    [SerializeField] private Transform guard;
    [SerializeField] private Transform player;

    [Header("Guard Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float stopDistance = 1.8f;

    [Header("Dialogue")]
    [SerializeField] private string scoldDialogueID = "GuardTreeScold";
    [SerializeField] private string keyFoundDialogueID = "TreeKeyFound";

    [Header("Optional")]
    [SerializeField] private Pursuit pursuit;

    private Vector3 guardHomePosition;
    private Quaternion guardHomeRotation;
    private SequenceState state = SequenceState.Idle;
    private bool pursuitWasEnabled;

    private void Start()
    {
        if (guard == null)
        {
            Debug.LogError("[TreeHollow] Guard has not been assigned.", this);
            return;
        }

        // remember the guard's original level position and orientation.
        guardHomePosition = guard.position;
        guardHomeRotation = guard.rotation;
    }

    // called by gameplay signal: treehollow.searchstarted
    public void BeginSearch()
    {
        if (state != SequenceState.Idle) return;

        Debug.Log("[TreeHollow] Player chose to search the hollow.", this);

        if (pursuit != null)
        {
            pursuitWasEnabled = pursuit.enabled;
            pursuit.enabled = false;
        }

        // searchtreehollow is raised while the choice dialogue
        // is still open, so wait until dialoguerunner closes it.
        state = SequenceState.WaitSearchDialogueClose;
    }

    // called by gameplay signal: treehollow.walkaway
    public void WalkAway()
    {
        if (state != SequenceState.Idle) return;

        Debug.Log("[TreeHollow] Player walked away.", this);

        // the guard is currently facing the player because
        // he was the conversation actor.
        // wait for that dialogue to close before restoring him.
        state = SequenceState.WaitLeaveDialogueClose;
    }

    private void Update()
    {
        if (guard == null || player == null) return;

        switch (state)
        {
            case SequenceState.WaitSearchDialogueClose:
                if (DialogueRunner.IsDialogueOpen) return;
                state = SequenceState.ApproachPlayer;
                break;

            case SequenceState.ApproachPlayer:
                MoveGuardTowardsPlayer();
                break;

            case SequenceState.WaitScoldDialogue:
                if (DialogueRunner.IsDialogueOpen) return;
                state = SequenceState.ReturnHome;
                break;

            case SequenceState.ReturnHome:
                ReturnGuardHome();
                break;

            case SequenceState.WaitLeaveDialogueClose:
                if (DialogueRunner.IsDialogueOpen) return;
                // guard never moved, so only restore his original orientation.
                guard.rotation = guardHomeRotation;
                state = SequenceState.Idle;
                break;
        }
    }

    private void MoveGuardTowardsPlayer()
    {
        Vector3 targetPosition = player.position;
        targetPosition.y = guard.position.y;

        Vector3 direction = targetPosition - guard.position;

        if (direction.magnitude <= stopDistance)
        {
            Debug.Log("[TreeHollow] Guard reached the player.", this);
            StartScoldDialogue();
            state = SequenceState.WaitScoldDialogue;
            return;
        }

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            guard.rotation = Quaternion.Slerp(guard.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        guard.position = Vector3.MoveTowards(guard.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void StartScoldDialogue()
    {
        if (DialogueGraphLibrary.Instance == null)
        {
            Debug.LogError("[TreeHollow] DialogueGraphLibrary is unavailable.", this);
            state = SequenceState.ReturnHome;
            return;
        }

        DialogueGraphLibrary.Instance.StartDialogue(scoldDialogueID, guard);
    }

    private void ReturnGuardHome()
    {
        guard.position = Vector3.MoveTowards(guard.position, guardHomePosition, moveSpeed * Time.deltaTime);
        guard.rotation = Quaternion.Slerp(guard.rotation, guardHomeRotation, rotationSpeed * Time.deltaTime);

        float distance = Vector3.Distance(guard.position, guardHomePosition);
        float angle = Quaternion.Angle(guard.rotation, guardHomeRotation);

        if (distance > 0.02f || angle > 1f) return;

        // snap exactly back to where he started.
        guard.position = guardHomePosition;
        guard.rotation = guardHomeRotation;

        Debug.Log("[TreeHollow] Guard returned to original position.", this);

        if (pursuit != null) pursuit.enabled = pursuitWasEnabled;

        state = SequenceState.Idle;
        StartKeyFoundDialogue();
    }

    private void StartKeyFoundDialogue()
    {
        if (DialogueGraphLibrary.Instance == null) return;

        DialogueGraphLibrary.Instance.StartDialogue(keyFoundDialogueID, null);
    }
}