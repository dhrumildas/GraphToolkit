using System.Collections;
using UnityEngine;
using FuzzyGraph.Runtime;

public class ReturnCheck : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        if (FG2GameServices.Instance == null)
            yield break;

        Debug.Log("[BazaarReturnCheck] Checking return state.");

        FG2GameServices.Instance.RaiseEvent(
            "BazaarReturnCheck",
            transform);

        if (FG2GameServices.Instance.Context.TryGet(
                "Player.EnteredVault",
                out FuzzyValue enteredVault) &&
            enteredVault.type == FuzzyValueType.Bool &&
            enteredVault.boolVal)
        {
            Debug.Log("[BazaarReturnCheck] Player returned from Vault.");

            FG2GameServices.Instance.RaiseEvent(
                "BazaarEndingVendor",
                transform);
        }
    }
}