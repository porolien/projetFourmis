using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseIG : MonoBehaviour
{

    public void PauseAnt()
    {
        GameManager.Instance.PauseAnt();
    }

    public void PlayAnt()
    {
        GameManager.Instance.PlayAnt();
    }
}
