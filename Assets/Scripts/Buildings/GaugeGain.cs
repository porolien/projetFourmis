using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeGain : MonoBehaviour
{
    public GaugeManager gaugeManager;
    public int aLotOfHappy;

   public void GainSomeHappyness()
    {
        // Increase the gauge of happiness
        gaugeManager = GameManager.Instance.happyGauge;
        gaugeManager.happy += aLotOfHappy;
    }
}