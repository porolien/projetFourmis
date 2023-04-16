using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{
    public Camera cameraFollow;

    private float scrollSpeed = 10;
    // Multipliers depending of the zoom
    private float multiplier;
    private float vectoringMultiplier;

    // Maximum positions in X and Z
    private float maxPos;
    private float minPos;

    private int zoom;

    private void Start()
    {
        multiplier = 2f;
        vectoringMultiplier = 1f;
        maxPos = -14f;
        minPos = -37.5f;
        zoom = 9;
    }

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
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime * multiplier);
        }
        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime * multiplier);
        }
        if (mousePosY < scrollDistance)
        {
            transform.position = new Vector3(transform.position.x + 1 * -scrollSpeed * Time.deltaTime * vectoringMultiplier, transform.position.y, transform.position.z + 1 * -scrollSpeed * Time.deltaTime * vectoringMultiplier);
        }
        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.position = new Vector3(transform.position.x + 1 * scrollSpeed * Time.deltaTime * vectoringMultiplier, transform.position.y, transform.position.z + 1 * scrollSpeed * Time.deltaTime * vectoringMultiplier);
        }

        // Check if the camera is outside the limits
        if (transform.position.x <= minPos)
        {
            transform.position = new Vector3(minPos, transform.position.y, transform.position.z);
        }
        if (transform.position.z <= minPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minPos);
        }
        if (transform.position.x >= maxPos)
        {
            transform.position = new Vector3(maxPos, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= maxPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPos);
        }

        // Zoom the camera
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoom--;
            if(zoom < 0)
            {
                zoom = 1;
            }
            ZoomTheCamera();
        }

        //zoom out the camera
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            zoom++;
            if (zoom > 11)
            {
                zoom = 11;
            }
            ZoomTheCamera();
        }  
    }

    void ZoomTheCamera()
    {
        // Different steps of the zoom
        switch (zoom)
        {
            case 1:
                Camera.main.fieldOfView = 10;
                minPos = -40.5f;
                maxPos = -12f;
                multiplier = 0.4f;
                vectoringMultiplier = 0.3f;
                break;
            case 2:
                Camera.main.fieldOfView = 15;
                minPos = -40f;
                maxPos = -12.5f;
                multiplier = 0.5f;
                vectoringMultiplier = 0.4f;
                break;
            case 3:
                Camera.main.fieldOfView = 25;
                minPos = -39.5f;
                maxPos = -12.5f;
                multiplier = 0.8f;
                vectoringMultiplier = 0.6f;
                break;
            case 4:
                Camera.main.fieldOfView = 35;
                minPos = -39f;
                maxPos = -13f;
                multiplier = 1f;
                vectoringMultiplier = 0.7f;
                break;
            case 5:
                Camera.main.fieldOfView = 45;
                minPos = -38.5f;
                maxPos = -13f;
                multiplier = 1.3f;
                vectoringMultiplier = 0.9f;
                break;
            case 6:
                Camera.main.fieldOfView = 55;
                minPos = -38.5f;
                maxPos = -13f;
                multiplier = 1.6f;
                vectoringMultiplier = 1f;
                break;
            case 7:
                Camera.main.fieldOfView = 65;
                minPos = -38f;
                maxPos = -13.5f;
                multiplier = 1.8f;
                vectoringMultiplier = 1f;
                break;
            case 8:
                Camera.main.fieldOfView = 75;
                minPos = -37.5f;
                maxPos = -14f;
                multiplier = 2f;
                vectoringMultiplier = 1f;
                break;
            case 9:
                Camera.main.fieldOfView = 85;
                minPos = -37.5f;
                maxPos = -14f;
                multiplier = 2f;
                vectoringMultiplier = 1f;
                break;
            case 10:
                Camera.main.fieldOfView = 95;
                minPos = -37.5f;
                maxPos = -14f;
                multiplier = 2f;
                vectoringMultiplier = 1f;
                break;
            case 11:
                Camera.main.fieldOfView = 105;
                minPos = -37f;
                maxPos = -14f;
                multiplier = 2f;
                vectoringMultiplier = 1f;
                break;
        }
    }
}