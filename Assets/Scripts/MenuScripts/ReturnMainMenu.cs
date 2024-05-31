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
            
            StartCoroutine(ReturnToMainMenuAfterDelay());
            
        }
    }

    private IEnumerator ReturnToMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(150f);
        FadeInOutScene.Instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
