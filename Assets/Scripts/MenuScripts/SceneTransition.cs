using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] public float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup != null)
        {
            float startAlpha = fadeCanvasGroup.alpha;
            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float t = (Time.time - startTime) / fadeDuration;

                if (fadeCanvasGroup != null)
                {
                    fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                    elapsedTime = Time.time - startTime;
                    yield return null;
                }
                else
                {
                    yield break;
                }
            }

            if (fadeCanvasGroup != null)
            {
                fadeCanvasGroup.alpha = targetAlpha;
            }
        }
    }


    public IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return Fade(1f);
        SceneManager.LoadScene(sceneName);
    }
}
