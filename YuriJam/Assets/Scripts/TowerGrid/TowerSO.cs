using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "YuriJam/TowerSO")]
public class TowerSO : ScriptableObject
{
    // Fields
    public new string name;
    public Transform prefab;
    public Resource buildCost;
}
