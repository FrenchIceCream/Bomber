using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioSource winSourse;
    [SerializeField] private AudioSource loseSourse;
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
            text.text = "Пауза";
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
        text.text = "Победа!";
    }

    private void LostUI()
    {
        gameObject.SetActive(true);
        loseSourse.Play();
        text.text = "Поражение...";
    }
}
