using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovingAnt : MonoBehaviour
{
    // Possible ant jobs
    private string[] jobs = new string[] { "vagrant", "lumberjack", "collier", "explorer", "mason"};
    public string job;

    public List<GameObject> skins = new List<GameObject>();

    public NavMeshAgent agent;

    // Place to reach
    private GameObject LastWaypoint;
    public GameObject waypointToReach;

    bool exhausted;

    private void Awake()
    {
        // Choose a job for new ants who haven't job
        if (job == "")
        {
            job = jobs[Random.Range(0, jobs.Length)];
        }
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            skins.Add(transform.GetChild(i).gameObject);
        }

        agent = GetComponent<NavMeshAgent>();
    }

    public void GoTo(GameObject waypoint)
    {
        // Send ant to a place

        agent.SetDestination(waypoint.transform.position);
        //waypoint.GetComponent<Resource>().numberWorker++;
        LastWaypoint = waypoint;
    }

    public void selectDestination()
    {
        // Select a random place for vagrant ants
        waypointToReach = GameManager.Instance.ground[Random.Range(0, GameManager.Instance.ground.Length)];
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if an ant enter in place

        // If the ant is a vagrant : go to an other place
        if (job == "vagrant")
        {
            if (other.transform.position == waypointToReach.transform.position)
            {
                StartCoroutine(VagrantWait());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Check if an ant who's not a vagrant is outside of a zone

        if (job != "vagrant")
        {
        }
        else
        {
            return;
        
        }
    }

    public void StartingDay()
    {
        // Select a destination at the beginning of the day and show the good skin depending
        // of the job
        if(!exhausted || job == "vagrant") 
        {
            foreach (GameObject skinChildren in skins)
            {
                skinChildren.SetActive(false);
            }
            switch (job)
            {
                case ("vagrant"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Vagrant");
                        skin.SetActive(true);

                        selectDestination();
                        break;
                    }
                case ("lumberjack"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Lumberjack");
                        skin.SetActive(true);

                        waypointToReach = GameManager.Instance.forests[Random.Range(0, GameManager.Instance.forests.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        break;
                    }
                case ("collier"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Collier");
                        skin.SetActive(true);

                        waypointToReach = GameManager.Instance.mines[Random.Range(0, GameManager.Instance.mines.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        break;
                    }
                case ("explorer"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Explorer");
                        skin.SetActive(true);

                        waypointToReach = GameManager.Instance.foods[Random.Range(0, GameManager.Instance.foods.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        break;
                    }
                case ("mason"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Mason");
                        skin.SetActive(true);

                        waypointToReach = GameManager.Instance.worksites[Random.Range(0, GameManager.Instance.worksites.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        break;
                    }
                case ("student"):
                    {
                        GameObject skin = skins.Find(x => x.tag == "Vagrant");
                        skin.SetActive(true);

                        waypointToReach = GameManager.Instance.school[Random.Range(0, GameManager.Instance.school.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        break;
                    }
                default:
                    {
                        Debug.Log($"Choose a valid job for {gameObject}");
                        break;
                    }
            }
        }
        GoTo(waypointToReach);
    }

    public void GoToSleep()
    {
        // Send all ants who aren't vagrants to sleep

        if (job != "vagrant")
        {
            bool hasFindAHouse = false;
            foreach (Building house in GameManager.Instance.houses) {
                if (house.antsAssignToThisBuilding.Count < house.capacity)
                {
                    waypointToReach = house.gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    GoTo(waypointToReach);
                    hasFindAHouse = true;
                    exhausted = false;
                    break;
                }
            }
            if (!hasFindAHouse)
            {
               exhausted = true;
            }   
        }
    }

    public IEnumerator VagrantWait()
    {
        // Behaviour of the vagrant

        yield return new WaitForSeconds(Random.Range(0, 5));
        selectDestination();
        GoTo(waypointToReach);
    }
}