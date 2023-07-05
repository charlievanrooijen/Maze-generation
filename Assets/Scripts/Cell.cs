using System.Collections.Generic;

public class Cell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IsVisited { get; set; }
    public Dictionary<string, bool> Walls { get; private set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        IsVisited = false;

        // Initially, every cell has all its walls
        Walls = new Dictionary<string, bool>
        {
            {"Top", true},
            {"Right", true},
            {"Bottom", true},
            {"Left", true}
        };
    }
}
