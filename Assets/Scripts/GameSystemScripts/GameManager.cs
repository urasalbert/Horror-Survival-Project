using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]private GameObject GameOverCanvas;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameOverCanvas.SetActive(false);
    }
    private void GameFinisher()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOverCanvas.SetActive(true);
    }
    private void Update()
    {
        if(PlayerState.Instance.currentHealth  == 999)
        {
            GameFinisher();
        }
    }
}
