using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject GameMenu;
    public GameObject MenuPause;

    public void Pause()
    {
        GameMenu.SetActive(false);
        MenuPause.SetActive(true);
    }

    public void Resume()
    {
        GameMenu.SetActive(true);
        MenuPause.SetActive(false);
    }

    public void Quit()
    {
        
    }
}
