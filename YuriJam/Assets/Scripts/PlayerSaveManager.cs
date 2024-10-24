using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerSaveManager 
{
    private static PlayerSaveManager instance;
    private PlayerSave playerSave;

    public PlayerSave PlayerSave 
    {
        get { return playerSave; } 
    }

    public static PlayerSaveManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerSaveManager();
            }

            return instance;
        }
    }

    PlayerSaveManager()
    {
        playerSave = new PlayerSave();
    }

    public bool LoadFromFile()
    {
        
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return false;
        }

        BinaryFormatter bf = new BinaryFormatter();
        playerSave = (PlayerSave)bf.Deserialize(file);
        file.Close();

        Debug.Log(playerSave.currentStageName);
        return true;
    }

    public void Reset()
    {
        playerSave = new PlayerSave();
    }

    public void Save()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, playerSave);
        file.Close();
    }
}
