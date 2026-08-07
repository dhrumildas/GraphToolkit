using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class RadiusGizmos : MonoBehaviour
{
    [Header("Fuzzy Near Set")]
    [Tooltip("Point A of the Low membership function.")]
    [SerializeField] private float fullMembershipDistance = 0f;

    [Tooltip("Point B of the Low membership function.")]
    [SerializeField] private float zeroMembershipDistance = 5f;

    [Header("Loiter Tracking")]
    [SerializeField] private float loiterRadius = 6f;

    [Header("Display")]
    [SerializeField] private bool showAlways = true;

    private void OnDrawGizmos()
    {
        if (!enabled)
            return;

        if (!showAlways)
            return;

        DrawRadii();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (showAlways) return;
    //    DrawRadii();
    //}

    private void DrawRadii()
    {
        Vector3 centre = transform.position;

        DrawMembershipRing(centre, 0.00f);
        DrawMembershipRing(centre, 0.25f);
        DrawMembershipRing(centre, 0.50f);
        DrawMembershipRing(centre, 0.75f);

        // crisp loiter accumulation boundary.
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centre, loiterRadius);

#if UNITY_EDITOR
        Handles.Label(centre + Vector3.forward * loiterRadius, $"Loiter boundary: {loiterRadius:0.##}m");
#endif
    }

    private void DrawMembershipRing(Vector3 centre, float membership)
    {
        float radius = Mathf.Lerp(zeroMembershipDistance, fullMembershipDistance, membership);

        Gizmos.color = new Color(1f, 0.7f, 0f, 1f);
        Gizmos.DrawWireSphere(centre, radius);

#if UNITY_EDITOR
        Handles.Label(centre + Vector3.forward * radius, $"Near μ = {membership:0.00}");
#endif
    }
}