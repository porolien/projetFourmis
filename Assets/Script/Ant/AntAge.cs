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
        maxAge = Random.Range(minOld, maxOld);
    }

    public void GainAge()
    {
        age += 1;
        if (age >= maxAge)
        {
            gameObject.GetComponent<MovingAnt>().die();
            /*GameManager.Instance.ants.Remove(gameObject.GetComponent<MovingAnt>());
            Destroy(gameObject);*/
        }
    }
}
