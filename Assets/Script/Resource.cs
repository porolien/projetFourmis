using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NumberOfWood;
    public TMPro.TextMeshProUGUI NumberOfStone;
    public TMPro.TextMeshProUGUI NumberOfFood;
    private int wood;
    private int stone;
    private int food;
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
    public int numberWorker;
    // Start is called before the first frame update
    void Start()
    {
        Wood = 0;
        Stone = 0;
        Food = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wood = 15;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stone = 15;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Food = 15;
        }
    }
}
