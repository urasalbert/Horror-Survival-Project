using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareScream : MonoBehaviour
{
    public static JumpscareScream Instance { get; private set; }

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
    public void PlayJumpScareSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
    public void StopJumpScareSound()
    {
        AudioSource.Stop();
    }
}

