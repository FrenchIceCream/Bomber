using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject LoseText;
    [SerializeField] private AudioClip WinSound;
    [SerializeField] private AudioClip LoseSound;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
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
        AudioSource.PlayClipAtPoint(WinSound, transform.position);
        canvas.enabled = true;
        WinText.SetActive(true);
        LoseText.SetActive(false);
    }

    private void LostUI()
    {
        AudioSource.PlayClipAtPoint(LoseSound, transform.position);
        canvas.enabled = true;
        WinText.SetActive(false);
        LoseText.SetActive(true);
    }
}
