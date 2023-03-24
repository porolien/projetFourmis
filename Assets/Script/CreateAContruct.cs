using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class CreateAContruct : MonoBehaviour
{
    GameObject cube;
    public bool ConstructMod;
    public GameObject cubePrevisual;
    public Resource Resource;
    public NavMeshSurface navMeshSurface;
    public NavigationBaker navigationBaker;
    // Start is called before the first frame update
    void Start()
    {
        // navMeshSurface = cubePrevisual.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    /*    if (ConstructMod)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                string tagFromHit = hit.transform.gameObject.tag;
                if (tagFromHit == "Groud")
                {
                    cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);


                    if (Input.GetMouseButton(0))
                    {
                        hit.transform.gameObject.tag = "Untagged";
                        cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
                        Resource.Wood = -cube.GetComponent<Construct>().woodCost;
                        Resource.Stone = -cube.GetComponent<Construct>().stoneCost;
                        Resource.Food = -cube.GetComponent<Construct>().foodCost;
                        cubePrevisual.SetActive(false);
                        GameObject newCube = Instantiate(cube, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity);
                        ConstructMod = false;
                        navigationBaker.bakeTheNavigation();
                    }
                    if (Input.GetMouseButton(1))
                    {
                        cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
                        cubePrevisual.SetActive(false);
                        ConstructMod = false;
                    }
                }
            }
        }
    */
    }

    public void PutABuilding(GameObject aConstruct)
    {
       Construct construct = aConstruct.GetComponent<Construct>();
        if (construct.woodCost <= Resource.Wood && construct.stoneCost <= Resource.Stone) 
        {
            cube = aConstruct;
            ConstructMod = true;
            cubePrevisual.SetActive(true);
            ConstructMod = true;
        }
    }
    public void create(RaycastHit hit)
    {
        cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);


        if (Input.GetMouseButton(0))
        {
            hit.transform.gameObject.tag = "Untagged";
            cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
            Resource.Wood = -cube.GetComponent<Construct>().woodCost;
            Resource.Stone = -cube.GetComponent<Construct>().stoneCost;
            cubePrevisual.SetActive(false);
            GameObject newCube = Instantiate(cube, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity);
            ConstructMod = false;
            navigationBaker.bakeTheNavigation();
        }
        if (Input.GetMouseButton(1))
        {
            cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
            cubePrevisual.SetActive(false);
            ConstructMod = false;
        }
    }
}
