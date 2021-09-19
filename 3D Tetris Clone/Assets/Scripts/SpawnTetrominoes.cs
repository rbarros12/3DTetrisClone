using UnityEngine;

public class SpawnTetrominoes : MonoBehaviour
{
    public GameObject[] tetrominoes;
    private GameObject previewTetromino;
    private GameObject nextTetromino;
    private Vector3 originalTetrominoScale;
    [SerializeField] private Vector3 previewTetrominoPosition;
    [SerializeField] private Vector3 previewTetrominoScale;

    public bool gameStarted = true;
    public bool isGameOver = false;

    private void Start()
    {
        originalTetrominoScale = Vector3.one;
        previewTetrominoPosition = new Vector3(1.3f, 28f, 0f);
        previewTetrominoScale = new Vector3(0.5f, 0.5f, 0.5f);
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
                previewTetromino.transform.localScale = originalTetrominoScale;
                nextTetromino = previewTetromino;
                nextTetromino.GetComponent<Tetromino>().enabled = true;
            }

            ChangePreviewTetromino(tetrominoIndex);
        }
    }

    private void ChangePreviewTetromino(int index)
    {
        previewTetromino = Instantiate(tetrominoes[index], previewTetrominoPosition, Quaternion.identity);
        previewTetromino.transform.localScale = previewTetrominoScale;
        previewTetromino.GetComponent<Tetromino>().enabled = false;
    }
}