using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum StatusEffects
{
    Burn,       // Deals damage over time
    Harvest,    // Gain resources on death
    Shatter     // Deals stacking damage with each affliction of Shatter
}

[Serializable]
public abstract class StatusEffect
{
    // General Fields
    protected StatusEffects type;
    protected Enemy target;
    [SerializeField] protected int power;
    [SerializeField] protected float duration;

    // Properties
    public StatusEffects Type { get { return type; } }

    // Constructor
    public StatusEffect(int power, float duration)
    {
        this.power = power;
        this.duration = duration;
    }

    // Methods

    // Called when first applied to an enemy
    public virtual void Afflict(Enemy e)
    {
        e.Afflictions.Add(this);
        target = e;
    }

    // Called while applied to an enemy
    public virtual void Process(float dt)
    {
        if (duration <= 0)
        {
            Expire();
            return;
        }
        else duration -= dt;
    }

    // Called when duration of effect has ended
    public virtual void Expire()
    {
        // Remove self from target's afflictions
        target.Afflictions.Remove(this);
        target = null;
    }
}

[Serializable]
public class BurnSE : StatusEffect
{
    // Fields
    private const float burnTick = 1f;
    private float burnTime = 0;

    // Constructor
    public BurnSE(int power, float duration) : base(power, duration)
    {
        type = StatusEffects.Burn;
    }

    // Methods
    public override void Afflict(Enemy e)
    {
        // If enemy is already burned, renew burn
        if (e.Afflictions.Exists(se => se.Type == type))
        {
            BurnSE burn = (BurnSE)e.Afflictions.Find(se => se.Type == type);
            burn.Renew(this);
            return;
        }

        // If no burn, apply burn
        base.Afflict(e);
    }

    public override void Process(float dt)
    {
        base.Process(dt);

        // Update burn timer
        burnTime += dt;
        if (burnTime > burnTick)
        {
            burnTime -= burnTick;
            target.TakeDamage(power);
        }
    }

    // Resets burn if a new burn is applied within its duration
    private void Renew(BurnSE newBurn)
    {
        duration = Math.Max(newBurn.duration, duration);
        power = Math.Max(newBurn.power, power);
    }
}

[Serializable]
public class HarvestSE : StatusEffect
{
    // Fields
    private Resources resource = Resources.LP;

    // Constructor
    public HarvestSE(int power, float duration) : base(power, duration)
    {
        type = StatusEffects.Harvest;
    }

    // Methods
    public override void Afflict(Enemy e)
    {
        // If enemy is already afflicted, do not afflict
        if (e.Afflictions.Exists(se => se.Type == type))
            return;

        base.Afflict(e);
        e.OnDeath += (object o, EventArgs ea) => { 
            Inventory.Instance.AddAmount(resource, power);
            MainHUD.CreateFadeMessage(e.Center, "+" + power + " " + resource, 1f, Color.green, 10);
        };
    }

    public override void Process(float dt)
    {
        // Empty to override duration for Harvest effect
    }
}

[Serializable]
public class ShatterSE : StatusEffect
{
    // Constructor
    public ShatterSE(int power, float duration) : base(power, duration)
    {
        type = StatusEffects.Shatter;
    }

    // Methods
    public override void Afflict(Enemy e)
    {
        // If enemy is already afflicted, activate Shatter effect
        if (e.Afflictions.Exists(se => se.Type == type))
        {
            ShatterSE shatter = (ShatterSE)e.Afflictions.Find(se => se.Type == type);

            // Take damage from existing shatter, then increase stack power
            e.TakeDamage(shatter.power);
            shatter.Stack(this);
            return;
        }

        base.Afflict(e);
    }

    private void Stack(ShatterSE shatter)
    {
        power += shatter.power;
        duration = shatter.duration;
    }
}
