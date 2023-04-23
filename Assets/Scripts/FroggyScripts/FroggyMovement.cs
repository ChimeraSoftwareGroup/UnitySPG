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

    [Header("Tuto")]
    private bool _isStarting = false;
    private bool _isText1 = true;
    private bool _isText2 = false;
    [SerializeField] GameObject _textTuto1;
    [SerializeField] GameObject _textTuto2;
    [SerializeField] GameObject _canvasTuto;
 
    private void Awake()
    {
        currentSpeed = minSpeed;
        _isAccelerating = true;
        _isMoving = true;
        _isStarting= true;
    }
    private void Start()
    {
        Invoke("StopTuto", 6f);

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
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            _isMoving = false;
        }

    }
    void Update()
    {
        if(_isMoving) MovePlayer();
    }
    private void FixedUpdate()
    {
        if (_isStarting)
        {
            _canvasTuto.SetActive(true);
            StartCoroutine(TutoTextFroggy());
        }
    }
    void MovePlayer()
    {
        transform.Translate(Vector3.right * currentSpeed * Time.smoothDeltaTime);
    }

    IEnumerator TutoTextFroggy()
    {
        _textTuto1.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _textTuto1.SetActive(false);
        _textTuto2.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _textTuto2.SetActive(false);
        _textTuto1.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _textTuto1.SetActive(false); 
        _textTuto2.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _textTuto2.SetActive(false);
        _textTuto1.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        StopTuto();
        yield break;
    }

    private void StopTuto()
    {
        _isStarting = false;
        _canvasTuto.SetActive(false);

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