using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBreathingWhileAttacking : MonoBehaviour
{
    public static StopBreathingWhileAttacking Instance { get; private set; }

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
    public void PlayIdleSound()
    {
        AudioSource.Stop();
        AudioSource.clip = AudioClip;
        AudioSource.Play();

    }
    public void StopIdleSound()
    {
        AudioSource.Stop();
    }
}
