using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float fallTime;

    public float fallSpeed = 0.8f;
    public static int height = 30;
    public static int width = 15;

    private static Transform[,] grid = new Transform[width, height];

    private bool placed = false;
    public int score = 0;

    private SpawnTetrominoes spawnTetrominoes;
    private GameManager gameManager;

    void Start()
    {
        spawnTetrominoes = FindObjectOfType<SpawnTetrominoes>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }

        if (Time.time - fallTime > (Input.GetKey(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!CheckBorder())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                this.enabled = false;
                spawnTetrominoes.SpawnTetromino();
            }
            fallTime = Time.time;
        }

        if (transform.position.y > 28.0f && placed)
        {
            spawnTetrominoes.gameOver = true;
            gameManager.GameOver();
            AddToGrid();
        }
    }

    void Move(int dx, int dy)
    {
        transform.position += new Vector3(dx, dy, 0);

        if (!CheckBorder())
        {
            transform.position -= new Vector3(dx, dy, 0);
        }
    }

    void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!CheckBorder())
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    void CheckForLines()
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

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        gameManager.Scored();
    }

    void RowDown(int i)
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


    void AddToGrid()
    {
        foreach (Transform block in transform)
        {
            int roundedX = Mathf.RoundToInt(block.transform.position.x);
            int roundedY = Mathf.RoundToInt(block.transform.position.y);

            grid[roundedX, roundedY] = block;

            placed = true;
        }
    }

    bool CheckBorder()
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

}
