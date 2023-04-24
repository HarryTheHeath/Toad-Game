using System.Collections;
using TMPro;
using UnityEngine;

public class WaveScoreUI : MonoBehaviour
{
    public float _AudioDelay;
    public AudioSource _Audio;
    public TextMeshProUGUI _WaveNumber;
    public Animator _Animator;
    
    private int _numPlayed = 0;
    
    public void AnnounceNewWave(int waveNumber)
    {
        Debug.Log("wave: " + waveNumber);

        var wave = waveNumber + 1;
        _WaveNumber.text = $"Round # {wave.ToString()}";
        
        _Animator.SetTrigger("show");
        
        if(waveNumber == 0) return;
        StartCoroutine(PlayAudioClip(waveNumber));
    }
    
    IEnumerator PlayAudioClip(int numPlays)
    {
        _Audio.PlayDelayed(_AudioDelay);
        while (_Audio.isPlaying)
        {
            yield return null;
        }
        _numPlayed++;
        if (_numPlayed < numPlays)
        {
            StartCoroutine(PlayAudioClip(numPlays));
        }
    }
}
