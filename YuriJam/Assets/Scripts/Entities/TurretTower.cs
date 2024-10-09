using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower
{
    // Fields
    public Transform projectile;
    public bool isPiercing;

    protected override void TakeAction()
    {
        // Create projectile using stats
        Projectile p = Instantiate(projectile, transform.position + Vector3.one * transform.localScale.x / 2, Quaternion.identity).GetComponent<Projectile>();
        p.transform.localScale *= transform.localScale.x;
        p.Initialize(stats.baseStats.power, stats.range * transform.localScale.x, isPiercing);
        p.Fire();
    }
}
