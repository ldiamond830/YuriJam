using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{ 
    public string currentStageName;

    public PlayerSave()
    {
        currentStageName = "Level1";
    }
}
