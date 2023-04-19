using UnityEngine;
using System.Collections;

public class SanicController : MonoBehaviour
{
    public float speed = 50f; // vitesse de déplacement de la sphère
    public float tilt = 2f; // facteur d'inclinaison du téléphone

    private Rigidbody rb; // référence au Rigidbody de la sphère

    void Start()
    {
      

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.acceleration.x; // récupère l'inclinaison horizontale
        float moveVertical = Input.acceleration.y; // récupère l'inclinaison verticale

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed); // applique la force pour faire rouler la sphère

        // Inclinaison de la sphère
        Vector3 tiltVector = new Vector3(moveVertical * tilt, 0f, -moveHorizontal * tilt);
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, tiltVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * transform.rotation, Time.deltaTime * 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            print("OBSTACLE !!!!!");
        }

    }
}





