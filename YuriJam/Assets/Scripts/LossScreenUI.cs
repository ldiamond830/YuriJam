using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenUI : MonoBehaviour
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
}
