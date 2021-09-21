using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action scoreChanger;

    public GameObject menuPanel;
    public TMP_Text menuPanelText;

    public AudioSource audioSource;
    public AudioClip scoreSFX;

    private void Start()
    {
        menuPanelText.text = "Paused";
        menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void Scored()
    {
        scoreChanger?.Invoke();
        audioSource.PlayOneShot(scoreSFX);
    }

    private void PauseGame()
    {
        menuPanelText.text = "Paused";

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            menuPanel.SetActive(false);
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            menuPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        menuPanel.SetActive(true);
        menuPanelText.text = "Game Over";
        Time.timeScale = 0;
    }
}
