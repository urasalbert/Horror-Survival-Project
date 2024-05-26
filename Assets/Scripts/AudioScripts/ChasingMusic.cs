using System;
using System.Collections;
using UnityEngine;

public class ChasingMusic : MonoBehaviour
{
    public static ChasingMusic Instance { get; private set; }

    public AudioSource backgroundMusic;
    public AudioClip chaseMusic;
    public AudioClip breathingSound;
    [SerializeField] public float fadeOutDuration = 1.0f;
    private Coroutine fadeOutCoroutine;
    public bool isPlaying;

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

        if (backgroundMusic == null)
        {
            Debug.LogWarning("ChasingMusic: backgroundMusic not assigned!");
        }
    }

    public void PlayChaseMusic()
    {
        if (!isPlaying || backgroundMusic.clip != chaseMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = chaseMusic;
            backgroundMusic.Play();
            isPlaying = true;

            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
                backgroundMusic.volume = 1f; // Reset volume
            }
        }
    }

    public void StopChaseMusic(bool fadeOut)
    {
        if (fadeOut)
        {
            if (fadeOutCoroutine == null)
            {
                fadeOutCoroutine = StartCoroutine(FadeOutMusic());
            }
        }
        else
        {
            backgroundMusic.Stop();
            isPlaying = false;
        }
    }

    public void StopChaseMusicInstant()
    {
        backgroundMusic.Stop();
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
        isPlaying = false;
        backgroundMusic.volume = startVolume;
        fadeOutCoroutine = null; // Reset coroutine reference
    }
}
