using UnityEngine;

public class CellPrefabController : MonoBehaviour
{
    public GameObject TopWall, RightWall, BottomWall, LeftWall;

    public void SetWallState(string wall, bool state)
    {
        switch(wall)
        {
            case "Top":
                TopWall.SetActive(state);
                break;
            case "Right":
                RightWall.SetActive(state);
                break;
            case "Bottom":
                BottomWall.SetActive(state);
                break;
            case "Left":
                LeftWall.SetActive(state);
                break;
        }
    }
}
