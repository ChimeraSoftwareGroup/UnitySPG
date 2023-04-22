using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrinaController : MonoBehaviour
{
    public int force = 200;
    [SerializeField] Rigidbody rb;

    [Header("Tuto")]
    private bool _isStarting = false;
    private bool _isText1 = true;
    private bool _isText2 = false;
    [SerializeField] GameObject _textTuto1;
    [SerializeField] GameObject _textTuto2;
    [SerializeField] GameObject _canvasTuto;

    private void Awake()
    {
        _isStarting= true; 
        _canvasTuto.SetActive(false);
    }

    private void Start()
    {
        Invoke("StopTuto", 6f);
       
    }
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * force);
        }
        if (_isStarting)
        {
            _canvasTuto.SetActive(true);
            StartCoroutine(TutoTextFroggy());
        }
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            print("OBSTACLE");
        }
    }
}
