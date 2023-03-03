using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAContruct : MonoBehaviour
{
    public GameObject cube;
    bool ConstructMod;
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
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.transform.position);
            if (Input.GetMouseButton(0) && ConstructMod)
            { 
                GameObject newCube = Instantiate(cube, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity); 
                ConstructMod = false;
            }   
        }
    }

    public void PutABuilding(Material colors)
    {
        ConstructMod = true;
        cube.GetComponent<MeshRenderer>().material = colors;
    }
}
