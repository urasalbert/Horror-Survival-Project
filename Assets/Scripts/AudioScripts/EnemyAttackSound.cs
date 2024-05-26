using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static EnemyAttack Instance { get; private set; }

    public AudioSource soundEffectPlayer;
    public AudioClip attackSound;

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

    public void PlayAttackSound()
    {
        soundEffectPlayer.Stop();
        soundEffectPlayer.clip = attackSound;
        soundEffectPlayer.Play();
    }

    public void StopAttackSound()
    {
        soundEffectPlayer.Stop();
    }

}

