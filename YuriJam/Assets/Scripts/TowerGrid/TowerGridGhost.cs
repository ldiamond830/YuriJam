using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGridGhost : MonoBehaviour
{
    // Fields
    private Transform visual;
    private TowerSO data;

    // Start is called before the first frame update
    void Start()
    {
        //RefreshVisual();
        transform.localScale = Vector3.one * TowerGrid.Instance.cellSize;
        TowerGrid.Instance.OnSelectionChange += OnSelectionChange;
        TowerGrid.Instance.OnCellMove += OnCellMove;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = TowerGrid.Instance.CurrentCellWorldPos;
    }

    private void OnSelectionChange(object sender, EventArgs e)
    {
        // Reset validity indicator
        //RefreshVisual();
        // If data != null, revisualize validity
    }

    private void OnCellMove(object sender, GridMoveEventArgs e)
    {
        // Toggle visibility if outside of grid bounds
        transform.GetChild(0).gameObject.SetActive(e.currGridPosition != TowerGrid.Instance.OFFGRID_POS);
    }

    // private void RefreshVisual()
    // {
    //     if (visual != null)
    //     {
    //         Destroy(visual.gameObject);
    //         visual = null;
    //     }
    // 
    //     data = TowerGrid.Instance.CurrentSelection;
    // 
    //     if (data != null)
    //     {
    //         visual = Instantiate(data.visual, Vector3.zero, Quaternion.identity);
    //         visual.parent = transform;
    //         visual.localPosition = Vector3.zero;
    //         visual.localEulerAngles = Vector3.zero;
    //     }
    // }
}
