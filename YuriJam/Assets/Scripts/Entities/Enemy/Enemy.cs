using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum EnemyMode
{
    move,
    attack
}

public class Enemy : MonoBehaviour
{
    // Fields
    public EnemyStats stats;
    public Vector3 desination;
    public int rowNum;
    private Tower target;
    private float attackTimer;
    private EnemyMode enemyMode = EnemyMode.move;
    private TowerGrid parent;

    public TowerGrid Parent
    {
        set { parent = value; } 
    }
    // Events 
    public event EventHandler<EventArgs> OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyMode)
        {
            case EnemyMode.move:
                Vector3 direction = (desination - transform.position).normalized;
                transform.position += stats.moveSpeed * direction * Time.deltaTime;
                break;
            case EnemyMode.attack:

                if(attackTimer == 0)
                {
                    attackTimer = stats.actSpeed;
                    Attack();
                }
                else
                {

                }

                break;
        }
    }

    public void TakeDamage(int dmg)
    {
        stats.baseStats.currHealth -= dmg;

        if (stats.baseStats.currHealth <= 0)
        {
            OnDeath?.Invoke(this, new EventArgs());
            parent.EnemyCount--;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var tower = collision.GetComponent<Tower>();
        if (tower != null && tower.rowNum == rowNum) 
        {
            target = tower;
            enemyMode = EnemyMode.attack;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        enemyMode = EnemyMode.move;
    }

    private void Attack()
    {
        Debug.Log("Enemy Attack");
        target.TakeDamage(stats.baseStats.power);
    }
}
