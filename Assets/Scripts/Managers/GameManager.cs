using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Animator Menu;
    public TextMeshProUGUI Highscore;
    public SceneFader Fader;
    
    private NewWaveSpawner WaveSpawner;

    private void Start()
    {
        WaveSpawner = FindObjectOfType<NewWaveSpawner>();
    }

    [ContextMenu("OnDeath")]
    public void OnDeath()
    {
        //Pause game
        //?

        Highscore.text = WaveSpawner.CurrentWave.ToString();

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