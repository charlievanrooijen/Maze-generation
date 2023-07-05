using System;
using System.Collections.Generic;
using System.Linq;

public class MazeGenerator
{
    private CellData[,] maze;
    private int width, height;
    private System.Random random;
    private Stack<CellData> stack;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.random = new System.Random();
        this.stack = new Stack<CellData>();
        maze = new CellData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = new CellData(x, y);
            }
        }
    }

    public void GenerateMaze()
    {
        CellData currentCell = maze[random.Next(width), random.Next(height)];
        currentCell.IsVisited = true;
        stack.Push(currentCell);

        while (stack.Count > 0)
        {
            CellData cell = stack.Pop();
            List<CellData> unvisitedNeighbours = GetValidNeighbors(cell.X, cell.Y).Where(c => !c.IsVisited).ToList();

            if (unvisitedNeighbours.Any())
            {
                stack.Push(cell);

                CellData chosenCell = unvisitedNeighbours[random.Next(unvisitedNeighbours.Count)];
                RemoveWall(cell, chosenCell);

                chosenCell.IsVisited = true;
                stack.Push(chosenCell);
            }
        }
    }

    private List<CellData> GetValidNeighbors(int x, int y)
    {
        List<CellData> neighbors = new List<CellData>();

        if (IsValid(x, y + 1))
        {
            neighbors.Add(maze[x, y + 1]);
        }
        if (IsValid(x, y - 1))
        {
            neighbors.Add(maze[x, y - 1]);
        }
        if (IsValid(x + 1, y))
        {
            neighbors.Add(maze[x + 1, y]);
        }
        if (IsValid(x - 1, y))
        {
            neighbors.Add(maze[x - 1, y]);
        }

        return neighbors;
    }

    private void RemoveWall(CellData currentCell, CellData nextCell)
    {
        if (currentCell.X == nextCell.X)
        {
            if (currentCell.Y < nextCell.Y)
            {
                currentCell.Walls["Top"] = false;
                nextCell.Walls["Bottom"] = false;
            }
            else
            {
                currentCell.Walls["Bottom"] = false;
                nextCell.Walls["Top"] = false;
            }
        }
        else
        {
            if (currentCell.X < nextCell.X)
            {
                currentCell.Walls["Right"] = false;
                nextCell.Walls["Left"] = false;
            }
            else
            {
                currentCell.Walls["Left"] = false;
                nextCell.Walls["Right"] = false;
            }
        }
    }

    private bool IsValid(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    public CellData[,] GetMaze()
    {
        return maze;
    }
}