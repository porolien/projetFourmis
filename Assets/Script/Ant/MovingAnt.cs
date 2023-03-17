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

    public NavMeshAgent agent;

    // Place to reach
    private GameObject LastWaypoint;
    public GameObject waypointToReach;

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
            if(other.gameObject == LastWaypoint)
            {
                LastWaypoint.GetComponent<Resource>().numberWorker--;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void StartingDay()
    {
        // Select a destination at the beginning of the day

        switch (job)
        {
            case ("vagrant"):
                {
                    selectDestination();
                    break;
                }
            case ("lumberjack"):
                {
                    waypointToReach = GameManager.Instance.forests[Random.Range(0, GameManager.Instance.forests.Count)].gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    break;
                }
            case ("collier"):
                {
                    waypointToReach = GameManager.Instance.mines[Random.Range(0, GameManager.Instance.mines.Count)].gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    break;
                }
            case ("explorer"):
                {
                    waypointToReach = GameManager.Instance.foods[Random.Range(0, GameManager.Instance.foods.Count)].gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    break;
                }
            case ("mason"):
                {
                    waypointToReach = GameManager.Instance.worksites[Random.Range(0, GameManager.Instance.worksites.Count)].gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    break;
                }
            case ("student"):
                {
                    waypointToReach = GameManager.Instance.worksites[Random.Range(0, GameManager.Instance.school.Count)].gameObject;
                    break;
                }
            default:
                {
                    Debug.Log($"Choose a valid job for {gameObject}");
                    break;
                }
        }
        GoTo(waypointToReach);
    }

    public void GoToSleep()
    {
        // Send all ants who aren't vagrants to sleep

        if (job != "vagrant")
        {
            waypointToReach = GameManager.Instance.houses[Random.Range(0, GameManager.Instance.houses.Count)].gameObject;
            Building waypointBuilding = waypointToReach.GetComponent<Building>();
            waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
            GoTo(waypointToReach);
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