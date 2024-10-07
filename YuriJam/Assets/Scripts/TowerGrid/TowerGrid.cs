using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    // Grid Fields
    private Grid<TowerGridObject> grid;
    public int gridWidth;
    public int gridHeight;
    public float cellSize;
    private Vector2Int currCellGridPos = Vector2Int.zero;

    // Building selection fields
    [SerializeField] private List<TowerSO> towerTypes;
    private int selectionIndex = 0;

    // Event fields
    public event EventHandler<GridSelectEventArgs> OnSelectionChange;
    public event EventHandler<GridMoveEventArgs> OnCellMove;

    // Properties
    public static TowerGrid Instance // Singleton property for global access
    {
        get;
        private set;
    }

    public Vector2Int CurrentCellGridPos // Mouse position on grid in grid coordinates
    {
        get { return currCellGridPos; }
    }

    public Vector3 CurrentCellWorldPos // Mouse position on grid in world coordinates
    {
        get { return grid.GetWorldPosition(currCellGridPos.x, currCellGridPos.y); }
    }

    public TowerSO CurrentSelection // Currently selected building type
    {
        get { return towerTypes[selectionIndex]; }
    }

    // Methods
    private void Awake()
    {
        // Singleton configuration for global access to an instance
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        // Instantiate new grid
        grid = new Grid<TowerGridObject>(
            gridWidth,
            gridHeight,
            cellSize,
            new Vector3(gridWidth, gridHeight) * -cellSize / 2f,
            (Grid<TowerGridObject> g, int x, int y) =>
            {
                return new TowerGridObject(g, x, y);
            });
    }

    // Attempts to create a new tower at given grid position
    // Returns true if new tower was successfully placed
    public bool PlaceTower(int gridX, int gridY)
    {
        // SANITIZE INPUT
        if (gridX < 0 || gridY < 0 || gridX >= gridWidth || gridY >= gridHeight)
        {
            Debug.Log("Cannot build: Outside of grid!");
            return false;
        }

        // VAlIDATE PLACEMENT
        // Ensure selected cell is vacant
        if (!grid.GetItem(gridX, gridY).IsEmpty)
        {
            Debug.Log("Cannot build: A tower already exists here!");
            return false;
        }

        // VERIFY BUILD COST
        // Remove resources from inventory, or cancel build if there are insufficient resources
        if (!Inventory.Instance.RemoveAmount(towerTypes[selectionIndex].buildCost))
        {
            Debug.Log("Cannot build: Insufficient resources!");
            return false;
        }

        // CREATE BUILDING
        // If selected cell is vacant and cost is met, instantiate tower and store reference in selected cell
        Tower newTower = Tower.Create(grid.GetWorldPosition(gridX, gridY), towerTypes[selectionIndex]);
        newTower.transform.parent = transform.parent;
        grid.GetItem(gridX, gridY).Tower = newTower;

        Debug.Log(towerTypes[selectionIndex].name + " has been built!");

        // Build success
        return true;
    }

    // Places tower at current position
    public bool PlaceTower()
    {
        return PlaceTower(currCellGridPos.x, currCellGridPos.y);
    }

    // Changes the currently selected tower type
    public void ChangeSelection(int newIndex)
    {
        int oldIndex = selectionIndex;
        selectionIndex = newIndex;

        // Loop to start/end of list as needed
        if (selectionIndex < 0)
            selectionIndex = towerTypes.Count - 1;
        if (selectionIndex >= towerTypes.Count)
            selectionIndex = 0;

        OnSelectionChange?.Invoke(this, new GridSelectEventArgs(oldIndex, newIndex));
        Debug.Log("Now building: " + towerTypes[selectionIndex].name);
    }

    // Destroys any existing tower at given position
    public void DestroyTower(int gridX, int gridY)
    {
        TowerGridObject gridObj = grid.GetItem(gridX, gridY);
        Tower tower = gridObj.Tower;

        // If tower exists at selected cell, remove cell reference and destroy
        if (tower != null)
        {
            // Remove grid cell references
            grid.GetItem(gridX, gridY).Tower = null;

            // Destroy building
            Destroy(tower.gameObject);
        }
    }

    // Update current cell position to mouse position
    // Returns false if mouse is outside of grid bounds
    public bool MoveToMouseCell()
    {
        grid.GetXY(GetMouseWorldPosition(), out int x, out int y);
        Vector2Int newGridPos = new(x, y);
        if (newGridPos.x < 0 || newGridPos.x >= gridWidth || newGridPos.y < 0 || newGridPos.y >= gridHeight) return false;

        // If grid position has changed, update stored position
        if (currCellGridPos != newGridPos)
        {
            OnCellMove?.Invoke(this, new GridMoveEventArgs(currCellGridPos, newGridPos));
            currCellGridPos = newGridPos;
        }

        return true;
    }

    // Update current cell position by given offsets
    // Returns false if new cell position is outside of grid bounds
    public bool MoveCurrentCell(int dx, int dy)
    {
        if (dx == 0 && dy == 0) return true;

        Vector2Int newGridPos = new(currCellGridPos.x + dx, currCellGridPos.y + dy);
        if (newGridPos.x < 0 || newGridPos.x >= gridWidth || newGridPos.y < 0 || newGridPos.y >= gridHeight) return false;

        OnCellMove?.Invoke(this, new GridMoveEventArgs(currCellGridPos, newGridPos));
        currCellGridPos = newGridPos;
        return true;
    }

    // Utility method: convert mouse screen position to world XY coordinates
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        return worldPos;
    }
}

public class GridMoveEventArgs : EventArgs
{
    public Vector2Int lastGridPosition;
    public Vector2Int currGridPosition;

    public GridMoveEventArgs(Vector2Int lastGridPosition, Vector2Int currGridPosition)
    {
        this.lastGridPosition = lastGridPosition;
        this.currGridPosition = currGridPosition;
    }
}

public class GridSelectEventArgs : EventArgs
{
    public int oldIndex;
    public int newIndex;

    public GridSelectEventArgs(int oldIndex, int newIndex)
    {
        this.oldIndex = oldIndex;
        this.newIndex = newIndex;
    }
}