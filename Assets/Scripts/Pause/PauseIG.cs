using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{
    List<LearningAnt> learnAnts = new();

    public void PauseAnt()
    {
        // Pause all learnings in the scene
        foreach (MovingAnt ant in GameManager.Instance.ants)
        {
            LearningAnt tempAnt = ant.gameObject.GetComponent<LearningAnt>();
            if (tempAnt.isLearningAJob)
            {
                learnAnts.Add(tempAnt);
                tempAnt.isPlayLearning = false;
            }
            else
            {
                GameManager.Instance.PauseAnt();
                return;
            }
        }
        GameManager.Instance.PauseAnt();
        //Time.timeScale = 0;
    }

    public void PlayAnt()
    {
        // Restart all learnings in the scene
        foreach (LearningAnt ant in learnAnts)
        {
            ant.isPlayLearning=true;
            learnAnts.Remove(ant);
        }
        GameManager.Instance.PlayAnt();
        //Time.timeScale = 1;
    }
}