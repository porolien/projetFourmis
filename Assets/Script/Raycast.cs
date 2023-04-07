using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public CreateAContruct createAContruct;
    public PlayerUi playerUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            string tagFromHit = hit.transform.gameObject.tag;
            if (tagFromHit == "Ground")
            {
               
                if(createAContruct.ConstructMod == true)
                {
                    Debug.Log("jetouchemieuxle sol");
                    createAContruct.create(hit);
                }
            }
            else if(tagFromHit == "Ant")
            {
                if (Input.GetMouseButton(0))
                {
                    playerUi.antwindow.SetActive(true);
                }
            }
            else if(tagFromHit == "house" || tagFromHit == "Forest" || tagFromHit == "Mine" || tagFromHit == "Food")
            {
                if (Input.GetMouseButton(0))
                {

                }
            }
        }
    }
}
