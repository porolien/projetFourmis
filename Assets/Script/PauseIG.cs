using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{
    public NavMeshAgent Ant;

    // Start is called before the first frame update
    void Start()
    {
        Ant = GetComponent<MovingAnt>().agent;
    }

    public void PauseAnt()
    {
        //AntRB.constraints = RigidbodyConstraints.FreezeAll;
        Ant.isStopped = true;
    }

    public void PlayAnt()
    {
        //AntRB.constraints = RigidbodyConstraints.None;
        Ant.isStopped = false;
    }
}
