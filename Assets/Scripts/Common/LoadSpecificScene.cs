using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Player>())
        {
            if (SceneManager.GetActiveScene().name == "GiraffeScene") SceneManager.LoadScene("SneukScene");
           // if (SceneManager.GetActiveScene().name == "SneukScene") SceneManager.LoadScene("GiraffeScene");
           // if (SceneManager.GetActiveScene().name == "SneukScene") SceneManager.LoadScene("FroggyScene");
            if (SceneManager.GetActiveScene().name == "SneukScene") SceneManager.LoadScene("BobeeScene");

            //if(GameListFromFlutter.Length > 0)
            //{
            //    SceneManager.LoadScene(GameListFromFlutter[0]);
            //    GameListFromFlutter.pop();
            //} else
            //{
            //    SceneLoadScene("EndGameScene"); 
            //}

        }
    }
}
