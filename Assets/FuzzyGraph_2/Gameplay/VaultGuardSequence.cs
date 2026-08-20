using UnityEngine;
using TMPro;

public class VaultGuardSequence : MonoBehaviour
{
    [Header("Timed Event UI")]
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private TextMeshProUGUI timedEventText;

    [Header("References")]
    [SerializeField] private Transform guard;
    [SerializeField] private Transform player;
    [SerializeField] private ConversationFacing guardFacing;
    [SerializeField] private ConversationFacing playerFacing;
    [SerializeField] private Transform confrontationPoint;

    [Header("Final Confrontation Event")]
    [SerializeField] private string finalConfrontationEvent;

    [Header("Arrival")]
    [SerializeField] private float arrivalDuration = 2.3f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;
    private bool arriving;

    [Header("Leave Sequence")]
    [SerializeField] private Transform guardDoorPoint;
    [SerializeField] private Transform guardExitPoint;
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private float leaveMoveSpeed = 3.5f;
    [SerializeField] private float leaveRotationSpeed = 8f;
    [SerializeField] private string leaveDialogueID = "GuardVaultLeave";
    [SerializeField] private string guardFinalDialogue = "GuardVaultChase";
    [Header("Chase Scene")]
    [SerializeField] private GameObject disableDuringChaseA;
    [SerializeField] private GameObject disableDuringChaseB;

    private enum LeaveState
    {
        None,
        WaitDialogue,
        MoveToDoor,
        MoveOut
    }

    private LeaveState leaveState = LeaveState.None;

    public void BeginArrival()
    {
        if (arriving || guard == null || player == null)
            return;

        guard.gameObject.SetActive(true);

        startPosition = guard.position;
        targetPosition = confrontationPoint.position;

        timer = 0f;
        arriving = true;

        if (timerCanvas != null)
            timerCanvas.SetActive(true);

        if (timedEventText != null)
        {
            timedEventText.gameObject.SetActive(true);
            timedEventText.text = $"GUARD ARRIVING: {arrivalDuration:0.0}";
        }

        if (guardFacing != null)
            guardFacing.BeginFacing(player);

        Debug.Log("[VaultGuard] Arrival started.");
    }

    private void Update()
    {
        if (arriving)
        {
            timer += Time.deltaTime;

            if (timedEventText != null)
            {
                float remaining = Mathf.Max(0f, arrivalDuration - timer);

                timedEventText.text = $"GUARD ARRIVING: {remaining:0.0}";
            }

            float t = Mathf.Clamp01(timer / arrivalDuration);

            guard.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
                FinishArrival();
        }

        if (leaveState == LeaveState.WaitDialogue)
        {
            if (!DialogueRunner.IsDialogueOpen)
                leaveState = LeaveState.MoveToDoor;

            return;
        }

        if (leaveState == LeaveState.MoveToDoor)
        {
            MoveGuardTowards(guardDoorPoint.position);

            if (Vector3.Distance(
                    guard.position,
                    guardDoorPoint.position) <= 0.05f)
            {
                if (exitDoor != null)
                    exitDoor.SetActive(false);

                leaveState = LeaveState.MoveOut;
            }

            return;
        }

        if (leaveState == LeaveState.MoveOut)
        {
            MoveGuardTowards(guardExitPoint.position);

            if (Vector3.Distance(guard.position,guardExitPoint.position) <= 0.05f)
            {
                guard.gameObject.SetActive(false);

                leaveState = LeaveState.None;

                Debug.Log("[VaultGuard] Guard unlocked the door and left.");
            }
        }
    }

    private void FinishArrival()
    {
        arriving = false;

        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        if (timedEventText != null)
            timedEventText.gameObject.SetActive(false);
        
        guard.position = targetPosition;

        if (guardFacing != null)
            guardFacing.BeginFacing(player);

        if (playerFacing != null)
            playerFacing.BeginFacing(guard);

        Debug.Log("[VaultGuard] Guard reached player.");

        if (FG2GameServices.Instance != null)
        {
            FG2GameServices.Instance.RaiseEvent(
                finalConfrontationEvent,
                guard);
        }
    }

    public void StopFacing()
    {
        if (guardFacing != null)
            guardFacing.StopFacing();

        if (playerFacing != null)
            playerFacing.StopFacing();
    }

    public void BeginGuardLeave()
    {
        StopFacing();

        if (DialogueGraphLibrary.Instance != null)
        {
            DialogueGraphLibrary.Instance.StartDialogue(
                leaveDialogueID,
                guard);
        }

        leaveState = LeaveState.WaitDialogue;
    }

    private void MoveGuardTowards(Vector3 destination)
    {
        destination.y = guard.position.y;

        Vector3 direction =
            destination - guard.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(
                    direction.normalized);

            guard.rotation =
                Quaternion.Slerp(
                    guard.rotation,
                    targetRotation,
                    leaveRotationSpeed * Time.deltaTime);
        }

        guard.position =
            Vector3.MoveTowards(
                guard.position,
                destination,
                leaveMoveSpeed * Time.deltaTime);
    }

    public void BeginGuardChase()
    {
        StopFacing();

        if (disableDuringChaseA != null)
            disableDuringChaseA.SetActive(false);

        if (disableDuringChaseB != null)
            disableDuringChaseB.SetActive(false);

        if (DialogueGraphLibrary.Instance != null)
        {
            DialogueGraphLibrary.Instance.StartDialogue(
                guardFinalDialogue,
                guard);
        }

        Debug.Log("[VaultGuard] Guard chase sequence started.");
    }

    public void RestoreAfterGuardFall()
    {
        if (disableDuringChaseA != null)
            disableDuringChaseA.SetActive(true);

        if (disableDuringChaseB != null)
            disableDuringChaseB.SetActive(true);

        Debug.Log("[VaultGuard] Chase objects restored.");
    }
}