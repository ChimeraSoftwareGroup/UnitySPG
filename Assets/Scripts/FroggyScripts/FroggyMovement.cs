using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggyMovement : MonoBehaviour { 

    [Header ("Speed Movement")]
    [SerializeField] float currentSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] Animator animator;
    [Header ("Direction")]
    [SerializeField] bool isGoingRight;
    public AudioSource audioSource;
    private bool _isAccelerating;
    private bool _isMoving;

    private void Awake()
    {
        currentSpeed = minSpeed;
        _isAccelerating = true;
        _isMoving = true;
    }

    private void OnEnable()
    {
        StartCoroutine(AccelerationCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(AccelerationCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) _isMoving = false;
    }
    void Update()
    {
        if(_isMoving) MovePlayer();
    }
    void MovePlayer()
    {
        transform.Translate(Vector3.right * currentSpeed * Time.smoothDeltaTime);
    }

   

    private IEnumerator AccelerationCoroutine()
    {
        while (_isAccelerating)
        {
            yield return new WaitForSeconds(1f);
            if (currentSpeed < maxMoveSpeed)
            {
                currentSpeed += 1;
            }
            
            if (currentSpeed < maxMoveSpeed)
            {
                yield return new WaitForSeconds(1f);
                currentSpeed += 1;
            }
        }
    }
}