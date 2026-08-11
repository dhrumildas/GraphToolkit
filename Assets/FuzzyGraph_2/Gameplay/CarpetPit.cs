using System.Collections;
using UnityEngine;

public class CarpetPit : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private GameObject carpet;
    [SerializeField] private GameObject hiddenPitEntrance;
    [SerializeField] private Transform vendor;

    [Header("Dialogue")]
    [SerializeField] private string confrontationDialogueID = "VendorPitConfrontation";

    [Header("QTE")]
    [SerializeField] private float responseTime = 5f;

    [Header("Optional Jump")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform pitLandingPoint;

    private Coroutine timerRoutine;
    private bool resolved;
    private bool excuseChosen;

    // called by gameplaysignal: pit.revealed
    public void BeginConfrontation()
    {
        resolved = false;
        excuseChosen = false;

        if (carpet != null) carpet.SetActive(false);
        if (hiddenPitEntrance != null) hiddenPitEntrance.SetActive(true);

        if (DialogueGraphLibrary.Instance != null)
        {
            DialogueGraphLibrary.Instance.StartDialogue(confrontationDialogueID, vendor);
        }

        if (timerRoutine != null) StopCoroutine(timerRoutine);

        timerRoutine = StartCoroutine(ResponseTimer());
    }

    private IEnumerator ResponseTimer()
    {
        yield return new WaitForSeconds(responseTime);

        if (resolved) yield break;

        resolved = true;

        Debug.Log("[CarpetPit] Player failed to answer in time.");

        if (DialogueRunner.Instance != null && DialogueRunner.IsDialogueOpen)
        {
            DialogueRunner.Instance.EndDialogue();
        }

        if (FG2GameServices.Instance != null)
        {
            FG2GameServices.Instance.RaiseEvent("VendorPitTimeout", vendor);
        }
    }

    // gameplaysignal: pit.jumpchosen
    public void ChooseJump()
    {
        if (resolved) return;

        resolved = true;

        if (timerRoutine != null) StopCoroutine(timerRoutine);

        if (DialogueRunner.Instance != null && DialogueRunner.IsDialogueOpen)
        {
            DialogueRunner.Instance.EndDialogue();
        }

        if (player != null && pitLandingPoint != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null) controller.enabled = false;

            player.position = pitLandingPoint.position;

            if (controller != null) controller.enabled = true;
        }

        Debug.Log("[CarpetPit] Player jumped into the pit.");
    }

    // gameplaysignal: pit.excusechosen
    public void ChooseExcuse()
    {
        if (resolved) return;

        resolved = true;
        excuseChosen = true;

        if (timerRoutine != null) StopCoroutine(timerRoutine);

        Debug.Log("[CarpetPit] Player chose to make an excuse.");
    }

    private void Update()
    {
        // excuse dialogue has finished. now trigger the guard response.
        if (!excuseChosen) return;
        if (DialogueRunner.IsDialogueOpen) return;

        excuseChosen = false;

        if (FG2GameServices.Instance != null)
        {
            FG2GameServices.Instance.RaiseEvent("VendorCallsGuard", vendor);
        }
    }
}