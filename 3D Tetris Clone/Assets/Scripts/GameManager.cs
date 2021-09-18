using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int score = 0;
    public Text scoreText;

    public GameObject menuPanel;
    public Text panelText;

    public AudioSource audioSource;
    public AudioClip scoreSFX;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    void Update()
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

    public void PauseGame()
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

    public void GameOver()
    {
        menuPanel.SetActive(true);
        panelText.text = "Game Over";
        Time.timeScale = 0;
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
}
