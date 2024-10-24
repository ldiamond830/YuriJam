using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnSO : ScriptableObject
{
    public GameObject Prefab;
    public int total;
    [Tooltip("used to track the number of enemies left in the level, does not need to be set in inspector")]
    public int remaining;
}
