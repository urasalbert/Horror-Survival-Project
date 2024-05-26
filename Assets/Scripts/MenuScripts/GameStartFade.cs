using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float startAlpha = 1f;
    public float targetAlpha = 0f; 
    public float fadeSpeed = 0.5f; 

    void Start()
    {

        canvasGroup.alpha = startAlpha;
    }

    void Update()
    {

        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }
}
