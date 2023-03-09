using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface surface;

    // Use this for initialization
 

     public void bakeTheNavigation()
    {
        surface.BuildNavMesh();
    }

}