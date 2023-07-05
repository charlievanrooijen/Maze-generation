using UnityEngine;

public class CellPrefabController : MonoBehaviour
{
    public GameObject topWall;
    public GameObject rightWall;
    public GameObject bottomWall;
    public GameObject leftWall;

    public void Init(CellData cellData)
    {
        topWall.SetActive(cellData.Walls["Top"]);
        rightWall.SetActive(cellData.Walls["Right"]);
        bottomWall.SetActive(cellData.Walls["Bottom"]);
        leftWall.SetActive(cellData.Walls["Left"]);
    }

    public void SetWallState(string wall, bool state)
    {
        switch (wall)
        {
            case "Top":
                topWall.SetActive(state);
                break;
            case "Right":
                rightWall.SetActive(state);
                break;
            case "Bottom":
                bottomWall.SetActive(state);
                break;
            case "Left":
                leftWall.SetActive(state);
                break;
            default:
                Debug.LogError("Invalid wall name");
                break;
        }
    }
}
