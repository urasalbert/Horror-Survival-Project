using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private CanvasGroup Canvas;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public static Blink Instance { get; private set; }
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
    public void ShowIU()
    {
        fadeIn = true;
    }
    public void HideUI()
    {
        fadeOut = true;
    }

    void Update()
    {
        if (fadeIn)
        {
            if (Canvas.alpha < 1)
            {
                Canvas.alpha += Time.deltaTime;
                {
                    if (Canvas.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
            }
        }
        if (fadeOut)
        {
            if (Canvas.alpha >= 0)
            {
                Canvas.alpha -= Time.deltaTime * 1.5f;
                {
                    if (Canvas.alpha == 0)
                    {
                        fadeOut = false;
                    }
                }
            }
        }
    }
}
