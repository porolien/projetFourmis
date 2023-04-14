using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToload;
    public GameObject optionsWindow;
    public GameObject creditsWindow;
    public GameObject mainMenu;


    public void StartGame()
    {
        SceneManager.LoadScene(levelToload);
    }

    public void OpenMainManu()
    {
        mainMenu.SetActive(true);
    }

    public void CloseMainManu()
    {
        mainMenu.SetActive(false);
    }


    public void OpenOptionsWindow()
    {
        optionsWindow.SetActive(true);
    }

    public void CloseOptionsWindow()
    {
        optionsWindow.SetActive(false);
    }

    public void OpenCreditsWindow()
    {
        creditsWindow.SetActive(true);
    }

    public void CloseCreditsWindow()
    {
        creditsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
