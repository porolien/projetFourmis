using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{


    public float sensitivity = 5f;
    private int Zoom = 3;
    public Camera cameraFollow;



     private float aze = 0f;
     int bze = 31;

    private Vector3 cameraFollowPosition;

    public float scrollSpeed = 10;

    private void Start()
    {
        
    }




    public void Update()
    {
        //Mouving caméra

        var mPosX = Input.mousePosition.x;
        var mPosY = Input.mousePosition.y;


        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 10;
        if (mousePosX < scrollDistance)
        {
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }

        if (mousePosY < scrollDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + 1 * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + 1 * scrollSpeed * Time.deltaTime);
        }

        Debug.Log(cameraFollow.transform.position);

        if (transform.position.x >= 0)
        {
            transform.position = new Vector3 (0, transform.position.y, transform.position.z);
        }

        else if(transform.position.x <= -40)
        {
            transform.position = new Vector3(-40, transform.position.y, transform.position.z);
        }

        else if (transform.position.z >= -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        else if (transform.position.z <= -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }





        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
            Zoom--;
            if(Zoom < 0)
            {
                Zoom = 1;
            }
            ZoomTheCamera();
        }
        //Dezoom la caméra
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
            Zoom++;
            if (Zoom > 5)
            {
                Zoom = 5;
            }
            ZoomTheCamera();
        }
        
    }

    void ZoomTheCamera()
    {
        switch (Zoom)
        {
            case 1:
                Camera.main.fieldOfView = 45;
                break;
            case 2:
                Camera.main.fieldOfView = 55;
                break;
            case 3:
                Camera.main.fieldOfView = 65;
                break;
            case 4:
                Camera.main.fieldOfView = 75;
                break;
            case 5:
                Camera.main.fieldOfView = 85;
                break;
        }
    }



}
