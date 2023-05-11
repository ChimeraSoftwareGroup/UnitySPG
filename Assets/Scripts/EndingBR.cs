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
        Debug.Log("score : " + score.user_position);
        _endingText.text = score.user_position.ToString();
    }

    void Update()
    {
        
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        _networkManager.SocketDisconnect();
        print("BACK TO THE MAIN MENU");

        //Quit room
    }
}
