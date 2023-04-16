using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovingAnt : MonoBehaviour
{
    // Possible ant jobs
    private string[] jobs = new string[] { "vagrant", "lumberjack", "collier", "explorer", "mason" };
    public string job;

    public List<GameObject> skins = new();

    public NavMeshAgent agent;

    // Place to reach
    private GameObject LastWaypoint;
    public GameObject waypointToReach;

    public bool isWorking;
    public bool exhausted;

    private void Awake()
    {
        // Choose a job for new ants who haven't job
        if (job == "")
        {
            job = jobs[Random.Range(0, jobs.Length)];
        }

        // Retrieve the list of all skins in children
        for (int i = 0; i < transform.childCount; i++)
        {
            skins.Add(transform.GetChild(i).gameObject);
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
        LastWaypoint = waypoint;
    }

    public void SelectDestination()
    {
        // Select a random place for vagrant ants
        waypointToReach = GameManager.Instance.ground[Random.Range(0, GameManager.Instance.ground.Count)];
    }

    public void OnTriggerEnter(Collider other)
    {
        // If the ant is a vagrant : go to an other place
        if (job == "vagrant" || exhausted)
        {
            if (waypointToReach != null)
            {
                if (other.transform.position == waypointToReach.transform.position)
                {
                    StartCoroutine(VagrantWait());
                }
            }
        }
    }

    public void StartingDay()
    {
        // Select a destination at the beginning of the day and show the good skin depending
        // of the job
        // If the ant is exhausted, she behaves as a vagrant
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

                    StartCoroutine(VagrantWait());
                    break;
                }
            case ("lumberjack"):
                {
                    GameObject skin = skins.Find(x => x.tag == "Lumberjack");
                    skin.SetActive(true);

                    if (!exhausted && GameManager.Instance.forests.Count >= 0)
                    {
                        waypointToReach = GameManager.Instance.forests[Random.Range(0, GameManager.Instance.forests.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        GoTo(waypointToReach);
                    }
                    else
                    {
                        StartCoroutine(VagrantWait());
                    }
                    break;
                }
            case ("collier"):
                {
                    GameObject skin = skins.Find(x => x.tag == "Collier");
                    skin.SetActive(true);

                    if (!exhausted && GameManager.Instance.mines.Count > 0)
                    {
                        waypointToReach = GameManager.Instance.mines[Random.Range(0, GameManager.Instance.mines.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        GoTo(waypointToReach);
                    }
                    else
                    {
                        StartCoroutine(VagrantWait());
                    }
                    break;
                }
            case ("explorer"):
                {
                    // Send explorer ants to a farm in priority and to a bush if it's full
                    GameObject skin = skins.Find(x => x.tag == "Explorer");
                    skin.SetActive(true);

                    if (!exhausted && GameManager.Instance.foods.Count > 0)
                    {
                        for(int i = 0; i < GameManager.Instance.farms.Count; i++)
                        {
                            if (GameManager.Instance.farms[i].GetComponent<Building>().antsAssignToThisBuilding.Count < GameManager.Instance.farms[i].GetComponent<Building>().capacity && waypointToReach.tag != "Farm" )
                            {
                                waypointToReach = GameManager.Instance.farms[i].gameObject;
                            }
                        }
                        if(waypointToReach != null)
                        {
                            if (waypointToReach.tag != "Farm" )
                            {
                                waypointToReach = GameManager.Instance.foods[Random.Range(0, GameManager.Instance.foods.Count)].gameObject;
                            }
                        }
                        else
                        {
                            waypointToReach = GameManager.Instance.foods[Random.Range(0, GameManager.Instance.foods.Count)].gameObject;
                        }
                        
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        GoTo(waypointToReach);
                    }
                    else
                    {
                        StartCoroutine(VagrantWait());
                    }
                    break;
                }
            case ("mason"):
                {
                    // If the mason doesen't find a worksite, she behaves as a vagrant
                    GameObject skin = skins.Find(x => x.tag == "Mason");
                    skin.SetActive(true);
                    if (!exhausted && GameManager.Instance.worksites.Count > 0)
                    {
                        waypointToReach = GameManager.Instance.worksites[Random.Range(0, GameManager.Instance.worksites.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        GoTo(waypointToReach);
                        isWorking = true;
                    }
                    else
                    {
                        StartCoroutine(VagrantWait());
                    }
                    break;
                }
            case ("student"):
                {
                    GameObject skin = skins.Find(x => x.tag == "Vagrant");
                    skin.SetActive(true);

                    if (!exhausted && GameManager.Instance.schools.Count > 0)
                    {
                        waypointToReach = GameManager.Instance.schools[Random.Range(0, GameManager.Instance.schools.Count)].gameObject;
                        Building waypointBuilding = waypointToReach.GetComponent<Building>();
                        waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                        GoTo(waypointToReach);
                    }
                    else
                    {
                        StartCoroutine(VagrantWait());
                    }
                    break;
                }
            default:
                {
                    Debug.Log($"Choose a valid job for {gameObject}");
                    break;
                }
        }
    }

    public void GoToSleep()
    {
        // Send all ants who aren't vagrants to sleep
        if (job != "vagrant")
        {
            bool hasFindAHouse = false;
            foreach (Building house in GameManager.Instance.houses)
            {
                if (house.antsAssignToThisBuilding.Count < house.capacity)
                {
                    waypointToReach = house.gameObject;
                    Building waypointBuilding = waypointToReach.GetComponent<Building>();
                    waypointBuilding.antsAssignToThisBuilding.Add(gameObject.GetComponent<MovingAnt>());
                    GoTo(waypointToReach);
                    hasFindAHouse = true;
                    GameManager.Instance.UpOurHappyness(1);
                    exhausted = false;
                    break;
                    
                }
            }
            // If the ant has not find a house, she behaves as a vagrant and she is exhausted
            // and sad
            if (!hasFindAHouse)
            {
                GameManager.Instance.UpOurHappyness(-1);
                exhausted = true;
                StartingDay();
            }
            isWorking = false;
        }
    }

    public IEnumerator VagrantWait()
    {
        // Behaviour of the vagrant
        yield return new WaitForSeconds(Random.Range(0, 5));
        if (!isWorking)
        {
            SelectDestination();
            GoTo(waypointToReach);
        } 
    }

    public string InvisibleAnt(string ObjectName)
    {
        switch (job)
        {
            case "lumberjack":
                ObjectName = "Lumberjack";
                break;
            case "collier":
                ObjectName = "Collier";
                break;
            case "mason":
                ObjectName = "Mason";
                break;
            case "explorer":
                ObjectName = "Explorer";
                break;
            case "vagrant":
                ObjectName = "Vagrant";
                break;
            case "student":
                ObjectName = "Vagrant";
                break;
        }
        return ObjectName;
    }

    public void Death()
    {
      /* for (int i = 0; i < GameManager.Instance.ants.Count; i++)
        {
            if (GameManager.Instance.ants[i] == gameObject.GetComponent<MovingAnt>())
            {
                GameManager.Instance.ants.Remove(GameManager.Instance.ants[i]);
            }
        }
        Destroy(gameObject);*/
    }

    public IEnumerator leftTheConstruct()
    {
        yield return new WaitForSeconds(0.1f);
        StartingDay();
    }
    public void wantToLeft()
    {
        StartCoroutine(leftTheConstruct());
    }
}