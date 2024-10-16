using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Tower linkSelect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TowerGrid.Instance.MoveToMouseCell();
    }

    public void OnBuild()
    {
        TowerGrid.Instance.PlaceTower();
    }

    public void OnLink()
    {
        // Get tower at current grid cell
        Tower t = TowerGrid.Instance.GetTowerAtCurrent();

        // Cancel link if no tower is selected
        if (!t)
        {
            Debug.Log("Cannot link: No tower at current position!");
            if (linkSelect) linkSelect = null;
            return;
        }

        // Cancel action if selected tower is already linked
        if (t.linked)
        {
            Debug.Log("Cannot link: Selected tower is already linked!");
            linkSelect = null;
            return;
        }

        // If no other tower selected, use this tower in next selection
        if (!linkSelect) 
            linkSelect = t;
        else
        {
            // Sanity check: do not link tower to itself
            if (t == linkSelect)
            {
                Debug.Log("Cannot link: cannot link tower to itself!");
                linkSelect = null;
                return;
            }

            // Ensure that selected towers are adjacent
            Vector2Int tPos = TowerGrid.Instance.GetGridPositionAt(t.Center);
            Vector2Int lPos = TowerGrid.Instance.GetGridPositionAt(linkSelect.Center);
            if (Mathf.Abs(tPos.x - lPos.x) + Mathf.Abs(tPos.y - lPos.y) > 1)
            {
                Debug.Log("Cannot link: Selected towers are not adjacent!");
                linkSelect = null;
                return;
            }

            // Criteria met: link towers
            Debug.Log("Linking towers!");
            linkSelect.LinkToTower(t);
            linkSelect = null;
        }
    }
}
