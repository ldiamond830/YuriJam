using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    public Grid<GridObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<GridObject>(5, 5, 10, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        return worldPos;
    }
}
