using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalleineController : MonoBehaviour
{

    public float speed = 50f; // vitesse de déplacement de la sphère
    public float tilt = 2f; // facteur d'inclinaison du téléphone

    private Rigidbody rb; // référence au Rigidbody de la sphère

    [SerializeField] GameObject _tutoCanvas;
    private bool _isTutoStart = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _isTutoStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if (_isTutoStart)
        {
            _tutoCanvas.SetActive(true);
            Invoke("StopTutoCanvas", 3f);
            _isTutoStart = false;
        }


        float moveHorizontal = Input.acceleration.x; // récupère l'inclinaison horizontale
        float moveVertical = Input.acceleration.y; // récupère l'inclinaison verticale

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed); // applique la force pour faire rouler la sphère

        // Inclinaison de la sphère
        Vector3 tiltVector = new Vector3(moveVertical * tilt, 0f, -moveHorizontal * tilt);
        Quaternion targetRotation = Quaternion.FromToRotation(transform.right, tiltVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * transform.rotation, Time.deltaTime * 10);
    }

    private void StopTutoCanvas()
    {
        _tutoCanvas.SetActive(false);

    }
}
