using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndingBR : MonoBehaviour
{
    [SerializeField] Text _endingText;

    private NetworkManager _networkManager;

    void Start()
    {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //Invoke("ReturnMainMenu", 7f);
        print("ENDING SCENE");

        EndingScoreResponse score = _networkManager.GetFinalScore();
       // _endingText.text = score.position.ToString();
    }

    void Update()
    {
        
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        print("BACK TO THE MAIN MENU");

        //Quit room
    }
}
