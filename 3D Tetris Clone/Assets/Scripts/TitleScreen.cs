using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Gameplay";

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(gameplaySceneName);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
