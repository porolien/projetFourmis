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
    public Resource resource;
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

    public int restNightTime;
    public int restDayTime;

    public bool isItDay;

    public Coroutine routine;

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
        restNightTime = nightTime;
        restDayTime = dayTime;
        FindAllInTheScene();

        foreach (MovingAnt ant in ants)
        {
            foreach (MovingAnt otherAnt in ants)
            {
                Physics.IgnoreCollision(ant.gameObject.GetComponent<Collider>(), otherAnt.gameObject.GetComponent<Collider>());
            }
        }
        routine = StartCoroutine(Day(dayTime));
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


        // Schools
        GameObject[] tempSchools = GameObject.FindGameObjectsWithTag("School");

        foreach (GameObject school in tempSchools)
        {
            houses.Add(school.GetComponent<Building>());
        }
    }

    public IEnumerator Day(int dayTime)
    {
        isItDay = true;
        for (int i = 1; i <= dayTime; i++)
        {
            if (restDayTime == this.dayTime)
            {
                foreach (MovingAnt ant in ants)
                {
                    ant.graphicComponents.SetActive(true);
                    ant.StartingDay();
                }
            }
            restDayTime = dayTime - i;
            yield return new WaitForSeconds(1f);
        }
        restDayTime = this.dayTime;
        routine = StartCoroutine(Night(nightTime));
    }

    public IEnumerator Night(int nightTime)
    {
        isItDay = false;
        for (int i = 1; i <= nightTime; i++)
        {
            if (restNightTime == this.nightTime)
            {
                foreach (MovingAnt ant in ants)
                {
                    //resource.Food = -1;
                    ant.graphicComponents.SetActive(true);
                    ant.GoToSleep();
                }
            }
            restNightTime = nightTime - i;
            yield return new WaitForSeconds(1f);
        }

        // Create a new ant
        GameObject newAnt = Instantiate(ant);
        ants.Add(newAnt.GetComponent<MovingAnt>());

        yield return new WaitForSeconds(0.01f);
        restNightTime = this.nightTime;
        routine = StartCoroutine(Day(dayTime));
    }

    public void PauseAnt()
    {
        foreach (MovingAnt ant in ants)
        {
            ant.agent.isStopped = true;
        }

        if (isItDay)
        {
            StopCoroutine(routine);
        }
        else
        {
            StopCoroutine(routine);
        }
    }

    public void PlayAnt()
    {
        foreach (MovingAnt ant in ants)
        {
            ant.agent.isStopped = false;
        }

        if (isItDay)
        {
            Debug.Log(restDayTime);
            routine = StartCoroutine(Day(restDayTime));
        }
        else
        {
            Debug.Log(restNightTime);
            routine = StartCoroutine(Night(restNightTime));
        }
    }
}