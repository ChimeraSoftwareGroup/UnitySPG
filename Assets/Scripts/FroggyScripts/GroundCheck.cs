using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundCheck : MonoBehaviour
{
    public float distanceToCheck = 2f;
    public bool isGrounded;
    public RaycastHit hit;
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, out hit, distanceToCheck);
    }
}