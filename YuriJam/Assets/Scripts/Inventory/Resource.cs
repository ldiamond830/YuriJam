using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resources
{
    LP // Lesbian power???
}

[Serializable]
public class Resource
{
    public Resources type;
    public int amount;

    public Resource(Resources type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }

    public override string ToString()
    {
        return amount.ToString() + " " + type.ToString();
    }
}
