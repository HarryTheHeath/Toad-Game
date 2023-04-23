using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    private bool fading = false;

    public void FadeToScene(int sceneIndex)
    {
        if (!fading)
        {
            StartCoroutine(FadeOut(sceneIndex));
        }
    }
    
    IEnumerator FadeOut(int sceneIndex)
    {
        fading = true;

        while (fadeImage.color.a < 1)
        {
            float newAlpha = fadeImage.color.a + fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void FadeToQuit()
    {
        if (!fading)
        {
            StartCoroutine(FadeOutToQuit());
        }
    }

    IEnumerator FadeOutToQuit()
    {
        fading = true;

        while (fadeImage.color.a < 1)
        {
            float newAlpha = fadeImage.color.a + fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        Application.Quit();
    }
}