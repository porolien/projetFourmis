using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt ant;

    string TheLearningJob;
    public bool isLearningAJob;

    public float TimeNeeded;
    float timeBeforeLast;

    // Check if the game was on play or pause
    public bool isPlayLearning;

    void Start()
    {
        ant = GetComponent<MovingAnt>();
        isPlayLearning = true;
    }

    void Update()
    {
        // Decrease time to learn if the ant is learning a job and if the game is not pause
        // If the ant has finished to learn, she is exhausted
        if (isPlayLearning)
        {
            if (isLearningAJob)
            {
                timeBeforeLast += Time.deltaTime;  		
                if (timeBeforeLast > 1)
                {                 
                    TimeNeeded--;
                    timeBeforeLast = 0;
                    if (TimeNeeded <= 0)
                    {
                        ant.exhausted = true;
                        isLearningAJob = false;
                        ant.job = TheLearningJob;
                    }
                }
            }
        }   
    }

    public void LearnAJob(string AJobToLearn)
    {
        // Launch the learning depending of the job
        isLearningAJob = true;
        
        switch (AJobToLearn)
        {
            case "lumberjack":
                TimeNeeded = 5f;
                TheLearningJob = AJobToLearn;
                break;
            case "collier":
                TimeNeeded = 40f;
                TheLearningJob = AJobToLearn;
                break;
            case "explorer":
                TimeNeeded = 50f;
                TheLearningJob = AJobToLearn;
                break;
            case "mason":
                TimeNeeded = 60f;
                TheLearningJob = AJobToLearn;
                break;
        }
    }
}
