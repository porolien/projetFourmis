using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{

    public void PauseAnt()
    {
        GameManager.Instance.PauseAnt();
        Time.timeScale = 0;
    }

    public void PlayAnt()
    {
        GameManager.Instance.PlayAnt();
        Time.timeScale = 1;
    }
}
