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
}
