using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class TowerSO : ScriptableObject
{
    // Fields
    public new string name;
    public Transform prefab;
    public Transform visual;
    public Resource buildCost;
}
