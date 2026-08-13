using UnityEngine;
using UnityEngine.InputSystem;

public class PitQTE : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject qteCanvas;
    [SerializeField] private TPP_Controller playerController;
    [SerializeField] private Pursuit guardPursuit;

    [SerializeField] private GuardPitFall guardPitFall;
    [SerializeField] private PlayerPitFall playerPitFall;
    [Header("Slow Motion")]
    [SerializeField, Range(0.05f, 1f)]
    private float slowMotionScale = 0.3f;

    private bool playerInside;
    private bool qteRunning;

    private float previousTimeScale = 1f;
    private bool pursuitWasEnabled;

    private void Start()
    {
        if (qteCanvas != null)
            qteCanvas.SetActive(false);
    }

    private void Update()
    {
        if (qteRunning)
            return;

        if (!playerInside)
            return;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            BeginQTE();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TPP_Controller player =
            other.GetComponentInParent<TPP_Controller>();

        if (player == playerController)
        {
            playerInside = true;
            Debug.Log("[PitQTE] Player entered QTE zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TPP_Controller player =
            other.GetComponentInParent<TPP_Controller>();

        if (player == playerController)
        {
            playerInside = false;
            Debug.Log("[PitQTE] Player left QTE zone.");
        }
    }

    private void BeginQTE()
    {
        if (qteRunning)
            return;

        qteRunning = true;

        Debug.Log("[PitQTE] SPACE pressed inside zone - QTE START.");

        if (guardPursuit != null)
        {
            pursuitWasEnabled = guardPursuit.enabled;
            guardPursuit.enabled = false;
        }

        previousTimeScale = Time.timeScale;
        Time.timeScale = slowMotionScale;

        if (qteCanvas != null)
            qteCanvas.SetActive(true);
    }

    public void ResolveQTE(bool success)
    {
        if (!qteRunning)
            return;

        Debug.Log("[PitQTE] ResolveQTE received: " + success);

        qteRunning = false;

        // QTE is finished regardless of result.
        Time.timeScale =
            previousTimeScale > 0f
                ? previousTimeScale
                : 1f;

        if (qteCanvas != null)
            qteCanvas.SetActive(false);

        if (success)
        {
            Debug.Log("[PitQTE] SUCCESS - forcing guard into pit.");

            if (guardPitFall != null)
                guardPitFall.BeginFall();
        }
        else
        {
            Debug.Log("[PitQTE] FAIL - player falls into pit.");

            if (guardPursuit != null)
                guardPursuit.enabled = false;

            if (playerPitFall != null)
                playerPitFall.BeginFall();
            else
                Debug.LogError("[PitQTE] PlayerPitFall reference missing!");
        }
    }
}