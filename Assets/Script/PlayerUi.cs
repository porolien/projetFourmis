using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public GameObject Lumberjack, Mason, Explorer, Collier;
    public GameObject antwindow, antwindow1, antwindow2, antwindow3, antwindow4;
    public GameObject ressourcesmanager;
    public GameObject ant;
    public TMP_Text AntName;
    //public Text ressources;

    public void AntButton()
    {
            antwindow.SetActive(true);
    }

    public void CloseAntButton()
    {
        antwindow.SetActive(false);
    }

    public void AntButton1()
    {
        antwindow1.SetActive(true);
    }

    public void CloseAntButton1()
    {
        antwindow1.SetActive(false);
    }

    public void AntButton2()
    {
        antwindow2.SetActive(true);
    }

    public void CloseAntButton2()
    {
        antwindow2.SetActive(false);
    }

    public void AntButton3()
    {
        antwindow3.SetActive(true);
    }

    public void CloseAntButton3()
    {
        antwindow3.SetActive(false);
    }

    public void AntButton4()
    {
        antwindow4.SetActive(true);
    }

    public void CloseAntButton4()
    {
        antwindow4.SetActive(false);
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

    public void Init()
    {
        AntName.text = ant.name;
        Debug.Log(ant.GetComponent<MovingAnt>().job);
        switch (ant.GetComponent<MovingAnt>().job)
        {
            
            case "lumberjack":
                Lumberjack.GetComponent<Image>().color = Color.red;
                break;
            case "collier":
                Lumberjack.GetComponent<Image>().color = Color.red;
                break;
            case "explorer":
                Lumberjack.GetComponent<Image>().color = Color.red;
                break;
            case "mason":
                Lumberjack.GetComponent<Image>().color = Color.red;
                break;
            default:
                break;
        }
    }



}