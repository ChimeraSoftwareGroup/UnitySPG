using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BobeeCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float distance = 10f;
    public float height = 27.98163f; // Modification de la hauteur

    void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position - transform.forward * distance;
        desiredPosition.y = 27.98163f;
        desiredPosition.x = target.position.x;
        desiredPosition.z = target.position.z;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}





