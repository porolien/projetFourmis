using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{

    public float horizontalSpeed = 50;
    public float verticalSpeed = 50;
    private int Zoom = 3;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float horizontalOffset = horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            float verticalOffset = verticalSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            transform.Translate(horizontalOffset, verticalOffset, 0);
        }
        //zoom la caméra
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
