using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int height;
    public int length;

    bool[,] grid;

    public void Init(int length, int height)
    {
        grid = new bool[length, height];
        this.length = length;
        this.height = height;
    }

    public void Set(int x, int y, bool to)
    {
        grid[x, y] = to;
    }

    public bool Get(int x, int y)
    {
        return grid[x, y];
    }
}
