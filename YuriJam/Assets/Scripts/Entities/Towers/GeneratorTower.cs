using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : Tower
{
    // Fields
    public Resources resource;

    // Methods
    protected override void TakeAction()
    {
        Inventory.Instance.AddAmount(resource, stats.baseStats.power);
        Debug.Log("Generated " + stats.baseStats.power + " " + resource);
    }
}
