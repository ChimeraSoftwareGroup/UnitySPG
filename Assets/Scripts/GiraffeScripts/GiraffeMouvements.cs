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

    [Header("Player")]
    [SerializeField] GameObject _girafe;
    [SerializeField] GameObject _girafeCanvas;
    private bool _isCollided = false;


    private void Awake()
    {
        currentSpeed = minSpeed;
    }

    private void OnEnable()
    {
     //   StartCoroutine(AccelerationCoroutine());
    }
    private void OnDisable()
    {
     //   StopCoroutine(AccelerationCoroutine());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_isCollided)
        {
            if (collision.gameObject.tag == "Wall")
            {
                _isCollided = true;
                gameObject.SetActive(false);
            }
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

        if (isGoingUp)
        {
            _girafeCanvas.SetActive(true);
        }
      
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

    public void GoingLeft()
    {
        isGoingUp = false;
        isGoingRight = false;
        isGoingLeft = true;
    }
    public void GoingUp()
    {
        isGoingUp = true;
        isGoingRight = false;
        isGoingLeft = false;
    }
    public void GoingRight()
    {
        isGoingUp = false;
        isGoingRight = true;
        isGoingLeft = false;
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

    //private IEnumerator AccelerationCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        if (currentSpeed < maxMoveSpeed)
    //        {
    //            currentSpeed += 1;
    //        }

    //        if (currentSpeed < maxMoveSpeed)
    //        {
    //            yield return new WaitForSeconds(1f);
    //            currentSpeed += 1;
    //        }
    //    }

    //}

}