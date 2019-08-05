using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private float distance = 3f;
    private float height = 1f;
    private float lookAtAroundAngle = 180;

    public Transform currentTarget;

    private void Start()
    {
        
    }

    private void Update()
    {
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
    }
}
