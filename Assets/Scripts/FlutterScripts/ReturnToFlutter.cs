using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FlutterUnityIntegration;

public class ReturnToFlutter : MonoBehaviour
{
    private UnityMessageManager message;
    void Start()
    {
        message = GetComponent<UnityMessageManager>();
    }

    public void OnClicQuitButton()
    {
        message.SendMessageToFlutter("closeUnity");
    }
}
