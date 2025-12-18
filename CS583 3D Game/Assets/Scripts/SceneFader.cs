using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;        // full-screen black Image
    public float fadeDuration = 1f;
    public float startDelay = 0.5f;

    void Start()
    {
        // Make sure we start fully black
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;

            // Fade in when the scene starts
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        // optional delay before we start fading in
        yield return new WaitForSeconds(startDelay);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            Color c = fadeImage.color;
            c.a = a;
            fadeImage.color = c;
            yield return null;
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            Color c = fadeImage.color;
            c.a = a;
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
