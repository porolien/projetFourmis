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

    public void JobButton(string TheJob)
    {
        MovingAnt movingAnt = ant.GetComponent<MovingAnt>();
        movingAnt.job = "student";

        foreach (GameObject skinChildren in movingAnt.skins)
        {
            skinChildren.SetActive(false);
        }
        GameObject skin = movingAnt.skins.Find(x => x.tag == "Vagrant");
        skin.SetActive(true);

        if (GameManager.Instance.isItDay)
        {
            movingAnt.waypointToReach = GameManager.Instance.schools[Random.Range(0, GameManager.Instance.schools.Count)].gameObject;
            Building waypointBuilding = movingAnt.waypointToReach.GetComponent<Building>();
            waypointBuilding.antsAssignToThisBuilding.Add(movingAnt);
            movingAnt.GoTo(movingAnt.waypointToReach);
        }
        else
        {
            movingAnt.GoToSleep();
        }
        movingAnt.GetComponent<LearningAnt>().LearnAJob(TheJob);
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
