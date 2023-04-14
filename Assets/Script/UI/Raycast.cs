using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public CreateAContruct createAContruct;
    public PlayerUi playerUi;

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            
            string tagFromHit = hit.transform.gameObject.tag;
            if (tagFromHit == "Ground" && gameObject.tag != "Ant")
            {
               
                if(createAContruct.ConstructMod == true)
                {   
                    createAContruct.create(hit);
                }
            }
            else if(tagFromHit == "Ant")
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log(hit.transform.gameObject.GetComponent<MovingAnt>().job);
                    playerUi.ant = hit.transform.gameObject;
                    playerUi.antwindow.SetActive(true);
                    playerUi.Init();
                }
            }
        }
    }
}
