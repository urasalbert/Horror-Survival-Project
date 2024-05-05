using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerSightSound : MonoBehaviour
{
    public static TrackerSightSound Instance { get; private set; }

    public AudioSource AudioSource;
    public AudioClip sightSoundEffect;
    public bool isPlayed;

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
        isPlayed = false;
    }

    public void PlaySightSound()
    {
        if (!isPlayed)
        {
            AudioSource.Stop();
            AudioSource.clip = sightSoundEffect;
            AudioSource.Play();
            isPlayed = true;
        }
    }
    public void StopSightSound()
    {
        AudioSource.Stop();
    }

}
