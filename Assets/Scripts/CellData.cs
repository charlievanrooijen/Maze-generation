using System.Collections.Generic;

public class CellData
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsVisited { get; set; }
    public Dictionary<string, bool> Walls { get; set; }

    public CellData(int x, int y)
    {
        X = x;
        Y = y;
        IsVisited = false;
        Walls = new Dictionary<string, bool>
        {
            { "Top", true },
            { "Bottom", true },
            { "Right", true },
            { "Left", true }
        };
    }
}
