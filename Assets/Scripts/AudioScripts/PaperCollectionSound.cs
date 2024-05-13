using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperCollectionSound : MonoBehaviour
{
    public static PaperCollectionSound Instance { get; private set; }

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
    public void PlayPaperSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
    public void StopPaperSound()
    {
        AudioSource.Stop();
    }
}