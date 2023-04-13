using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeGain : MonoBehaviour
{
    public GaugeManager gaugeManager;
    public int aLotOfHappy;
    // Start is called before the first frame update
   public void gainSomeHappyness()
    {
        gaugeManager = GameManager.Instance.HappyGauge;
        gaugeManager.happy += aLotOfHappy;
    }
}
