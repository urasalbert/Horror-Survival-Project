using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;
    public bool isWalking;

    public float minTimeBetweenSteps;
    public float maxTimeBetweenSteps;

    private float nextStepTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerMovement.Instance.isWalking == true && Time.time >= nextStepTime)
        {
            PlayFootstepSound();
            nextStepTime = Time.time + Random.Range(minTimeBetweenSteps, maxTimeBetweenSteps);
        }
    }

    public void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
