using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter(Collider other)
    {
        // If it's the waypoint to reach : enter in

        foreach (MovingAnt ant in antsAssignToThisBuilding)
        {
            if (ant == other)
            {
                // Add ant in the building
                antsInBuilding.Add(ant);
                ant.gameObject.SetActive(false);
                ant.transform.position = transform.position;
            }
        }
    }
}
