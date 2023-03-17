using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    // Time for product ressources
    public float timeForProduct;

    // Maximum capacity of the building
    public int capacity;

    // Lists of ants who want to go in the building and ants in the building
    public List<MovingAnt> antsAssignToThisBuilding = new();
    public List<MovingAnt> antsInBuilding = new();

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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ant need to enter in the building
        if (other.TryGetComponent<MovingAnt>(out var movingAnt))
        {
            if (antsAssignToThisBuilding.Contains(movingAnt))
            {
                // Add ant in the building
                antsInBuilding.Add(movingAnt);
                movingAnt.gameObject.SetActive(false);
                movingAnt.transform.position = transform.position;
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
            }
        }
    }
}
