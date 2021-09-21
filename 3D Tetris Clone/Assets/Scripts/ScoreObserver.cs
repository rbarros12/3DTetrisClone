using UnityEngine;
using TMPro;

public class ScoreObserver : MonoBehaviour
{
    private TMP_Text scoreText;
    private int score;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.scoreChanger += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.scoreChanger -= UpdateScore;
    }

    private void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}