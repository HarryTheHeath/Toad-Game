using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public AudioClip[] loopingSoundscapes;
    public AudioClip[] loopingMusics;
    public AudioClip[] randomSFXs;
    public float randomSFXMinDelay = 1;
    public float randomSFXMaxDelay = 5;

    public AudioSource soundscape;
    public AudioSource music;
    public AudioSource random;

    private void Start()
    {
        if (loopingSoundscapes.Length > 0)
        {
            soundscape.clip = PickRandom(loopingSoundscapes);
            soundscape.loop = true;
            soundscape.Play();
        }

        if (loopingMusics.Length > 0)
        {
            music.clip = PickRandom(loopingMusics);
            music.loop = true;
            music.Play();
        }

        if (randomSFXs.Length > 0)
        {
            StartCoroutine(PlayRandomSFX(0));
        }
    }

    IEnumerator PlayRandomSFX(float delay)
    {
        float additionalRandomDelay = Random.Range(randomSFXMinDelay, randomSFXMaxDelay);
        yield return new WaitForSeconds(delay + additionalRandomDelay);

        random.clip = PickRandom(randomSFXs);
        random.Play();

        float duration = random.clip.length;

        StartCoroutine(PlayRandomSFX(duration));
    }

    private AudioClip PickRandom(AudioClip[] clips)
    {
        int r = Random.Range(0, clips.Length);
        return clips[r];
    }
}