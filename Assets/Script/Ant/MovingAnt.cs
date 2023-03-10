using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingAnt : MonoBehaviour
{
    public bool isItDay = true;
    public string job;

    public GameObject[] ground;
    public GameObject[] forests;
    public GameObject[] mines;
    public GameObject[] foods;
    public GameObject[] worksites;

    public MeshRenderer meshRenderer;
    public NavMeshAgent agent;
    private GameObject LastWaypoint;
    public GameObject waypointToReach;
    void Start()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground");
        forests = GameObject.FindGameObjectsWithTag("Forest");
        mines = GameObject.FindGameObjectsWithTag("Mine");
        foods = GameObject.FindGameObjectsWithTag("Food");
        worksites = GameObject.FindGameObjectsWithTag("Worksite");


        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();

        transform.position = GameObject.FindGameObjectWithTag("House").transform.position;
        StartCoroutine(Day());
    }

    private void Update()
    {
        
    }

    public void GoTo(GameObject waypoint)
    {
        agent.SetDestination(waypoint.transform.position);
        waypoint.GetComponent<Resource>().numberWorker++;
        LastWaypoint = waypoint;
    }

    public void selectDestination()
    {
        waypointToReach = ground[Random.Range(0, ground.Length)];
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
                    meshRenderer.enabled = false;
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
            if (other.gameObject.tag == "House")
            {
                meshRenderer.enabled = true;
            }
            else if(other.gameObject == LastWaypoint)
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

    public IEnumerator Day()
    {
        isItDay = true;
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
        yield return new WaitForSeconds(15f);
        StartCoroutine(Night());
    }

    public IEnumerator Night()
    {
        isItDay = false;
        if (job != "vagrant")
        {
            waypointToReach = GameObject.FindGameObjectWithTag("House");
            GoTo(waypointToReach);
        }
        yield return new WaitForSeconds(15f);
        StartCoroutine(Day());
    }

    public IEnumerator VagrantWait()
    {
        yield return new WaitForSeconds(Random.Range(0, 5));
        selectDestination();
        GoTo(waypointToReach);
    }
}
