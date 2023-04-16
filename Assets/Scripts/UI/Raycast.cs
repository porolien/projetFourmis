using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public CreateAConstruct createAContruct;
    public PlayerUi playerUi;

    void FixedUpdate()
    {
        // Check on what we click in the scene
        // Use to open ant sheet and place building
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {    
            string tagFromHit = hit.transform.gameObject.tag;
            if (tagFromHit == "Ground" && gameObject.tag != "Ant")
            {
               
                if(createAContruct.constructMod == true)
                {   
                    createAContruct.Create(hit);
                }
            }
            else if(tagFromHit == "Ant")
            {
                if (Input.GetMouseButton(0))
                {
                    playerUi.ant = hit.transform.gameObject;
                    playerUi.antwindow.SetActive(true);
                    playerUi.DisplayInformations();
                }
            }
        }
    }
}
