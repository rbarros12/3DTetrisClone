using UnityEngine;

public class SpawnTetrominoes : MonoBehaviour
{
    public GameObject[] tetrominoes;
    private GameObject nextTetromino;
    private GameObject holdTetromino;
    private int tetrominoIndex;

    private Vector3 originalTetrominoScale;
    [SerializeField] private Vector3 nextTetrominoScale;
    [SerializeField] private Vector3 nextTetrominoPosition;

    public bool gameStarted = true;
    public bool isGameOver = false;

    private void Start()
    {
        originalTetrominoScale = Vector3.one;
        nextTetrominoPosition = new Vector3(1.3f, 28f, 0f);
        nextTetrominoScale = new Vector3(0.5f, 0.5f, 0.5f);
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        if (!isGameOver)
        {
            if (gameStarted)
            {
                gameStarted = false;
                tetrominoIndex = Random.Range(0, tetrominoes.Length);
                holdTetromino = Instantiate(tetrominoes[tetrominoIndex], transform.position, Quaternion.identity);
            }
            else
            {
                nextTetromino.transform.localPosition = transform.position;
                nextTetromino.transform.localScale = originalTetrominoScale;
                holdTetromino = nextTetromino;
                holdTetromino.GetComponent<Tetromino>().enabled = true;
            }

            ChangeNextTetromino();
        }
    }

    private void ChangeNextTetromino()
    {
        tetrominoIndex = Random.Range(0, tetrominoes.Length);
        nextTetromino = Instantiate(tetrominoes[tetrominoIndex], nextTetrominoPosition, Quaternion.identity);
        nextTetromino.transform.localScale = nextTetrominoScale;
        nextTetromino.GetComponent<Tetromino>().enabled = false;
    }
}