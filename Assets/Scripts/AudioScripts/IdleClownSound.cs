using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleClownSound : MonoBehaviour
{
    public static IdleClownSound Instance { get; private set; }

    public AudioSource AudioSource;
    public AudioClip AudioClip;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(AudioClip);
    }
    public void PlayClownIdleSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
    public void StopClownIdleSound()
    {
        AudioSource.Stop();
    }
}

