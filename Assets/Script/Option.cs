using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Option : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    //public TMP_Text TxtVolume;

    public void SliderChange()
    {
        audioSource.volume = slider.value;
       // TxtVolume.text = "Volume" + (audioSource.volume + 100).ToString("00") + "%";

    }
}
