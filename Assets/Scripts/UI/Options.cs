using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    public void SliderChange()
    {
        // Set the audio volume
        audioSource.volume = slider.value;
    }
}
