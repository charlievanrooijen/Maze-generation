using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MazeController : MonoBehaviour
{
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public Button generateButton;
    public GameObject parent; // parent GameObject for all cells
    public GameObject wallPrefab; // Prefab for walls
    public GameObject pathPrefab; // Prefab for paths

    private MazeGenerator mazeGenerator;
    private Cell[,] maze;

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
                var cell = maze[x, y];
                var position = new Vector3(x, y, 0); // Changed here

                GameObject prefab;

                if (cell.Walls["Top"] || cell.Walls["Right"] || cell.Walls["Bottom"] || cell.Walls["Left"])
                {
                    prefab = wallPrefab;
                }
                else
                {
                    prefab = pathPrefab;
                }

                var instance = Instantiate(prefab, position, Quaternion.identity, parent.transform);
            }
        }
    }
}
