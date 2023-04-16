using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{
    public float sensitivity = 5f;
    private int Zoom = 3;
    public Camera cameraFollow;

    public int poseCamera = -31;

    public float scrollSpeed = 10;

    public void Update()
    {
        // Camera movements
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
            transform.position = new Vector3(transform.position.x + 1 * -scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z + 1 * -scrollSpeed * Time.deltaTime);
        }
        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.position = new Vector3(transform.position.x + 1 * scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z + 1 * scrollSpeed * Time.deltaTime);
        }

        // Check if the camera is outside the limits
        if (transform.position.x <= -35)
        {
            transform.position = new Vector3(-35, transform.position.y, transform.position.z);
        }
        if (transform.position.z <= -35)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -35);
        }
        if (transform.position.x >= 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        // Zoom the camera
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Zoom--;
            if(Zoom < 0)
            {
                Zoom = 1;
            }
            ZoomTheCamera();
        }

        //zoom out the camera
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
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
        // Different steps of the zoom
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