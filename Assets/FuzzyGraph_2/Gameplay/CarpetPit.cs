using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CarpetPit : MonoBehaviour
{
    [Header("Timed Event UI")]
    [SerializeField] private TextMeshProUGUI timedEventText;

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

    [Header("Quiet Vendor Reveal")]
    [SerializeField] private RadiusGizmos carpetContext;

    private Coroutine quietRevealRoutine;

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

        timerRoutine = StartCoroutine(WaitForChoice());
    }

    private IEnumerator WaitForChoice()
    {
        // Wait while the opening dialogue is being read.
        while (DialogueRunner.IsDialogueOpen && !DialogueRunner.IsChoiceOpen)
            yield return null;

        // Dialogue somehow ended before reaching the choice.
        if (!DialogueRunner.IsDialogueOpen)
            yield break;

        // NOW the Jump / Excuse choice is actually visible.
        yield return ResponseTimer();
    }

    private IEnumerator ResponseTimer()
    {
        float timeRemaining = responseTime;

        if (timedEventText != null)
        {
            timedEventText.gameObject.SetActive(true);
            timedEventText.text = $"DECIDE: JUMP OR EXCUSE  {timeRemaining:0.0}";
        }

        while (timeRemaining > 0f)
        {
            if (resolved)
            {
                HideTimedText();
                yield break;
            }

            timeRemaining -= Time.deltaTime;

            if (timedEventText != null)
            {
                timedEventText.text =
                    $"DECIDE: JUMP OR EXCUSE  {Mathf.Max(0f, timeRemaining):0.0}";
            }

            yield return null;
        }

        if (resolved)
        {
            HideTimedText();
            yield break;
        }

        resolved = true;

        HideTimedText();

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

    private void HideTimedText()
    {
        if (timedEventText != null)
            timedEventText.gameObject.SetActive(false);
    }

    // gameplaysignal: pit.jumpchosen
    public void ChooseJump()
    {
        if (resolved) return;

        resolved = true;

        if (timerRoutine != null) StopCoroutine(timerRoutine);

        HideTimedText();

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

        HideTimedText();

        Debug.Log("[CarpetPit] Player chose to make an excuse.");
    }

    public void RevealQuietlyAfterDialogue()
    {
        if (quietRevealRoutine != null)
            StopCoroutine(quietRevealRoutine);

        quietRevealRoutine = StartCoroutine(QuietRevealRoutine());
    }

    private IEnumerator QuietRevealRoutine()
    {
        // Choice has just been pressed:
        // force the vendor to maximum reaction.
        if (carpetContext != null)
            carpetContext.ShowMaximumReaction();

        // D44 is still playing.
        while (DialogueRunner.IsDialogueOpen)
            yield return null;

        // D44 has ended.
        if (carpetContext != null)
            carpetContext.FinishContext();

        if (carpet != null)
            carpet.SetActive(false);

        if (hiddenPitEntrance != null)
            hiddenPitEntrance.SetActive(true);

        quietRevealRoutine = null;

        Debug.Log("[CarpetPit] Quiet vendor reveal complete.");
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