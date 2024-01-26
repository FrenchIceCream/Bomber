using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject LoseText;
    [SerializeField] private AudioSource winSourse;
    [SerializeField] private AudioSource loseSourse;
    [SerializeField] private Sprite pauseButton;
    [SerializeField] private Sprite playButton;
    private bool paused = false;

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
    }

    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

    public void ShowUI(bool won)
    {
        if (won)
            Invoke("WonUI", 0.5f);
        else 
            Invoke("LostUI", 1f);
    }

    private void WonUI()
    {
        gameObject.SetActive(true);
        winSourse.Play();
        WinText.SetActive(true);
        LoseText.SetActive(false);
    }

    private void LostUI()
    {
        gameObject.SetActive(true);
        loseSourse.Play();
        WinText.SetActive(false);
        LoseText.SetActive(true);
    }
}
