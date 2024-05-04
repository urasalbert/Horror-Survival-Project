using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundPlayerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isAudioPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isAudioPlayed == false)
        {
            if (other.CompareTag("Player"))
            {
                audioSource.Play();
                isAudioPlayed = true;
            }
        }

    }
}
