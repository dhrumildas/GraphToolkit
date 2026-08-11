using UnityEngine;

public class Arrest : MonoBehaviour
{
    [Header("Disable these when arrested")]
    [SerializeField] private Behaviour[] controlsToDisable;

    private bool arrested;

    public void ArrestPlayer()
    {
        if (arrested)
            return;

        arrested = true;

        foreach (Behaviour behaviour in controlsToDisable)
        {
            if (behaviour != null)
                behaviour.enabled = false;
        }

        Debug.Log("[Arrest] Player controls disabled.");
    }
}