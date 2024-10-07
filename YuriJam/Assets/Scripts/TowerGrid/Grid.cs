using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid<T>
{
    // Fields
    private int width, height;
    private float cellSize;
    private Vector3 originPos;
    private T[,] gridArray;
    private TextMesh[,] debugTextArray;

    // Event Handlers
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x, y;
    }

    // Constructor
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createGridObject)
    {
        // Initialize grid data
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPosition;

        // Initialize grid objects using provided function
        gridArray = new T[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        // Debug tools: visualize grid layout and item values
        bool showDebugGrid = true;
        bool showDebugText = false;
        if (showDebugGrid || showDebugText)
        {
            if (showDebugText) debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Draw cell borders and values
                    if (showDebugText)
                        debugTextArray[x, y] = CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize / 2, cellSize / 2), 30, Color.white);
                    if (showDebugGrid)
                    {
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    }
                }
            }
            // Finish grid border
            if (showDebugGrid)
            {
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            }

            // Update debug text values when cell values are changed
            if (showDebugText)
            {
                OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };
            }
        }
    }

    // Methods

    // Sanitized getter for grid values using grid position
    public T GetItem(int  x, int y)
    {
        // Validate target grid position
        // Return T's default value for invalid positions
        if (x < 0 || y < 0 || x >= width || y >= height) return default;

        return gridArray[x, y];
    }

    // Sanitized getter for grid values using world position
    public T GetItem(Vector3 worldPos)
    {
        GetXY(worldPos, out int x, out int y);
        return GetItem(x, y);
    }

    // Sanitized setter for grid values using grid position
    public void SetItem(int x, int y, T value)
    {
        // Validate target grid position
        if (x < 0 || y < 0 || x >= width || y >= height) return;

        gridArray[x, y] = value;
        OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    // Sanitized setter for grid values using world position
    public void SetItem(Vector3 worldPos, T value)
    {
        GetXY(worldPos, out int x, out int y);
        SetItem(x, y, value);
    }

    // Calls value change event at given grid position
    public void FlagDirty(int x, int y)
    {
        OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    // Utility method: converts grid position into world position
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }

    // Utility method: converts world position to grid position
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
    }

    // Debugging method: displays value of an array item
    private static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPos = default, int fontSize = 40, Color color = default, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center)
    {
        if (color == null) color = Color.white;

        GameObject obj = new GameObject("World Text", typeof(TextMesh));
        Transform transform = obj.transform;
        transform.SetParent(parent, false);
        obj.transform.localPosition = localPos;
        TextMesh textMesh = obj.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.alignment = textAlignment;
        textMesh.anchor = textAnchor;
        return textMesh;
    }
}
