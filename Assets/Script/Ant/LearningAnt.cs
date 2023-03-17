using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt Ant;
    float TimeNeeded;
    bool isLearningAJob;
    float timeBeforeLast;
    string TheLearningJob;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                    Ant.job = TheLearningJob;
                }
            }
        }
    }

    public void LearnAJob( string AJobToLearn)
    {
 
        switch (AJobToLearn)
        {
            case "lumberjack":
                TimeNeeded = 30f;
                break;
            case "collier":
                TimeNeeded = 40f;
                break;
            case "explorer":
                TimeNeeded = 50f;
                break;
            case "mason":
                TimeNeeded = 60f;
                break;
        }
        isLearningAJob = true;
        TheLearningJob = AJobToLearn;
   
    }
}
