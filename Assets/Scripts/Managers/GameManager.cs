using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        var wave = WaveSpawner.CurrentWave + 1;
        Highscore.text = wave.ToString();

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