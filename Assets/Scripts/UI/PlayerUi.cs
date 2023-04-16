using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUi : MonoBehaviour
{
    public GameObject lumberjack, mason, explorer, collier;
    public GameObject antwindow;
    public GameObject ant;
    public TMP_InputField antName;
    public TMP_Text antJob;
    public TMP_Text antAge;

    public void AntButton()
    {
        // Open ant sheet
        antwindow.SetActive(true);
    }

    public void CloseAntButton()
    {
        // Close ant sheet
        lumberjack.SetActive(true);
        collier.SetActive(true);
        explorer.SetActive(true);
        mason.SetActive(true);
        antwindow.SetActive(false);
    }

    public void ChangeName()
    {
       ant.name = antName.text;
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
            if (GameManager.Instance.schools.Count > 0)
            {
                movingAnt.waypointToReach = GameManager.Instance.schools[Random.Range(0, GameManager.Instance.schools.Count)].gameObject;
                Building waypointBuilding = movingAnt.waypointToReach.GetComponent<Building>();
                waypointBuilding.antsAssignToThisBuilding.Add(movingAnt);
                movingAnt.GoTo(movingAnt.waypointToReach);
            }
            else
            {
                return;
            }
        }
        else
        {
            movingAnt.GoToSleep();
        }
        movingAnt.GetComponent<LearningAnt>().LearnAJob(TheJob);
    }

    public void DisplayInformations()
    {
        antName.text = ant.name;
        antJob.text = $"Job : {ant.GetComponent<MovingAnt>().job}";
        antAge.text = $"Age : {ant.GetComponent<AntAge>().age}";

        switch (ant.GetComponent<MovingAnt>().job)
        {
            case "lumberjack":
                lumberjack.SetActive(false);
                break;
            case "collier":
                collier.SetActive(false);
                break;
            case "explorer":
                explorer.SetActive(false);
                break;
            case "mason":
                mason.SetActive(false);
                break;
            default:
                break;
        }
    }
}