using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{

    public void PauseAnt()
    {
        foreach (MovingAnt ant in GameManager.Instance.ants)
        {
            ant.agent.isStopped = true;
        }
    }

    public void PlayAnt()
    {
        foreach (MovingAnt ant in GameManager.Instance.ants)
        {
            ant.agent.isStopped = false;
        }
    }
}
