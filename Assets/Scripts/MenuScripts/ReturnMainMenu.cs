using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    private bool playerEnteredTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredTrigger = true;
            
            StartCoroutine(ReturnToMainMenuAfterDelay(150f));
            
        }
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FadeInOutScene.Instance.FadeIn();
        yield return new WaitForSeconds(delay + 2);
        SceneManager.LoadScene("MainMenu");
    }
}
