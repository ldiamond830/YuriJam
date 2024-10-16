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

public enum TowerLinkType
{
    StatBoost,      // Numeric changes to existing stats
    AttackEffect     // Add status effect to attacks (only affects TurretTower classes)
}

public class Tower : MonoBehaviour
{
    // General Fields
    public TowerStats stats;
    protected float actCooldown;
    [NonSerialized] public int rowNum;

    // Link Fields
    [NonSerialized] public Tower linked;

    [Space(15)]
    public TowerLinkType linkType;
    public TowerStats linkStatBoost;
    public StatusEffectSO linkAttackEffect;

    // Events
    public event EventHandler<EventArgs> OnDeath;

    // Properties
    public Vector3 Center
    {
        get { return transform.position + (Vector3.one * transform.localScale.x / 2); }
    }

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
        MainHUD.CreateFadeMessage(Center, "-" + dmg, 0.5f, Color.red, 10);

        if (stats.baseStats.currHealth <= 0)
        {
            OnDeath?.Invoke(this, new EventArgs());
            Destroy(gameObject);
        }
    }

    public bool LinkToTower(Tower tower)
    {
        // Ensure both towers are unlinked
        if (linked != null || tower.linked != null)
        {
            Debug.Log("Tower link failed: tower is already linked!");
            return false;
        }

        // Link towers together
        LinkActivate(tower);
        tower.LinkActivate(this);

        return true;
    }

    protected virtual void LinkActivate(Tower tower)
    {
        // Link tower and add callback to undo link if destroyed
        linked = tower;
        OnDeath += tower.LinkDeath;

        // Activate link effects available to all towers
        switch (tower.linkType)
        {
            case TowerLinkType.StatBoost:
                stats += tower.linkStatBoost;
                break;
        }
    }

    protected virtual void LinkDeath(object o, EventArgs e)
    {
        // Undo link effects available to all towers
        switch (linked.linkType)
        {
            case TowerLinkType.StatBoost:
                stats -= linked.linkStatBoost;
                break;
        }

        linked = null;
    }
}
