using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheCamera : MonoBehaviour
{

    public float horizontalSpeed = 50;
    public float verticalSpeed = 50;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float horizontalOffset = horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            float verticalOffset = verticalSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            transform.Translate(horizontalOffset, verticalOffset, 0);
        }
    }

}
