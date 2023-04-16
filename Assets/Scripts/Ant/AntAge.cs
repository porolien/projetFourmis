using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAge : MonoBehaviour
{
    [SerializeField] private int minOld = 15;
    [SerializeField] private int maxOld = 20;
    public int maxAge;
    public int age = 0;

    void Start()
    {
        // Set the maximum age of the ant
        maxAge = Random.Range(minOld, maxOld);
    }

    public void GainAge()
    {
        // Call at each day
        // ant is getting old and dies if she is too old
        age += 1;
        if (age >= maxAge)
        {
            gameObject.GetComponent<MovingAnt>().Death();
            /*GameManager.Instance.ants.Remove(gameObject.GetComponent<MovingAnt>());
            Destroy(gameObject);*/
        }
    }
}
