using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeController : MonoBehaviour
{
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public Button generateButton;
    public GameObject parent; // parent GameObject for all cells
    public GameObject cellPrefab; // Prefab for the cells

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

        parent.transform.localPosition = new Vector3(-width / 2f, -height / 2f, 0);
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

    public int GetMazeWidth()
    {
        return maze.GetLength(0);
    }
    
    public int GetMazeHeight()
    {
        return maze.GetLength(1);
    }
    
    public bool IsMazeGenerated()
    {
        return maze != null;
    }
}
