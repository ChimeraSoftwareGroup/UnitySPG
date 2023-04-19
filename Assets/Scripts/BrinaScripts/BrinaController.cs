using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrinaController : MonoBehaviour
{
    public int force = 200;
    [SerializeField] Rigidbody rb;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * force);
        }
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            print("OBSTACLE");
        }
    }
}
