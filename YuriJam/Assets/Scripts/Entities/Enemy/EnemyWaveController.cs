using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    // Fields
    [SerializeField]
    [Range(1.0f, 3.0f)]
    private float minSpawnTime;
    [SerializeField]
    [Range(4.0f, 6.0f)]
    private float maxSpawnTime;
    private float spawnTimer;
    [SerializeField] private List<EnemySpawnSO> enemiesToSpawn;
    private int enemyCount;

    // Properties
    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    private void Awake()
    {
        foreach (EnemySpawnSO enemySpawn in enemiesToSpawn)
        {
            enemySpawn.remaining = enemySpawn.total;
        }

        SetSpawnTimer();
    }

    private void Update()
    {
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            SetSpawnTimer();
        }

        spawnTimer -= Time.deltaTime;

        //when all enemies on screen are dead and there are none left to spawn player wins
        if (enemyCount == 0 && enemiesToSpawn.Count == 0)
        {
            Debug.Log("Player wins");
        }
    }

    private void SpawnEnemy()
    {
        if (enemiesToSpawn.Count != 0)
        {
            enemyCount++;

            TowerGrid grid = TowerGrid.Instance;
            int enemyType = UnityEngine.Random.Range(0, enemiesToSpawn.Count - 1);
            int row = UnityEngine.Random.Range(0, grid.gridHeight);

            Enemy newEnemy = Instantiate(enemiesToSpawn[enemyType].Prefab).GetComponent<Enemy>();
            newEnemy.transform.position = grid.GetWorldPositionAt(grid.gridWidth, row);
            newEnemy.transform.localScale = Vector3.one * TowerGrid.Instance.cellSize;
            newEnemy.rowNum = row;
            newEnemy.destination = grid.GetWorldPositionAt(-3, row); //keeps enemy moving until it reaches off screen
            newEnemy.OnDeath += (object o, EventArgs e) => { enemyCount--; };

            enemiesToSpawn[enemyType].remaining--;
            if (enemiesToSpawn[enemyType].remaining <= 0)
            {
                enemiesToSpawn.RemoveAt(enemyType);
            }
        }
    }

    private void SetSpawnTimer()
    {
        spawnTimer = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}
