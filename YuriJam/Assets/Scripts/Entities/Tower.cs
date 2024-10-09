using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Generator,
    LongRange,
    ShortRange,
    Wall,
    Focus
}

public class Tower : MonoBehaviour
{
    // Fields
    public TowerStats stats;
    private float actCooldown;

    // Events
    public event EventHandler<EventArgs> OnDeath;

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

    public void TakeDamage(int dmg)
    {
        stats.baseStats.currHealth -= dmg;

        if (stats.baseStats.currHealth <= 0)
        {
            OnDeath?.Invoke(this, new EventArgs());
            Destroy(gameObject);
        }
    }
}
