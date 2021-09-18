using UnityEngine;

public class SpawnTetrominoes : MonoBehaviour
{
    public GameObject[] tetrominoes;
    private GameObject previewTetromino;
    private GameObject nextTetromino;

    public bool gameStarted = true;
    public bool isGameOver = false;

    private void Start()
    {
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        if (!isGameOver)
        {
            int tetrominoIndex = Random.Range(0, tetrominoes.Length);

            if (gameStarted)
            {
                gameStarted = false;

                nextTetromino = Instantiate(tetrominoes[tetrominoIndex], transform.position, Quaternion.identity);
            }
            else
            {
                previewTetromino.transform.localPosition = transform.position;
                nextTetromino = previewTetromino;
                nextTetromino.GetComponent<Tetromino>().enabled = true;
            }

            ChangePreviewTetromino(tetrominoIndex);
        }
    }

    private void ChangePreviewTetromino(int index)
    {
        previewTetromino = Instantiate(tetrominoes[index], new Vector3(-6.75f, 25f, 0f), Quaternion.identity);
        previewTetromino.GetComponent<Tetromino>().enabled = false;
    }
}