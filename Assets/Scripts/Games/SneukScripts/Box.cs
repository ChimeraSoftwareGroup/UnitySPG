using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ObjectsToSpawn
{

    [SerializeField] GameObject _hitBox;
    [SerializeField] GameObject _boxSprite;
    [SerializeField] GameObject _boxDestructionAnimation;

    private void OnCollisionEnter(Collision collision)
    {
        // We remove the hitbox on contact with floor / player
        _hitBox.gameObject.SetActive(false);
        
        Destroy(gameObject, .5f);
        // To avoid box falling into void
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

    }
}
