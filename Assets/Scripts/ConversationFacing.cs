using UnityEngine;

public class ConversationFacing : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 8f;

    private Transform target;

    public void BeginFacing(Transform newTarget)
    {
        target = newTarget;
    }

    public void StopFacing()
    {
        target = null;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction =
            target.position - transform.position;

        // Rotate only horizontally.
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}