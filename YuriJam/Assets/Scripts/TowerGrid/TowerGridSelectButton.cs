using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGridSelectButton : MonoBehaviour
{
    public TowerType type;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void SetTowerGridSelection()
    {
        TowerGrid.Instance.ChangeSelection((int)type);
    }
}
