using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ObjectsToSpawn
{

    [SerializeField] GameObject _hitBox;
    [SerializeField] GameObject _boxSprite;
    [SerializeField] GameObject _boxDestructionAnimation;
    [SerializeField] GameObject _boxExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        _boxExplosion.SetActive(true);
        _hitBox.gameObject.SetActive(false);
        // We remove the hitbox on contact with floor / player

        Destroy(gameObject, .2f);
        // To avoid box falling into void
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

    }


}
