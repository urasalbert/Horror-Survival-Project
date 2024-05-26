using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvasTextController : MonoBehaviour
{
    public static GameOverCanvasTextController Instance { get; private set; }

    public GameObject textObject;
    public GameObject blackScreenObject;
    public float fadeDuration = 2.0f;
    [SerializeField] private TextMeshProUGUI textMeshProComponent;
    [SerializeField] private Image blackScreenImage;

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

        // Get the Image component from the blackScreenObject
        blackScreenImage = blackScreenObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (PlayerState.Instance.currentHealth <= 0 && !textObject.activeInHierarchy && !blackScreenObject.activeInHierarchy)
        {
            StartCoroutine(SetActiveTextAndBlackScreen());
        }
    }

    IEnumerator SetActiveTextAndBlackScreen()
    {
        textObject.SetActive(true);
        blackScreenObject.SetActive(true);

        // Ensure the black screen is fully opaque
        Color blackScreenColor = blackScreenImage.color;
        blackScreenColor.a = 1.0f;
        blackScreenImage.color = blackScreenColor;

        yield return null;
    }

    public IEnumerator FadeOutTextAndBlackScreen()
    {
        float startAlpha = 1.0f;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        if (textMeshProComponent != null && blackScreenImage != null)
        {
            Color originalTextColor = textMeshProComponent.color;
            Color originalBlackColor = blackScreenImage.color;

            while (progress < 1.0f)
            {
                float alpha = Mathf.Lerp(startAlpha, 0, progress);

                // Fade out text
                Color tempTextColor = originalTextColor;
                tempTextColor.a = alpha;
                textMeshProComponent.color = tempTextColor;

                // Fade out black screen
                Color tempBlackColor = originalBlackColor;
                tempBlackColor.a = alpha;
                blackScreenImage.color = tempBlackColor;

                progress += rate * Time.deltaTime;
                yield return null;
            }

            textMeshProComponent.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 0);
            blackScreenImage.color = new Color(originalBlackColor.r, originalBlackColor.g, originalBlackColor.b, 0);
        }

        textObject.SetActive(false);
        blackScreenObject.SetActive(false);
    }
}
