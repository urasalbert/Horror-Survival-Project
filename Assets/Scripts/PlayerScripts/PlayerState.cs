using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    public float currentHealth;
    public float maxHealth;

    public AudioSource[] audioSources;

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
        currentHealth = maxHealth;
        // Find all AudioSource components attached to the player
        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(DieAndGameOver());
        }
    }

    public IEnumerator DieAndGameOver()
    {
        // Disable all AudioSource components
        DisableAllAudioSources();
        ChasingMusic.Instance.StopChaseMusicInstant();
        EnemyAttack.Instance.StopAttackSound();
        // Load main menu and gameover canvas with sounds
        FadeInOutScene.Instance.FadeIn();
        PlayerMovement.Instance.speed = 0;
        DieSound.Instance.PlayDieSound();
        yield return GameOverCanvasTextController.Instance.StartCoroutine(GameOverCanvasTextController.Instance.FadeOutTextAndBlackScreen());
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    private void DisableAllAudioSources()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.enabled = false;
        }
    }
}
