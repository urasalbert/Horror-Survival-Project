using UnityEngine;
using TMPro;
using System.Collections;

public class TriggerTextVisibility : MonoBehaviour
{
    public GameObject textObject;
    public float fadeDuration = 2.0f;
    private bool hasTriggered = false;
    [SerializeField]private TextMeshProUGUI textMeshProComponent;

    void Start()
    {
        if (textMeshProComponent == null && textMeshProComponent == null)
        {
            Debug.LogError("Text or TextMeshPro component not found on the textObject.");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !hasTriggered)
        {
            textObject.SetActive(true);
            hasTriggered = true;
            StartCoroutine(FadeOutText());
        }
    }
    IEnumerator FadeOutText()
    {
        float startAlpha = 1.0f;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        if (textMeshProComponent != null)
        {
            Color originalColor = textMeshProComponent.color;
            while (progress < 1.0f)
            {
                Color tempColor = originalColor;
                tempColor.a = Mathf.Lerp(startAlpha, 0, progress);
                textMeshProComponent.color = tempColor;
                progress += rate * Time.deltaTime;
                yield return null;
            }
            textMeshProComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }
        else if (textMeshProComponent != null)
        {
            Color originalColor = textMeshProComponent.color;
            while (progress < 1.0f)
            {
                Color tempColor = originalColor;
                tempColor.a = Mathf.Lerp(startAlpha, 0, progress);
                textMeshProComponent.color = tempColor;
                progress += rate * Time.deltaTime;
                yield return null;
            }
            textMeshProComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }

        textObject.SetActive(false);
    }
}
