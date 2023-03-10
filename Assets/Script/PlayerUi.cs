using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{

    public GameObject botonplayer;
    public GameObject antwindow, workwindow;
    public GameObject ressourcesmanager;
    //public Text ressources;

    public void AntButton()
    {
        antwindow.SetActive(true);
    }

    public void CloseAntButton()
    {
        antwindow.SetActive(false);
    }

    public void WorkWindow()
    {
        workwindow.SetActive(true);
    }

    public void CloseWorkWindow()
    {
        workwindow.SetActive(false);
    }

    public void PrintState()
    {
        // ressources.text = ressourcesmanager. missing data;
        // ressources.text = ressourcesmanager. missing data;
        // ressources.text = ressourcesmanager. missing data;
        // ressources.text = ressourcesmanager. missing data;
        // ressources.text = ressourcesmanager. missing data;
        // ressources.text = ressourcesmanager. missing data;
    }



}
