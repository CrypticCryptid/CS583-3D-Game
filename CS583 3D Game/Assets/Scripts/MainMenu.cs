using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader fader;
    [SerializeField] private string gameSceneName = "Map"; // change if needed

    public void PlayGame()
    {
    if (fader != null)
        fader.FadeToScene(gameSceneName);
    else
        SceneManager.LoadScene(gameSceneName);
    }
    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
