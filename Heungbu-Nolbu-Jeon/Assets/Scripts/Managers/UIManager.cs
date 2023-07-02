using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseObj;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseObj.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseObj.SetActive(false);
        }
    }

    public void OnClickResumeButton()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseObj.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseObj.SetActive(false);
        }
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("StoryScene");
    }
}
