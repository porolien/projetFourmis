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

    // Maximum capacity of the building
    public int capacity;

    // Lists of ants who want to go in the building and ants in the building
    public List<MovingAnt> antsAssignToThisBuilding = new();
    public List<MovingAnt> antsInBuilding = new();

    float timeBeforeLast;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (antsAssignToThisBuilding.Count == antsInBuilding.Count)
        {
            antsAssignToThisBuilding.Clear();
        }
    }
    private void Update()
    {
        if (tag == "Food" || tag == "Mine" || tag == "Forest")
        {
            timeBeforeLast += Time.deltaTime;  // ajoute a chaque update le temps écoulé depuis le dernier Update		
            if (timeBeforeLast > 5)
            {
                GainResources();
                timeBeforeLast = 0;
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
                movingAnt.graphicComponents.SetActive(false);
                movingAnt.transform.position = transform.position;
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
