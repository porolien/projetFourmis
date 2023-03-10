using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningAnt : MonoBehaviour
{
    public MovingAnt Ant;
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
        float TimeNeeded = 0;
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
        StartCoroutine(LearningAJob(TimeNeeded));
    }

    public IEnumerator LearningAJob(float TimeToLearnAJob)
    {
        yield return new WaitForSeconds(TimeToLearnAJob);
    }
}
