using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerSaveManager.Instance.LoadFromFile())
        {
            ContinueButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerSaveManager.Instance.PlayerSave.currentStageName);
    }

    public void Quit()
    {
        PlayerSaveManager.Instance.Save();
        Application.Quit();
    }
}
