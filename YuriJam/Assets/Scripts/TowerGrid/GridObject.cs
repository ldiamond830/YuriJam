using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    // Fields
    public Grid<GridObject> grid;
    public int x;
    public int y;

    // Constructor
    public GridObject(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public GridObject(Grid<GridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    // Methods
    public override string ToString()
    {
        return x + ", " + y;
    }
}
