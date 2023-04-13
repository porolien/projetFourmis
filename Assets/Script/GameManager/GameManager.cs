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

    //prefab of ressources building needed to be paused and played
    public GameObject Mine;
    public GameObject Forest;
    public GameObject Food;

    // Prefab to clone each day
    public GameObject ant;

    //Resource and the gauge which will be upgrade during the game
    public Resource resource;
    public GaugeManager HappyGauge;

    // All objects we need to access during the game
    public List<MovingAnt> ants = new();
    public List<Building> forests = new();
    public List<Building> mines = new();
    public List<Building> foods = new();
    public List<Building> worksites = new();
    public List<Building> houses = new();
    public List<Building> schools = new();
    public List<Building> museums = new(); 
    public List<Building> librarys = new();
    public List<Building> farms = new();
    public List<GameObject> ground = new();

    //public GameObject[] ground;

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
            schools.Add(school.GetComponent<Building>());
        }

        // Museums
        GameObject[] tempMuseum = GameObject.FindGameObjectsWithTag("Museum");

        foreach (GameObject museum in tempMuseum)
        {
            museums.Add(museum.GetComponent<Building>());
        }
        // Librarys
        GameObject[] tempLibrary = GameObject.FindGameObjectsWithTag("Library");

        foreach (GameObject library in tempLibrary)
        {
            librarys.Add(library.GetComponent<Building>());
        }
        // Farms
        GameObject[] tempFarm = GameObject.FindGameObjectsWithTag("Farm");

        foreach (GameObject farm in tempFarm)
        {
            farms.Add(farm.GetComponent<Building>());
        }

        // Grounds
        GameObject[] tempGround = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject AGround in tempGround)
        {
            ground.Add(AGround);
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
                   // ant.gameObject.SetActive(true);
                    ant.StartingDay();
                    AntAge antAge = ant.GetComponent<AntAge>();
                    antAge.GainAge();
                    foreach (Transform AntChild in ant.transform)
                    {
                        if (AntChild.gameObject.name == ant.InvisibleAnt(ant.job))
                        {
                            AntChild.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
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
                for (int a = 0; a < ants.Count; a++ )
                {
                    MovingAnt ant = ants[a];

                    for (int j = 0; j < ant.transform.childCount; j++)
                    {
                        Transform AntChild = ant.transform.GetChild(j);
                        if (AntChild.gameObject.name == ant.InvisibleAnt(ant.job))
                        {
                            AntChild.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                        ant.exhausted = false;
                        ant.GoToSleep();
                        if (resource.Food < 1)
                        {
                            ant.die();
                        }
                        else
                        {
                            resource.Food = -1;
                        }            
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

        foreach (Building Forest in forests)
        {
            Building forest = Forest.gameObject.GetComponent<Building>();
            forest.isWork = false;
        }
        foreach (Building Mine in mines)
        {
            Building mine = Mine.gameObject.GetComponent<Building>(); 
            mine.isWork = false;
        }
        foreach (Building Food in foods)
        {
            Building food = Food.gameObject.GetComponent<Building>(); 
            food.isWork = false;
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

        foreach (Building Forest in forests)
        {
            Building forest = Forest.gameObject.GetComponent<Building>();
            forest.isWork = true;
        }
        foreach (Building Mine in mines)
        {
            Building mine = Mine.gameObject.GetComponent<Building>();
            mine.isWork = true;
        }
        foreach (Building Food in foods)
        {
            Building food = Food.gameObject.GetComponent<Building>();
            food.isWork = true;
        }
    }

    public void UpOurHappyness(int amountOfHappyness)
    {
        HappyGauge.happy += amountOfHappyness; 
    }
}