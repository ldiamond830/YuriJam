using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Fields
    public EnemyStats stats;

    // Events 
    public event EventHandler<EventArgs> OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
