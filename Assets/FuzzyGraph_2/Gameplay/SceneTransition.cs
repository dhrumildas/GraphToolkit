using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneTransition : MonoBehaviour
{
    [SerializeField] private string vaultSceneName = "Vault";

    public void LoadVault()
    {
        Debug.Log("[SceneTransition] Loading Vault scene.");
        SceneManager.LoadScene(vaultSceneName);
    }
}