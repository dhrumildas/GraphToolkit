using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VaultLockMinigame : MonoBehaviour
{
    [Header("Lock")]
    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameObject vaultDoor;

    [Header("Timer")]
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private TMP_Text timerText;

    [Header("Events")]
    [SerializeField] private string SuccessEvent;
    [SerializeField] private string FailureEvent;
    [SerializeField] private string noiseCheckEvent;
    [SerializeField] private string alertCheckEvent;

    private readonly List<Color> originalColors = new();

    private List<Button> shuffledButtons;
    private TPP_Controller playerController;

    private int counter;
    private float timeRemaining;
    private bool active;

    private void Awake()
    {
        originalColors.Clear();

        foreach (Button button in buttons)
        {
            originalColors.Add(button.image.color);
        }
    }

    public void BeginLock()
    {
        counter = 0;
        timeRemaining = timeLimit;
        active = true;

        FreezePlayer();

        shuffledButtons = buttons.OrderBy(x => Random.Range(0, 100)).ToList();

        for (int i = 0; i < shuffledButtons.Count; i++)
        {
            Button button = shuffledButtons[i];
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();

            text.text = (i + 1).ToString();
            button.interactable = true;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < originalColors.Count) buttons[i].image.color = originalColors[i];
        }

        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
            Button capturedButton = button;
            button.onClick.AddListener(() => PressButton(capturedButton));
        }

        if (timerText != null) timerText.text = timeRemaining.ToString("0.00");

        Debug.Log("[VaultLock] Minigame started.");
    }

    private void Update()
    {
        if (!active) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = Mathf.Max(0f, timeRemaining).ToString("0.00");
        }

        if (timeRemaining <= 0f) FailLock("Timer expired");
    }

    private void PressButton(Button button)
    {
        if (!active) return;

        TMP_Text text = button.GetComponentInChildren<TMP_Text>();

        if (!int.TryParse(text.text, out int number)) return;

        Debug.Log($"[VaultLock] Pressed {number} | Expected {counter + 1}");

        if (number != counter + 1)
        {
            FailLock($"Wrong button: pressed {number}");
            return;
        }

        counter++;
        button.interactable = false;

        if (counter >= buttons.Count) SolveLock();
    }

    private void SolveLock()
    {
        if (!active) return;
        active = false;

        Debug.Log($"[VaultLock] SUCCESS! Solved in {timeLimit - timeRemaining:0.00} seconds.");
        StartCoroutine(PresentResult(true));
    }

    private void FailLock(string reason)
    {
        if (!active) return;
        active = false;

        Debug.Log($"[VaultLock] FAILED! {reason}");
        StartCoroutine(PresentResult(false));
    }

    private IEnumerator PresentResult(bool success)
    {
        Color resultColor = success ? Color.green : Color.red;

        foreach (Button button in buttons)
        {
            button.image.color = resultColor;
            button.interactable = false;
        }

        // show result for 2 seconds
        yield return new WaitForSeconds(2f);

        if (success)
        {
            FG2GameServices.Instance.RaiseEvent(SuccessEvent,transform);
            FG2GameServices.Instance.RaiseEvent(noiseCheckEvent, transform);
            FG2GameServices.Instance.RaiseEvent(alertCheckEvent, transform);
            if (vaultDoor != null) vaultDoor.SetActive(false);
        }
        else
        {
            FG2GameServices.Instance.RaiseEvent(FailureEvent, transform);
        }

        RestorePlayer();

        // hide canvas
        gameObject.SetActive(false);
    }

    private void FreezePlayer()
    {
        if (playerController == null) playerController = FindAnyObjectByType<TPP_Controller>();

        if (playerController != null)
        {
            // match dialogue cursor
            playerController.SetCursorLocked(false);

            // stop movement and camera
            playerController.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void RestorePlayer()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
            playerController.SetCursorLocked(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}