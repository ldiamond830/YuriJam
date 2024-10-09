using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGridObject : GridObject
{
    // Fields
    public new Grid<TowerGridObject> grid;
    private Tower tower;

    // Properties
    public bool IsEmpty
    {
        get { return tower == null; }
    }

    public Tower Tower
    {
        get { return tower; }
        set
        {
            tower = value;
            if (tower != null)
                tower.OnDeath += (object o, EventArgs e) => { Tower = null; };

            grid.FlagDirty(x, y);
        }
    }

    // Constructor
    public TowerGridObject(Grid<TowerGridObject> grid, int x, int y)
        : base(x, y)
    {
        this.grid = grid;
    }
}
