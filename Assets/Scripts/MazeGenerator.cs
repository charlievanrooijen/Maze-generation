using System;
using System.Collections.Generic;

public class MazeGenerator
{
    private Cell[,] maze;
    private int width, height;
    private System.Random random;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.random = new System.Random();
        maze = new Cell[width, height];

        // Initialize all cells as walls and unvisited
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = new Cell(x, y);
            }
        }
    }

    public void GenerateMaze()
    {
        int startX = random.Next(1, width - 1);
        int startY = random.Next(1, height - 1);
        int endX = random.Next(1, width - 1);
        int endY = random.Next(1, height - 1);

        maze[startX, startY].IsVisited = true;
        maze[endX, endY].IsVisited = true;

        ConnectCells(startX, startY, endX, endY);
    }

    private void ConnectCells(int startX, int startY, int endX, int endY)
    {
        int currentX = startX;
        int currentY = startY;

        while (currentX != endX || currentY != endY)
        {
            List<Cell> neighbors = GetValidNeighbors(currentX, currentY);

            if (neighbors.Count > 0)
            {
                Cell nextCell = neighbors[random.Next(neighbors.Count)];

                int nextX = nextCell.X;
                int nextY = nextCell.Y;

                RemoveWall(currentX, currentY, nextX, nextY);
                CarvePath(currentX, currentY, nextX, nextY);

                maze[nextX, nextY].IsVisited = true;

                currentX = nextX;
                currentY = nextY;
            }
            else
            {
                break;
            }
        }
    }

    private List<Cell> GetValidNeighbors(int x, int y)
    {
        List<Cell> neighbors = new List<Cell>();

        if (IsValid(x, y + 1) && !maze[x, y + 1].IsVisited)
        {
            neighbors.Add(maze[x, y + 1]);
        }
        if (IsValid(x, y - 1) && !maze[x, y - 1].IsVisited)
        {
            neighbors.Add(maze[x, y - 1]);
        }
        if (IsValid(x + 1, y) && !maze[x + 1, y].IsVisited)
        {
            neighbors.Add(maze[x + 1, y]);
        }
        if (IsValid(x - 1, y) && !maze[x - 1, y].IsVisited)
        {
            neighbors.Add(maze[x - 1, y]);
        }

        Shuffle(neighbors);
        return neighbors;
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void RemoveWall(int currentX, int currentY, int nextX, int nextY)
    {
        if (currentX == nextX)
        {
            if (currentY < nextY)
            {
                maze[currentX, currentY].Walls["Top"] = false;
                maze[nextX, nextY].Walls["Bottom"] = false;
            }
            else
            {
                maze[currentX, currentY].Walls["Bottom"] = false;
                maze[nextX, nextY].Walls["Top"] = false;
            }
        }
        else if (currentY == nextY)
        {
            if (currentX < nextX)
            {
                maze[currentX, currentY].Walls["Right"] = false;
                maze[nextX, nextY].Walls["Left"] = false;
            }
            else
            {
                maze[currentX, currentY].Walls["Left"] = false;
                maze[nextX, nextY].Walls["Right"] = false;
            }
        }
    }

    private void CarvePath(int currentX, int currentY, int nextX, int nextY)
    {
        if (currentX != nextX)
        {
            int min = Math.Min(currentX, nextX);
            for (int i = min; i <= min + 1; i++)
            {
                maze[i, currentY].Walls["Top"] = false;
                maze[i, currentY].Walls["Bottom"] = false;
            }
        }
        else if (currentY != nextY)
        {
            int min = Math.Min(currentY, nextY);
            for (int i = min; i <= min + 1; i++)
            {
                maze[currentX, i].Walls["Left"] = false;
                maze[currentX, i].Walls["Right"] = false;
            }
        }
    }

    private bool IsValid(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    public Cell[,] GetMaze()
    {
        return maze;
    }
}
