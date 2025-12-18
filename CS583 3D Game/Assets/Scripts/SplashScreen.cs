using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    public SceneFader fader;   // assign in Inspector

    void Update()
    {
        // Only change scenes when the player presses a key or mouse button
        if (Input.anyKeyDown)
        {
            LoadMenu();
        }
    }

    void LoadMenu()
    {
        if (fader != null)
        {
            fader.FadeToScene(mainMenuSceneName);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
