using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance = null;
    private GameManager() { }
    public static GameManager Instance => _instance;
    //

    public GameObject ant;

    public List<MovingAnt> ants = new List<MovingAnt>();

    public GameObject[] ground;

    public int dayTime;
    public int nightTime;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }


    void Start()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground");
        MovingAnt[] tempAnts = FindObjectsOfType<MovingAnt>();
        
        foreach (MovingAnt ant in tempAnts)
        {
            ants.Add(ant);
        }
        StartCoroutine(Day());
    }

    public IEnumerator Day()
    {
        GameObject newAnt = Instantiate(ant);
        ants.Add(newAnt.GetComponent<MovingAnt>());

        foreach (MovingAnt ant in ants)
        {
            ant.gameObject.SetActive(true);
            ant.isItStartingDay = true;
        }
        yield return new WaitForSeconds(15f);
        StartCoroutine(Night());
    }

    public IEnumerator Night()
    {
        foreach (MovingAnt ant in ants)
        {
            ant.isItEndingDay = true;
        }
        yield return new WaitForSeconds(15f);
        StartCoroutine(Day());
    }
}
