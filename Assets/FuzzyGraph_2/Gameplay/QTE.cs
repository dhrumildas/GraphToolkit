using UnityEngine;
using UnityEngine.InputSystem;

public class QTE : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private RectTransform pointer;
    [SerializeField] private RectTransform pointA;
    [SerializeField] private RectTransform pointB;
    [SerializeField] private RectTransform safeZone;
    [SerializeField] private float moveSpeed = 500f;

    [Header("Result")]
    [SerializeField] private PitQTE pitQTE;

    private Vector3 target;
    private bool resolved;

    private void OnEnable()
    {
        resolved = false;

        if (pointer == null ||
            pointA == null ||
            pointB == null)
            return;

        pointer.position = pointA.position;
        target = pointB.position;

        Debug.Log("[QTE] Enabled. Press E to stop pointer.");
    }

    private void Update()
    {
        if (resolved)
            return;

        if (pointer == null ||
            pointA == null ||
            pointB == null ||
            safeZone == null)
            return;

        MovePointer();

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("[QTE] E PRESSED.");
            CheckResult();
        }
    }

    private void MovePointer()
    {
        pointer.position =
            Vector3.MoveTowards(
                pointer.position,
                target,
                moveSpeed * Time.unscaledDeltaTime);

        if (Vector3.Distance(pointer.position, pointB.position) < 1f)
        {
            target = pointA.position;
        }
        else if (Vector3.Distance(pointer.position, pointA.position) < 1f)
        {
            target = pointB.position;
        }
    }

    private void CheckResult()
    {
        resolved = true;

        Vector3 localPointerPosition =
            safeZone.InverseTransformPoint(pointer.position);

        bool success =
            safeZone.rect.Contains(localPointerPosition);

        Debug.Log(
            success
                ? "[QTE] SUCCESS."
                : "[QTE] FAILED.");

        if (pitQTE != null)
        {
            pitQTE.ResolveQTE(success);
        }
        else
        {
            Debug.LogError("[QTE] PitQTE reference is missing!");
        }
    }
}