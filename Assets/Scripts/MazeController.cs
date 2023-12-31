using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeController : MonoBehaviour
{
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public Button generateButton;
    public GameObject parent;
    public GameObject cellPrefab;
    public float paddingFactor = 1.2f;

    private MazeGenerator mazeGenerator;
    private CellData[,] maze;

    private void Start()
    {
        generateButton.onClick.AddListener(GenerateMaze);
    }

    private void GenerateMaze()
    {
        int width = int.Parse(widthInput.text);
        int height = int.Parse(heightInput.text);

        mazeGenerator = new MazeGenerator(width, height);
        mazeGenerator.GenerateMaze();

        maze = mazeGenerator.GetMaze();

        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }

        UpdateTilemap();

        Vector3 centerOffset = new Vector3(GetMazeWidth() / 2f, GetMazeHeight() / 2f, 0);

        foreach (Transform child in parent.transform)
        {
            child.position -= centerOffset;
        }

        SetCamera();
    }

    private void SetCamera()
    {
        if ((float)GetMazeWidth() / GetMazeHeight() > Camera.main.aspect)
        {
            Camera.main.orthographicSize = GetMazeWidth() / 2f / Camera.main.aspect * paddingFactor;
        }
        else
        {
            Camera.main.orthographicSize = GetMazeHeight() / 2f * paddingFactor;
        }
    }

    private void UpdateTilemap()
    {
        int width = maze.GetLength(0);
        int height = maze.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellData = maze[x, y];
                var position = new Vector3(x * 1f, y * 1f, 0);

                var cellPrefabInstance = Instantiate(cellPrefab, position, Quaternion.identity, parent.transform);
                var cellPrefabController = cellPrefabInstance.GetComponent<CellPrefabController>();

                cellPrefabController.SetWallState("Top", cellData.Walls["Top"]);
                cellPrefabController.SetWallState("Right", cellData.Walls["Right"]);
                cellPrefabController.SetWallState("Bottom", cellData.Walls["Bottom"]);
                cellPrefabController.SetWallState("Left", cellData.Walls["Left"]);
            }
        }
    }

    private int GetMazeWidth()
    {
        return maze.GetLength(0);
    }

    private int GetMazeHeight()
    {
        return maze.GetLength(1);
    }
}
