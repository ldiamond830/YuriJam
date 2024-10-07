using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : Tower
{
    // Fields
    public Resource resource;

    // Methods
    protected override void TakeAction()
    {
        Inventory.Instance.AddAmount(resource);
    }
}
