using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerUi : MonoBehaviour
{
    public GameObject Lumberjack, Mason, Explorer, Collier;
    public GameObject antwindow;
    public GameObject ant;
    public TMP_InputField AntName;
    //public Text ressources;

    public void AntButton()
    {
        // Open ant sheet
        antwindow.SetActive(true);
    }

    public void CloseAntButton()
    {
        // Close ant sheet
        antwindow.SetActive(false);
    }

    public void ChangeName()
    {
       ant.name = AntName.text;
    }
    public void JobButton(string TheJob)
    {
        // Send the ant if we choose to change the job of an ant
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

    public void DisplayInformations()
    {
        AntName.text = ant.name;
        switch (ant.GetComponent<MovingAnt>().job)
        {
            case "lumberjack":
                Lumberjack.GetComponent<Image>().color = Color.red;
                break;
            case "collier":
                Collier.GetComponent<Image>().color = Color.red;
                break;
            case "explorer":
                Explorer.GetComponent<Image>().color = Color.red;
                break;
            case "mason":
                Mason.GetComponent<Image>().color = Color.red;
                break;
            default:
                break;
        }
    }
}