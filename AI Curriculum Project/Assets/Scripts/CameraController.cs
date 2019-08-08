using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private float distance = 3f;
    private float height = 1f;
    private float lookAtAroundAngle = 180;

    Vector2 rotation = new Vector2(0, 0);
    public float speed = 1f;

    public Transform currentTarget;


    private void Update()
    {
        //Look();

        
        if (currentTarget == null)
        {
            return;
        }

        float targetHeight = currentTarget.position.y + height;
        float currentRotationAngle = lookAtAroundAngle;

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 position = currentTarget.position;
        position -= currentRotation * Vector3.forward * distance;
        position.y = targetHeight;

        transform.position = position;
        transform.LookAt(currentTarget.position + new Vector3(0, height, 0));

        Look();
    }

    void Look()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
    }
}
