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
        // Call if button Play is clicked
        SceneManager.LoadScene(levelToload);
    }

    public void OpenMainMenu()
    {
        // Close main menu
        mainMenu.SetActive(true);
    }

    public void CloseMainMenu()
    {
        // Close main menu
        mainMenu.SetActive(false);
    }

    public void OpenOptionsWindow()
    {
        // Call if button Options is clicked
        optionsWindow.SetActive(true);
    }

    public void CloseOptionsWindow()
    {
        // Close options
        optionsWindow.SetActive(false);
    }

    public void OpenCreditsWindow()
    {
        // Call if button Credits is clicked
        creditsWindow.SetActive(true);
    }

    public void CloseCreditsWindow()
    {
        // Close credits
        creditsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit game
        Application.Quit();
    }
}