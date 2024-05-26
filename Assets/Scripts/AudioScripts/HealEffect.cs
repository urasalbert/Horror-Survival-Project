using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour
{

    public static HealEffect Instance { get; private set; }

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
    public void PlayHealSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
    public void StopHealSound()
    {
        AudioSource.Stop();
    }
}
