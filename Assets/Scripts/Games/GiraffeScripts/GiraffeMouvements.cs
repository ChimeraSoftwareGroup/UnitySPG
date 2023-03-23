using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeMouvements : MonoBehaviour
{

    [Header("Speed Movement")]
    [SerializeField] float currentSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] Animator animator;
    
    [Header("Direction")]
    [SerializeField] bool isGoingUp;
    [SerializeField] bool isGoingLeft;
    [SerializeField] bool isGoingRight;

    [Header("Sound")]
    public AudioSource audioSource;




    private void Awake()
    {
        currentSpeed = minSpeed;
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
        // collision with a wall : just go back
        if (other.gameObject.CompareTag("wall"))
        {

        }

        if (other.gameObject.CompareTag("obstacle"))
        {

        }
    }
    private void Start()
    {
        isGoingUp = true;
    }
    void Update()
    {
        VerifInput();
        MovePlayer();
    }
    void VerifInput()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            if(isGoingUp)
            {
                isGoingUp = false;
                isGoingLeft = true;
            } else if (isGoingRight)
            {
                isGoingRight = false;
                isGoingUp = true;
            }
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (isGoingUp)
            {
                isGoingUp = false;
                isGoingRight = true;
            }
            else if (isGoingLeft)
            {
                isGoingLeft = false;
                isGoingUp = true;
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isGoingLeft)
            {
                isGoingUp = true;
                isGoingLeft = false;
            }
            else if (isGoingRight)
            {
                isGoingUp = true;
                isGoingRight = false;
            }
        }

    }
    void MovePlayer()
    {
       if(isGoingLeft)
       {
            transform.Translate(-Vector3.right * currentSpeed * Time.smoothDeltaTime);

       }
        if (isGoingRight)
        {
            transform.Translate(Vector3.right * currentSpeed * Time.smoothDeltaTime);

        }
        if (isGoingUp)
        {
            transform.Translate(Vector3.up * currentSpeed * Time.smoothDeltaTime);

        }

    }

    void FlipPlayer()
    {
        GetComponentInChildren<SpriteRenderer>().flipX = !GetComponentInChildren<SpriteRenderer>().flipX;
    }

    private IEnumerator AccelerationCoroutine()
    {
        while (true)
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