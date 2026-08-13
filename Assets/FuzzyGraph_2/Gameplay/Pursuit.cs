using FuzzyGraph.Runtime;
using UnityEngine;

public class Pursuit : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private Transform player;
    [SerializeField] private CharacterController playerController;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 8f;

    [Header("Arrest")]
    [SerializeField] private float arrestDistance = 1.5f;

    private bool arrested;

    private void Update()
    {
        if (arrested) return;

        if (FG2GameServices.Instance == null || FG2GameServices.Instance.Context == null) return;

        if (!FG2GameServices.Instance.Context.TryGet("Guard.PursuePlayer", out FuzzyValue value)) return;

        if (value.type != FuzzyValueType.Bool || !value.boolVal) return;

        if (DialogueRunner.IsDialogueOpen)
            return;

        if (player == null) return;

        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = targetPosition - transform.position;

        bool playerIsGrounded = playerController == null || playerController.isGrounded;

        if (direction.magnitude <= arrestDistance && playerIsGrounded)
        {
            arrested = true;
            Debug.Log("[Pursuit] player reached arrest radius.");

            FG2GameServices.Instance.RaiseEvent("GuardArrestPlayer", transform);
            return;
        }

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}