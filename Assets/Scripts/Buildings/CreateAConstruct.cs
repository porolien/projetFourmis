using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class CreateAConstruct : MonoBehaviour
{
    public GameObject cube;
    public bool constructMod;
    public GameObject cubePrevisual;
    public GameObject largeCubePrevisual;
    public Resource resource;
    public NavMeshSurface navMeshSurface;
    public NavigationBaker navigationBaker;
    public GameObject parentOfBLoc;

    private void Awake()
    {
        // Bake the Navmesh a first time
        navigationBaker.BakeTheNavigation();
    }

    public void PutABuilding(GameObject aConstruct)
    {
        Construct construct = aConstruct.GetComponent<Construct>();
        if (construct.woodCost <= resource.Wood && construct.stoneCost <= resource.Stone)
        {
            cube = aConstruct;
            constructMod = true;
            cubePrevisual.SetActive(true);
        }
    }
    public void Create(RaycastHit hit)
    {
        cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);

        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < GameManager.Instance.ground.Count; i++)
            {
                if (GameManager.Instance.ground[i] == hit.transform.gameObject)
                {
                    GameManager.Instance.ground.Remove(GameManager.Instance.ground[i]);
                }
            }
            hit.transform.gameObject.tag = "Untagged";
            cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
            cubePrevisual.SetActive(false);

            resource.Wood = -cube.GetComponent<Construct>().woodCost;
            resource.Stone = -cube.GetComponent<Construct>().stoneCost;
            GameObject newCube = Instantiate(cube, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), Quaternion.identity);
            newCube.GetComponent<Construct>().contructThePrefab();
            constructMod = false;
            newCube.transform.SetParent(parentOfBLoc.transform);
            GameManager.Instance.worksites.Add(newCube.GetComponent<Building>());
            for (int i = 0; i < GameManager.Instance.ants.Count; i++)
            {
                if (GameManager.Instance.ants[i].job == "mason" && GameManager.Instance.isItDay && !GameManager.Instance.ants[i].isWorking)
                {
                    GameManager.Instance.ants[i].StartingDay();
                }
            }
            navigationBaker.BakeTheNavigation();
        }
        if (Input.GetMouseButton(1))
        {
            cubePrevisual.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - 2, hit.transform.position.z);
            cubePrevisual.SetActive(false);
            constructMod = false;
        }
    }
}