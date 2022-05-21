using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour
{
    public string gameSceneName = "Game";
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void StartCreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    public void ExitGame()
    {

    }
}
