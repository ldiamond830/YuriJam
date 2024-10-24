using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSaveManager.Instance.PlayerSave.currentStageName = SceneManager.GetActiveScene().name;
        PlayerSaveManager.Instance.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
