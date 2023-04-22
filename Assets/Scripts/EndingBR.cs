using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndingBR : MonoBehaviour
{
    void Start()
    {
        Invoke("ReturnMainMenu", 7f);
    }

    void Update()
    {
        
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        print("BACK TO THE MAIN MENU");
    }
}
