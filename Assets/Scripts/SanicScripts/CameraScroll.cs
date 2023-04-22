using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private int _scrollSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Récupérez la position actuelle de la caméra
        Vector3 pos = transform.position;

        // Déplacez la caméra vers le bas avec une vitesse de défilement
        pos.y -= _scrollSpeed * Time.deltaTime;

        // Définissez la nouvelle position de la caméra
        transform.position = pos;
    }
}
