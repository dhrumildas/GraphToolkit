using System.Collections;
using UnityEngine;

public class ReturnCheck : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Wait one frame so the Bazaar SignalRouter is ready.
        yield return null;

        if (FG2GameServices.Instance != null)
        {
            Debug.Log("[BazaarReturnCheck] Checking return state.");

            FG2GameServices.Instance.RaiseEvent(
                "BazaarReturnCheck",
                transform);
        }
    }
}