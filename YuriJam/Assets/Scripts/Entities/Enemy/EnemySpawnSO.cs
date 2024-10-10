using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnSO : ScriptableObject
{
    public GameObject Prefab;
    public int total;
    public int remaining;
}
