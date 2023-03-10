using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingAnt : MonoBehaviour
{
    private string[] jobs = new string[] { "vagrant", "lumberjack", "collier", "explorer", "mason" };
    public string job;

    public bool isItStartingDay;
    public bool isItEndingDay;

    public GameObject[] forests;
    public GameObject[] mines;
    public GameObject[] foods;
    public GameObject[] worksites;

    public NavMeshAgent agent;

    private GameObject LastWaypoint;
    public GameObject waypointToReach;

    void Start()
    {
        forests = GameObject.FindGameObjectsWithTag("Forest");
        mines = GameObject.FindGameObjectsWithTag("Mine");
        foods = GameObject.FindGameObjectsWithTag("Food");
        worksites = GameObject.FindGameObjectsWithTag("Worksite");

        agent = GetComponent<NavMeshAgent>();

        if (job == "")
        {
            job = jobs[Random.Range(0, jobs.Length)];
        }
    }

    private void FixedUpdate()
    {
        if (isItStartingDay)
        {
            StartingDay();
        }
        if (isItEndingDay)
        {
            GoToSleep();
        }
    }

    public void GoTo(GameObject waypoint)
    {
        agent.SetDestination(waypoint.transform.position);
        waypoint.GetComponent<Resource>().numberWorker++;
        LastWaypoint = waypoint;
    }

    public void selectDestination()
    {
        waypointToReach = GameManager.Instance.ground[Random.Range(0, GameManager.Instance.ground.Length)];
    }

    public void OnTriggerEnter(Collider other)
    {
        if (job == "vagrant")
        {
            if (other.transform.position == waypointToReach.transform.position)
            {
                StartCoroutine(VagrantWait());
            }
        }
        else
        {
            if (other.transform.position == waypointToReach.transform.position)
            {
                if (other.gameObject.tag == "House")
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.position = transform.position;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
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
        isItStartingDay = false;
        switch (job)
        {
            case ("vagrant"):
                {
                    selectDestination();
                    break;
                }
            case ("lumberjack"):
                {
                    waypointToReach = forests[Random.Range(0, forests.Length)];
                    break;
                }
            case ("collier"):
                {
                    waypointToReach = mines[Random.Range(0, mines.Length)];
                    break;
                }
            case ("explorer"):
                {
                    waypointToReach = foods[Random.Range(0, foods.Length)];
                    break;
                }
            case ("mason"):
                {
                    waypointToReach = worksites[Random.Range(0, worksites.Length)];
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
        isItEndingDay = false;
        if (job != "vagrant")
        {
            waypointToReach = GameObject.FindGameObjectWithTag("House");
            GoTo(waypointToReach);
        }
    }

    public IEnumerator VagrantWait()
    {
        yield return new WaitForSeconds(Random.Range(0, 5));
        selectDestination();
        GoTo(waypointToReach);
    }
}
