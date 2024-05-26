using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    public static FlashlightHandler Instance {get; private set;}
    [SerializeField] private Light FlashlightLight;
    public bool isFlashlightOn;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    private void Awake()
    {
        if(Instance != null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        FlashlightLight.enabled = false;
        isFlashlightOn = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            if (isFlashlightOn) 
            {
                FlashlightLight.enabled = false;
                isFlashlightOn = false;
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            else
            {
                FlashlightLight.enabled = true;
                isFlashlightOn = true;
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
        }
    }

}
