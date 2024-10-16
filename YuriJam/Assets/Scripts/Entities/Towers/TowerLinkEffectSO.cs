using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerLinkType
{
    StatBoost,      // Numeric changes to existing stats
    EnemyEffect     // Add status effect to enemy (only affects TurretTower classes)
}

[CreateAssetMenu(menuName = "YuriJam/TowerLinkEffectSO")]
public class TowerLinkEffectSO : ScriptableObject
{
    public TowerLinkType type;
    public TowerStats statBoost;
}
