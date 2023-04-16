using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPause;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        MenuPause.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
