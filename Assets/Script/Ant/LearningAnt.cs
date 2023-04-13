using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt ant;
    public float TimeNeeded;
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
                timeBeforeLast += Time.deltaTime;  		
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
