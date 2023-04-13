using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Time for product ressources
    public float timeForProduct;

    // For Gain Resources
    public Resource resource;
    public bool isAWorksite;

    // Maximum capacity of the building
    public int capacity;

    // Lists of ants who want to go in the building and ants in the building
    public List<MovingAnt> antsAssignToThisBuilding = new();
    public List<MovingAnt> antsInBuilding = new();

    public bool isWork;

    // Time Variable
    float timeBeforeLast;
    public float ConstructTime;
    void Start()
    {
        isWork = true;
        if(resource == null)
        {
            resource = GameManager.Instance.GetComponent<Resource>();
        }
    }

    //private void FixedUpdate()
    //{
    //    if (antsAssignToThisBuilding.Count == antsInBuilding.Count)
    //    {
    //        antsAssignToThisBuilding.Clear();
    //    }
    //}

    private void Update()
    {
        if (isWork)
        {
            if (tag == "Food" || tag == "Mine" || tag == "Forest")
            {
                // add at every update the time since the last update
                timeBeforeLast += Time.deltaTime * antsInBuilding.Count;	
                if (timeBeforeLast > 7)
                {
                    GainResources();
                    timeBeforeLast = 0;
                }
            }
            else if (tag == "Worksite")
            {
                timeBeforeLast += Time.deltaTime * antsInBuilding.Count;
                if (ConstructTime <= timeBeforeLast)
                {
                    ;
                    gameObject.GetComponent<Construct>().LetHimConstruct();
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ant need to enter in the building
        if (other.TryGetComponent<MovingAnt>(out var movingAnt))
        {
            if (antsAssignToThisBuilding.Contains(movingAnt))
            {
                // Add ant in the building
                antsInBuilding.Add(movingAnt);
                antsAssignToThisBuilding.Remove(movingAnt);
               
                foreach (Transform AntChild in other.transform)
                {
                    if(AntChild.gameObject.name == movingAnt.InvisibleAnt(movingAnt.job))
                    {
                        AntChild.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
                //movingAnt.gameObject.SetActive(false);
                movingAnt.transform.position = new Vector3(gameObject.transform.position.x, movingAnt.transform.position.y, gameObject.transform.position.z);
                if(tag == "School" && movingAnt.job == "student")
                {
                    movingAnt.GetComponent<LearningAnt>().isLearningAJob = true;
                }
            }
        }
        //antsAssignToThisBuilding.FindAll(x => x.job == "vagrant");
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the ant need to exit in the building

        if (other.TryGetComponent<MovingAnt>(out var movingAnt))
        {
            if (antsInBuilding.Contains(movingAnt))
            {
                // Remove ant in the building
                antsInBuilding.Remove(movingAnt);
                if (tag == "School" && movingAnt.job == "student")
                {
                    movingAnt.GetComponent<LearningAnt>().isLearningAJob = false;
                }
            }
        }
    }

    private void GainResources()
    {
        switch (tag)
        {
            case "Food":
                resource.Food = antsInBuilding.Count * 3;
                break;
            case "Mine":
                resource.Stone = antsInBuilding.Count * 3;
                break;
            case "Forest":
                resource.Wood = antsInBuilding.Count * 3;
                break;
        }
    }
}