using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSound : MonoBehaviour
{
    public static DieSound Instance { get; private set; }

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
    }
    public void PlayDieSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
}
