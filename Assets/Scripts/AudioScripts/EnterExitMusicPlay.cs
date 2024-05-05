using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitMusicPlay : MonoBehaviour
{
    public AudioSource musicSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            musicSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // musicSource.Stop();
        }
    }
}
