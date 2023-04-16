using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GaugeManager : MonoBehaviour
{
   public float happy = 25f;
   public float happyMax = 200f;

   public Image happyBar;

    void Update()
    {
        // Fill the gauge of happiness
        // If the gauge is completely full, victory
        //If the gauge is completly empty, defeat
        happyBar.fillAmount = happy / happyMax;

        happy = Mathf.Clamp(happy, 0f, happyMax);
        if(happy >= happyMax)
        {
            SceneManager.LoadScene("Victory");
        }
        if (happy <= 0)
        {
            SceneManager.LoadScene("Defeat");
        }
    }
}
