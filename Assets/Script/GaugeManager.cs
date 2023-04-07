using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeManager : MonoBehaviour
{
   public float happy = 25f;
   public float happyMax = 200f;

    public Image happyBar;


    void Update()
    {
        happyBar.fillAmount = happy / happyMax;

        happy = Mathf.Clamp(happy, 0f, happyMax);
    }
}
