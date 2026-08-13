using System.Collections;
using UnityEngine;

public class VaultDoorSequence : MonoBehaviour
{
    [SerializeField] private GameObject minigameCanvas;
    [SerializeField] private VaultLockMinigame lockMinigame;
    [SerializeField] private GameObject vaultDoor;

    [Header("Noise check event")]
    [SerializeField] private string noiseCheckEvent;

    [Header("Alert check event")]
    [SerializeField] private string alertCheckEvent;

    public void BeginLockAfterDialogue()
    {
        StartCoroutine(BeginLockRoutine());
    }

    public void OpenDoorAfterDialogue()
    {
        StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator BeginLockRoutine()
    {
        while (DialogueRunner.BlocksWorldInteraction)
            yield return null;

        minigameCanvas.SetActive(true);
        lockMinigame.BeginLock();
    }

    private IEnumerator OpenDoorRoutine()
    {
        while (DialogueRunner.BlocksWorldInteraction)
            yield return null;

        FG2GameServices.Instance.RaiseEvent(noiseCheckEvent, transform);
        FG2GameServices.Instance.RaiseEvent(alertCheckEvent, transform);

        vaultDoor.SetActive(false);
    }
}