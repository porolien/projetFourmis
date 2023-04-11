using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt ant;
    float TimeNeeded;
    public bool isLearningAJob;
    float timeBeforeLast;
    string TheLearningJob;

    //Check if the game was on play or pause
    public bool isPlayLearning;

    // Start is called before the first frame update
    void Start()
    {
        ant = GetComponent<MovingAnt>();
        isPlayLearning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayLearning)
        {
            if (isLearningAJob)
            {
                timeBeforeLast += Time.deltaTime;  // ajoute a chaque update le temps écoulé depuis le dernier Update		
                if (timeBeforeLast > 1)
                {
                    TimeNeeded--;
                    timeBeforeLast = 0;
                    if (TimeNeeded <= 0)
                    {

                        isLearningAJob = false;
                        ant.job = TheLearningJob;
                    }
                }
            }
        }
        
    }

    public void LearnAJob(string AJobToLearn)
    {
 
        switch (AJobToLearn)
        {
            case "Lumberjack":
                TimeNeeded = 30f;
                break;
            case "Collier":
                TimeNeeded = 40f;
                break;
            case "Explorer":
                TimeNeeded = 50f;
                break;
            case "Mason":
                TimeNeeded = 60f;
                break;
        }
        isLearningAJob = true;
        TheLearningJob = AJobToLearn;
   
    }
}
