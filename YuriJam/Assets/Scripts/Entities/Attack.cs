using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Attack
{
    // Fields
    public int power;
    public List<StatusEffect> effects;

    // Constructors
    public Attack(int power)
    {
        this.power = power;
        this.effects = new List<StatusEffect>();
    }

    public Attack(int power, List<StatusEffect> effects)
    {
        this.power = power;
        this.effects = effects != null ? effects : new();
    }
}
