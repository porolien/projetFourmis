using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    public int woodCost;
    public int stoneCost;
    public int foodCost;
    public GameObject TheConstruct;
    public string TypeOfConstruct;
    public float timeToConstruct;
    public float AllTheTime;
    // Update is called once per frame

    public void LetHimConstruct()
    {
        //the building will be visible then we destruct our wworksite
        TheConstruct.SetActive(true);
        switch (TypeOfConstruct)
        {          
            case "museum":
                GameManager.Instance.museums.Add(TheConstruct.GetComponent<Building>());
                TheConstruct.GetComponent<GaugeGain>().GainSomeHappyness();
                break;
            case "school":
                GameManager.Instance.schools.Add(TheConstruct.GetComponent<Building>());
                break;
            case "library":
                GameManager.Instance.librarys.Add(TheConstruct.GetComponent<Building>());
                TheConstruct.GetComponent<GaugeGain>().GainSomeHappyness();
                break;
            case "farm":
                GameManager.Instance.farms.Add(TheConstruct.GetComponent<Building>());
                break;
            case "house":
                GameManager.Instance.houses.Add(TheConstruct.GetComponent<Building>());
                break;
        }

        for(int i = 0; i < GameManager.Instance.worksites.Count; i++)
        {
            if (GameManager.Instance.worksites[i] == gameObject.GetComponent<Building>())
            {
                GameManager.Instance.worksites.Remove(GameManager.Instance.worksites[i]);
            }
        }
        for(int j = 0; j < gameObject.GetComponent<Building>().antsInBuilding.Count ; j++)
        {
            gameObject.GetComponent<Building>().antsInBuilding[j].wantToLeft();
            gameObject.GetComponent<Building>().antsInBuilding[j].isWorking = false;
        }
        Destroy(gameObject);
    }

    public void contructThePrefab()
    {
        //put a invisible building that will appear when we finish the work
        GameObject ThePrefab = Instantiate(TheConstruct, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        ThePrefab.SetActive(false);
        TheConstruct = ThePrefab;
    }
}