using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseCanvas;
    public static PauseScreenManager Instance { get; set; }
    public bool isGamePaused;
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
    void Start()
    {
        ResumeGame();
        isGamePaused = false;
    }

    void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (IsGamePaused())
                {
                    ResumeGame();                   
                    Cursor.visible = false;
                }

                else
                    PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        PauseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        PauseCanvas.SetActive(false);
        Cursor.visible = false;
    }
    private bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
