using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{
    NavMeshAgent Ant;
    Rigidbody AntRB;
    public bool isStopped;

    // Start is called before the first frame update
    void Start()
    {
        Ant = GetComponent<MovingAnt>().agent;
        AntRB = GetComponent<Rigidbody>();
        AntRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            Ant.speed = 0;
            isStopped = true;
        }
        else
        {
            Ant.speed = 3.5f;
            isStopped = false;
        }
    }

    public void ToggleStopped(bool isStopped)
    {
        isStopped = !isStopped;
    }
}
