using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetrominoes : MonoBehaviour
{
    public GameObject[] tetrominoes;
    public bool gameOver = false;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    public bool gameStarted = true;

    void Start()
    {
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        if (!gameOver)
        {
            int tetrominoIndex = Random.Range(0, tetrominoes.Length);

            if (gameStarted)
            {
                gameStarted = false;

                nextTetromino = Instantiate(tetrominoes[tetrominoIndex], transform.position, Quaternion.identity);
                previewTetromino = Instantiate(tetrominoes[tetrominoIndex], new Vector3(-6.75f, 25f, 0f), Quaternion.identity);
                previewTetromino.GetComponent<Tetromino>().enabled = false;
            }
            else
            {
                previewTetromino.transform.localPosition = transform.position;
                nextTetromino = previewTetromino;
                nextTetromino.GetComponent<Tetromino>().enabled = true;

                previewTetromino = Instantiate(tetrominoes[tetrominoIndex], new Vector3(-6.75f, 25f, 0f), Quaternion.identity);
                previewTetromino.GetComponent<Tetromino>().enabled = false;
            }
        }
    }
}
