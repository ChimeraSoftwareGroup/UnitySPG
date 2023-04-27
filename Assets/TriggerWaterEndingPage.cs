using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWaterEndingPage : MonoBehaviour
{

    [SerializeField] GameObject _splash;
    void Start()
    {
        Invoke("ActiveAnimSplash", 2f);
    }

    void Update()
    {
        
    }

    private void ActiveAnimSplash()
    {
        
            _splash.SetActive(true);

        
    }
}
