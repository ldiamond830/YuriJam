using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower
{
    // Fields
    [Space(15)]
    public Transform projectile;
    public bool isPiercing;
    public List<StatusEffectSO> attackEffects;

    // Methods
    protected override void TakeAction()
    {
        // Create projectile at center of grid cell and scale to grid
        Projectile p = Instantiate(projectile, Center, Quaternion.identity).GetComponent<Projectile>();
        p.transform.localScale *= transform.localScale.x;

        // Generate status effects
        List<StatusEffect> effects = new();
        foreach (StatusEffectSO so in attackEffects)
            effects.Add(so.CreateEffect());

        // Use stats to define projectile parameters, then activate
        p.Initialize(stats.baseStats.power, stats.range * transform.localScale.x, isPiercing, effects);
        p.Fire();
    }

    protected override void LinkActivate(Tower tower)
    {
        base.LinkActivate(tower);

        // Activate turret-specific link effects
        switch (tower.linkType)
        {
            case TowerLinkType.AttackEffect:
                attackEffects.Add(tower.linkAttackEffect);
                break;
        }
    }

    protected override void LinkDeath(object o, EventArgs e)
    {
        // Undo turret-specific link effects
        switch (linked.linkType)
        {
            case TowerLinkType.AttackEffect:
                attackEffects.Remove(linked.linkAttackEffect);
                break;
        }

        base.LinkDeath(o, e);
    }
}
