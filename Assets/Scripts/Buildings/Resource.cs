using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public TextMeshProUGUI numberOfWood;
    public TextMeshProUGUI numberOfStone;
    public TextMeshProUGUI numberOfFood;
    private int wood;
    private int stone;
    private int food;

    //the way to get some resource
    public int Wood
    { 
        get { return wood; }
        set { wood = value + wood;
            numberOfWood.text = "" + wood;   }
    }
    public int Stone
    {
        get { return stone; }
        set
        {
            stone = value + stone;
            numberOfStone.text = "" + stone;
        }
    }
    public int Food
    {
        get { return food; }
        set
        {
            food = value + food;
            numberOfFood.text = "" + food;
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