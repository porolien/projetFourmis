using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public TextMeshProUGUI NumberOfWood;
    public TextMeshProUGUI NumberOfStone;
    public TextMeshProUGUI NumberOfFood;
    private int wood;
    private int stone;
    private int food;

    //the way to get some resource
    public int Wood
    { 
        get { return wood; }
        set { wood = value + wood;
            NumberOfWood.text = "" + wood;   }
    }
    public int Stone
    {
        get { return stone; }
        set
        {
            stone = value + stone;
            NumberOfStone.text = "" + stone;
        }
    }
    public int Food
    {
        get { return food; }
        set
        {
            food = value + food;
            NumberOfFood.text = "" + food;
        }
    }
    public string resource;
    private bool onAWork;

    void Start()
    {
        Wood = 0;
        Stone = 0;
        Food = 0;
    }
}