using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingForPlayer : MonoBehaviour
{
    private bool _isCollided = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().PlayerTakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isCollided)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
            collision.gameObject.GetComponent<Player>().PlayerTakeDamage();
            _isCollided = true;
            }
        }
        StartCoroutine(CoroutineCollider());
    }
    IEnumerator CoroutineCollider()
    {
        yield return new WaitForSeconds(3);
    }
    public void ColliderIsActive()
    {
        _isCollided = false; 
    }
}
