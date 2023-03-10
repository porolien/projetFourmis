using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance = null;
    private GameManager() { }
    public static GameManager Instance => _instance;

    public int dayTime;
    public int nightTime;

    public bool isItDay;

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
        
    }

    public IEnumerator Day()
    {
        isItDay = true;
        yield return new WaitForSeconds(15f);
        StartCoroutine(Night());
    }

    public IEnumerator Night()
    {
        isItDay = false;
        yield return new WaitForSeconds(15f);
        StartCoroutine(Night());
    }
}
