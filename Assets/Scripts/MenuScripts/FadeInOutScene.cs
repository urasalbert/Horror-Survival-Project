using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScene : MonoBehaviour
{
    public static FadeInOutScene Instance { get; private set; }

    public CanvasGroup canvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;

    public float TimeToFade;
    private void Awake()
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
    void Update()
    {
        if(fadeIn == true)
        {
            if(canvasGroup.alpha <1)
            {
                canvasGroup.alpha += TimeToFade * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= TimeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }

    }
    public void FadeIn()
    {
        fadeIn = true;
    }
    public void FadeOut()
    {
        fadeOut = true;
    }
}
