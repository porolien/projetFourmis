using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt Ant;
    float TimeNeeded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(LearningAJob(TimeNeeded, AJobToLearn));
    }

    public IEnumerator LearningAJob(float TimeToLearnAJob, string AJobToLearn)
    { bool finishedToLearn = false;
        while (!finishedToLearn)
        {
            yield return new WaitForSeconds(TimeToLearnAJob);
            if (TimeToLearnAJob < TimeNeeded)
            {
                TimeToLearnAJob = TimeNeeded - TimeToLearnAJob;
            }
            else
            {
                finishedToLearn = true;
            }
        }
        Ant.job = AJobToLearn;

    }
}
