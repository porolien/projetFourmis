using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance = null;
    private GameManager() { }
    public static GameManager Instance => _instance;
    //

    // Prefab to clone each day
    public GameObject ant;

    // All objects we need to access during the game
    public List<MovingAnt> ants = new();
    public List<Building> forests = new();
    public List<Building> mines = new();
    public List<Building> foods = new();
    public List<Building> worksites = new();
    public List<Building> houses = new();
    public List<Building> school = new();
    public GameObject[] ground;

    // Time of the day and the night
    public int dayTime;
    public int nightTime;

    private void Awake()
    {
        //Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //
    }

    void Start()
    {
        FindAllInTheScene();
        StartCoroutine(Day());
    }

    private void FindAllInTheScene()
    {
        // Find all who's already placed in the scene like ants, ground or zones

        // Ground
        ground = GameObject.FindGameObjectsWithTag("Ground");

        // Ants
        MovingAnt[] tempAnts = FindObjectsOfType<MovingAnt>();

        foreach (MovingAnt ant in tempAnts)
        {
            ants.Add(ant);
        }

        // Forests
        GameObject[] tempForests = GameObject.FindGameObjectsWithTag("Forest");

        foreach (GameObject forest in tempForests)
        {
            forests.Add(forest.GetComponent<Building>());
        }

        // Mines
        GameObject[] tempMines = GameObject.FindGameObjectsWithTag("Mine");

        foreach (GameObject mine in tempMines)
        {
            mines.Add(mine.GetComponent<Building>());
        }

        // Foods
        GameObject[] tempFoods = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject food in tempFoods)
        {
            foods.Add(food.GetComponent<Building>());
        }

        // Worksites
        GameObject[] tempWorksites = GameObject.FindGameObjectsWithTag("Worksite");

        foreach (GameObject worksite in tempWorksites)
        {
            worksites.Add(worksite.GetComponent<Building>());
        }

        // Houses
        GameObject[] tempHouses = GameObject.FindGameObjectsWithTag("House");

        foreach (GameObject house in tempHouses)
        {
            houses.Add(house.GetComponent<Building>());
        }

        GameObject[] tempSchools = GameObject.FindGameObjectsWithTag("School");

        foreach (GameObject school in tempSchools)
        {
            houses.Add(school.GetComponent<Building>());
        }
    }

    public IEnumerator Day()
    {
        foreach (MovingAnt ant in ants)
        {
            ant.gameObject.SetActive(true);
            ant.StartingDay();
        }
        yield return new WaitForSeconds(dayTime);
        StartCoroutine(Night());
    }

    public IEnumerator Night()
    {
        foreach (MovingAnt ant in ants)
        {
            ant.gameObject.SetActive(true);
            ant.GoToSleep();
        }
        yield return new WaitForSeconds(nightTime);

        // Create a new ant
        GameObject newAnt = Instantiate(ant);
        ants.Add(newAnt.GetComponent<MovingAnt>());

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Day());
    }
}