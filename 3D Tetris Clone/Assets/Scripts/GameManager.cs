using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    public Text scoreText;

    public GameObject menuPanel;
    public TMP_Text panelText;

    public AudioSource audioSource;
    public AudioClip scoreSFX;

    private void Start()
    {
        panelText.text = "Paused";
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
        score++;
        scoreText.text = score.ToString();
        audioSource.PlayOneShot(scoreSFX);
    }

    private void PauseGame()
    {
        panelText.text = "Paused";
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
        panelText.text = "Game Over";
        Time.timeScale = 0;
    }
}
