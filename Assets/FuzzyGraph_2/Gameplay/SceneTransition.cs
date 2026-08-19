using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneTransition : MonoBehaviour
{
    [SerializeField] private string vaultSceneName = "Vault";
    [SerializeField] private string bazaarSceneName = "Bazaar_3.2";
    [SerializeField] private string mainMenuSceneName = "Menu";

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

    public void StartNewGame()
    {
        Time.timeScale = 1f;

        FG2GameServices.DestroyPersistentInstance();

        Debug.Log("[SceneTransition] Starting completely fresh game.");
        SceneManager.LoadScene(bazaarSceneName);
    }

    

    public void QuitGame()
    {
        Debug.Log("[SceneTransition] Quitting game.");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

}