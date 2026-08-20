using UnityEngine;

public class ReturnHandle : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private GameObject guard;
    [SerializeField] private Transform player;
    [SerializeField] private Transform vaultEntranceSpawn;

    public void ReturnFromVaultGate()
    {
        // Guard fell into the pit in Vault,
        // therefore he should not exist back in Bazaar.
        if (guard != null)
            guard.SetActive(false);

        // Put player outside the Vault entrance.
        if (player != null && vaultEntranceSpawn != null)
        {
            CharacterController controller =
                player.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            player.position = vaultEntranceSpawn.position;
            player.rotation = vaultEntranceSpawn.rotation;

            if (controller != null)
                controller.enabled = true;
        }

        Debug.Log(
            "[BazaarReturn] Returned through Vault gate. Guard removed and player repositioned.");
    }
}