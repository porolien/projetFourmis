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
                    createAContruct.create(hit);
                }
            }
            else if(tagFromHit == "Ant")
            {
                if (Input.GetMouseButton(0))
                {
                    playerUi.ant = hit.transform.gameObject;
                    playerUi.antwindow.SetActive(true);
                    playerUi.Init();
                }
            }
        }
    }
}
