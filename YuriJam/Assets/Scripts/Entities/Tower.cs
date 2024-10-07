using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Fields
    public TowerStats stats;
    private float actCooldown;

    // Constructors
    public static Tower Create(Vector3 worldPos, TowerSO data)
    {
        Transform towerTransform = Instantiate(data.prefab, worldPos, Quaternion.identity);
        Tower tower = towerTransform.GetComponent<Tower>();

        return tower;
    }

    // Methods
    void Start()
    {
        
    }

    void Update()
    {
        actCooldown += Time.deltaTime;

        if (actCooldown >= stats.actSpeed)
        {
            actCooldown -= stats.actSpeed;
            TakeAction();
        }
    }

    protected virtual void TakeAction() {}
}
