using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private SpawnTetrominoes spawnTetrominoes;
    private GameManager gameManager;

    private bool tetrominoIsPlaced = false;
    private float fallTime;

    public float fallSpeed = 0.8f;
    public Vector3 rotationPoint;

    [SerializeField] private float boardLimitHeight = 28.0f;
    public static int height = 30;
    public static int width = 15;
    private static Transform[,] grid = new Transform[width, height];

    public int score = 0;

    private void Start()
    {
        spawnTetrominoes = FindObjectOfType<SpawnTetrominoes>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        TetrominoControl();
        GameOverCheck();
    }

    private void TetrominoControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTetromino(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTetromino(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateTetromino();
        }

        if (Time.time - fallTime > (Input.GetKey(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!CheckBoardBorder())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                spawnTetrominoes.SpawnTetromino();
            }

            fallTime = Time.time;
        }
    }

    private void MoveTetromino(int directionX, int directionY)
    {
        transform.position += new Vector3(directionX, directionY, 0);

        if (!CheckBoardBorder())
        {
            transform.position -= new Vector3(directionX, directionY, 0);
        }
    }

    private void RotateTetromino()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!CheckBoardBorder())
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    private void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        gameManager.Scored();
    }

    private void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    private void AddToGrid()
    {
        foreach (Transform block in transform)
        {
            int roundedX = Mathf.RoundToInt(block.transform.position.x);
            int roundedY = Mathf.RoundToInt(block.transform.position.y);

            grid[roundedX, roundedY] = block;

            tetrominoIsPlaced = true;
        }
    }

    private bool CheckBoardBorder()
    {
        foreach (Transform block in transform)
        {
            int roundedX = Mathf.RoundToInt(block.transform.position.x);
            int roundedY = Mathf.RoundToInt(block.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }

    private void GameOverCheck()
    {
        if (transform.position.y > boardLimitHeight && tetrominoIsPlaced)
        {
            spawnTetrominoes.isGameOver = true;
            gameManager.GameOver();
            AddToGrid();
        }
    }
}
