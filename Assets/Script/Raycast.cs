using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public CreateAContruct createAContruct;
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
            if (tagFromHit == "Groud")
            {

            }
            else if(tagFromHit == "Ant")
            {

            }
        }
    }
}
