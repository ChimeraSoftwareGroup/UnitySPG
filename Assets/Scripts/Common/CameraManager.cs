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
    }
}
