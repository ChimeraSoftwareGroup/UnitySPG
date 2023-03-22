using FlutterUnityIntegration;
using UnityEngine;


public class Credits : MonoBehaviour
{
    private UnityMessageManager message;
    private void LoadMainMenu()
    {
        message.SendMessageToFlutter("closeUnity");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }
}
