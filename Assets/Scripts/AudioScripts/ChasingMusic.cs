using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMusic : MonoBehaviour
{

    public static ChasingMusic Instance { get; private set; }

    public AudioSource backgroundMusic;
    public AudioClip chaseMusic;
    public AudioClip breathingSound;
    [NonSerialized]public float fadeOutDuration = 1.0f;
    private Coroutine fadeOutCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayChaseMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = chaseMusic;
        backgroundMusic.Play();
    }

    public void StopChaseMusicWithFade()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = backgroundMusic.volume;
        float timer = 0f;

        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            backgroundMusic.volume = Mathf.Lerp(startVolume, 0f, timer / fadeOutDuration);
            yield return null;
        }

        backgroundMusic.Stop();
        backgroundMusic.volume = startVolume;
    }
}

