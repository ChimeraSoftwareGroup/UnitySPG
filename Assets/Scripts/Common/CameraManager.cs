using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera _camPlayer;

    public Camera[] cameras;
    private int currentCameraIndex;
    private void Start()
    {
        currentCameraIndex = 0;
    }
    void Update()
    {
        if(_camPlayer == null)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);

        }



        if (GameManager.instance.isCamActiveFroggyScene == false)
        {
            // Désactive la caméra actuelle
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Passe à la caméra suivante dans la liste
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Active la nouvelle caméra
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }



    }
}
