using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndingBR : MonoBehaviour
{

    [Header("First")]
    [SerializeField] GameObject _firstParticules;
    [SerializeField] GameObject _panel;
    [SerializeField] AudioClip _winGingle; 

    [Header("Second")]

    [Header("Third")]

    [Header("Loser")]
    [SerializeField] AudioClip _loseGingle;

    [Header("All")]
    [SerializeField] Text _endingText;
    [SerializeField] Text _emeText;

    private NetworkManager _networkManager;

  


    void Start()
    {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //Invoke("ReturnMainMenu", 7f);
        print("ENDING SCENE");

        EndingScoreResponse score = _networkManager.GetFinalScore();
        Debug.Log("score : " + score.user_position);
        _endingText.text = score.user_position.ToString();

        if (score.user_position == 1)
        {
            _emeText.text = "er";
        }
        else
        {
            _emeText.text = "ème";
        }
    }

    public void First()
    {
        _panel.SetActive(true);
        
    }

    public void Second()
    {

    }

    public void Third()
    {

    }

    public void Loser()
    {

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
