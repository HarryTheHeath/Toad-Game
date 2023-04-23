using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Animator Menu;
    public SceneFader Fader;

    [ContextMenu("OnDeath")]
    public void OnDeath()
    {
        //Pause game
        //?

        ShowHighScoreMenu();

        //Death audio
        FindObjectOfType<BackgroundAudio>()?.OnDeath();
    }

    public void OnRetry()
    {
        Fader.FadeToScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OnQuitToStartMenu()
    {
        Fader.FadeToScene(0);
    }

    private void ShowHighScoreMenu()
    {
        Menu.SetBool("show", true);
    }
}