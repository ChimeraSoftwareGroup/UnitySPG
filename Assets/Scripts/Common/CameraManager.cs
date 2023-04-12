using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Update()
    {
        if (!mainCamera.gameObject.activeSelf)
        {
            secondaryCamera.gameObject.SetActive(true);
        }
        else
        {
            secondaryCamera.gameObject.SetActive(false);
        }
    }
}
