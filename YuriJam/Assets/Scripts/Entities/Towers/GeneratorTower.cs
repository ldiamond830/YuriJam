using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : Tower
{
    // Fields
    [Space(15)]
    public Resources generatorResource;

    // Methods
    protected override void TakeAction()
    {
        Inventory.Instance.AddAmount(generatorResource, stats.baseStats.power);
        MainHUD.CreateFadeMessage(Center, "+" + stats.baseStats.power + " " + generatorResource, 1f, Color.green, 10);
    }
}
