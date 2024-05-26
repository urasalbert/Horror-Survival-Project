using UnityEngine;

public class StopBreathingWhileAttacking : MonoBehaviour
{
    public static StopBreathingWhileAttacking Instance { get; private set; }

    public AudioSource idleSoundSource;

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

        if (idleSoundSource == null)
        {
            Debug.LogWarning("StopBreathingWhileAttacking: idleSoundSource not assigned!");
        }
    }

    public void PlayIdleSound()
    {
        if (!idleSoundSource.isPlaying)
        {
            idleSoundSource.Play();
        }
    }

    public void StopIdleSound()
    {
        if (idleSoundSource.isPlaying)
        {
            idleSoundSource.Stop();
        }
    }
}
