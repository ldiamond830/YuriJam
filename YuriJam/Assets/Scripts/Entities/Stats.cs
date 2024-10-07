using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Stats
{
    public int maxHealth;   // Upper limit of entity health
    public int currHealth;  // Current health of entity: if this hits 0, entity dies
    public int power;       // Strength of entity's main action

    // Constructors
    public Stats(int health, int power)
    {
        maxHealth = health;
        currHealth = health;
        this.power = power;
    }

    public Stats(int maxHealth, int currHealth, int power)
    {
        this.maxHealth = maxHealth;
        this.currHealth = currHealth;
        this.power = power;
    }

    // Methods
    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats(
            a.maxHealth + b.maxHealth,
            a.currHealth + b.currHealth,
            a.power + b.power
            );
    }
    public static Stats operator -(Stats a, Stats b)
    {
        return new Stats(
            a.maxHealth - b.maxHealth,
            a.currHealth,
            a.power - b.power
            );
    }
}

public struct TowerStats
{
    public Stats baseStats; // Tower health and power
    public float actSpeed;  // Tower action cooldown (seconds)
    public float range;     // Range of tower ability

    // Constructors
    public TowerStats(Stats baseStats, float actSpeed, float range)
    {
        this.baseStats = baseStats;
        this.actSpeed = actSpeed;
        this.range = range;
    }

    public TowerStats(int maxHealth, int currHealth, int power, float actSpeed, float range)
    {
        baseStats = new Stats(maxHealth, currHealth, power);
        this.actSpeed = actSpeed;
        this.range = range;
    }

    // Methods
    public static TowerStats operator +(TowerStats a, TowerStats b)
    {
        return new TowerStats(
            a.baseStats + b.baseStats,
            a.actSpeed + b.actSpeed,
            a.range + b.range
            );
    }

    public static TowerStats operator -(TowerStats a, TowerStats b)
    {
        return new TowerStats(
            a.baseStats - b.baseStats,
            a.actSpeed - b.actSpeed,
            a.range - b.range
            );
    }
}

public struct EnemyStats
{
    public Stats baseStats; // Enemy health and power
    public float actSpeed;  // Enemy action cooldown (seconds)
    public float moveSpeed; // Movement speed

    // Constructors
    public EnemyStats(Stats baseStats, float actSpeed, float moveSpeed)
    {
        this.baseStats = baseStats;
        this.actSpeed = actSpeed;
        this.moveSpeed = moveSpeed;
    }

    public EnemyStats(int maxHealth, int currHealth, int power, float actSpeed, float moveSpeed)
    {
        baseStats = new Stats(maxHealth, currHealth, power);
        this.actSpeed = actSpeed;
        this.moveSpeed = moveSpeed;
    }
}