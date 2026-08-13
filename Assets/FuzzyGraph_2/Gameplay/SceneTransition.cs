using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneTransition : MonoBehaviour
{
    [SerializeField] private string vaultSceneName = "Vault";
    [SerializeField] private string bazaarSceneName = "Bazaar_3.2";

    public void LoadVault()
    {
        Debug.Log("[SceneTransition] Loading Vault scene.");
        SceneManager.LoadScene(vaultSceneName);
    }

    public void LoadBazaar()
    {
        Debug.Log("[SceneTransition] Loading Bazaar scene.");
        SceneManager.LoadScene(bazaarSceneName);
    }
}