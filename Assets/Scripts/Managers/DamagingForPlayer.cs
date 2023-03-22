using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingForPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("HELLO PLAYER");

            other.gameObject.GetComponent<Player>().PlayerTakeDamage();
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("HELLO PLAYER");
            collision.gameObject.GetComponent<Player>().PlayerTakeDamage();
            
        }
    }
   
}
