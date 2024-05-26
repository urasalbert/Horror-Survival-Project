using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameButton()
    {
        StartCoroutine(ScenePlayButton());

    }
    IEnumerator ScenePlayButton()
    {
        FadeInOutScene.Instance.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
       
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
