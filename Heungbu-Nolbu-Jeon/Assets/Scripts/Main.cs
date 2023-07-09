using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void OnClickDescriptionButton()
    {
        SceneManager.LoadScene("DescriptionScene");
    }

    public void OnClickGoBackMainButton()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
