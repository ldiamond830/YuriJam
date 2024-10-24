using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(PlayerSaveManager.Instance.PlayerSave.currentStageName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        PlayerSaveManager.Instance.Save();
        Application.Quit();
    }

    //testing only while visual novel segments unfinished, I'm just putting it here to save making a whole separate script
    public void TempContinue()
    {
        LevelManager manager = FindAnyObjectByType<LevelManager>();
        SceneManager.LoadScene(manager.nextLevel);
    }
}
