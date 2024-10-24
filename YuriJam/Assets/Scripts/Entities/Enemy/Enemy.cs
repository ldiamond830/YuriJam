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
    public Vector3 destination;
    public int rowNum;
    private Tower target;
    private float attackTimer;
    private EnemyMode enemyMode = EnemyMode.move;
    private TowerGrid parent;
    private List<StatusEffect> afflictions = new();

    // Properties
    public TowerGrid Parent
    {
        set { parent = value; } 
    }
    public List<StatusEffect> Afflictions
    {
        get { return afflictions; }
    }
    public Vector3 Center
    {
        get { return transform.position + (Vector3.one * transform.localScale.x / 2); }
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
                Vector3 direction = (destination - transform.position).normalized;
                transform.position += stats.moveSpeed * direction * Time.deltaTime;
                break;
            case EnemyMode.attack:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attackTimer += stats.actSpeed;
                    Attack();
                }

                break;
        }

        ProcessAfflictions();
    }

    public void TakeDamage(int dmg)
    {
        if (dmg == 0) return;

        stats.baseStats.currHealth -= dmg;
        MainHUD.CreateFadeMessage(Center, "-" + dmg, 0.5f, Color.red, 10);
        Debug.Log(dmg + " damage taken");

        if (stats.baseStats.currHealth <= 0)
        {
            OnDeath?.Invoke(this, new EventArgs());
            Destroy(gameObject);
        }
    }

    public void TakeDamage(Attack attack)
    {
        // Apply any status effects
        if (attack.effects.Count > 0)
        {
            foreach (StatusEffect se in attack.effects)
                se.Afflict(this);
        }

        // Deal attack damage
        TakeDamage(attack.power);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tower = collision.transform.GetComponent<Tower>();
        if (tower != null && tower.rowNum == rowNum)
        {
            target = tower;
            enemyMode = EnemyMode.attack;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tower"))
        {
            target = null;
            enemyMode = EnemyMode.move;
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy Attack");
        target.TakeDamage(stats.baseStats.power);
    }

    private void ProcessAfflictions()
    {
        foreach (StatusEffect se in afflictions)
            se.Process(Time.deltaTime);
    }
}
