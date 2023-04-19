using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchesMovement : MonoBehaviour
{
    [SerializeField] int _speed = 5;
    [SerializeField] Rigidbody rb;
    [SerializeField] float range = 2f;
    void Start()
    {
        rb.velocity = -Vector2.right * _speed;
        transform.position = new Vector3(transform.position.x, transform.position.y - range * Random.value, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
