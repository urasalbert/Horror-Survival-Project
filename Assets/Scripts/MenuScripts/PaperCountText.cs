using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperCountText : MonoBehaviour
{
    public static PaperCountText Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI paperCountText;

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

    public void UpdateScoreText()
    {
        paperCountText.text = PaperCollector.Instance.collectedPaperCount.ToString();
    }
}
