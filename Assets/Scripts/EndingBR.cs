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
    [SerializeField] GameObject _littleFireworks;
    [SerializeField] AudioClip _winGingle;
    [SerializeField] GameObject _bobee;
    [SerializeField] GameObject _firstThrophee;
    [SerializeField] GameObject _lightFirst;


    [Header("Second")]
    [SerializeField] GameObject _secondPanel;
    [SerializeField] GameObject _lightParticuleNd;
    [SerializeField] GameObject _medailNd;


    [Header("Third")]
    [SerializeField] GameObject _thirdPanel;
    [SerializeField] GameObject _medail;
    [SerializeField] GameObject _gerard;


    [Header("Loser")]
    [SerializeField] AudioClip _loseGingle;
    [SerializeField] GameObject _loseParticules;

    [Header("All")]
    [SerializeField] Text _endingText;
    [SerializeField] Text _emeText;
    [SerializeField] GameObject _bg;
 
    [SerializeField] GameObject _audioManager;

    public AudioSource audioSource;
    private NetworkManager _networkManager;

  


    void Start()
    {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        //EndingScoreResponse score = _networkManager.GetFinalScore();
        // _endingText.text = score.user_position.ToString();

        _endingText.text = "4";

        if (_endingText.text == "1")
        {
            First();
            _emeText.text = "er";
            
        }
        else
        {
            _emeText.text = "ème";
        }

        if(_endingText.text == "2")
        {
            Second();

        }
        else if (_endingText.text == "3")
        {
            Third();

        }
        else if (_endingText.text == "4")
        {
            Loser();

        }
    }

    public void First()
    {
        audioSource.PlayOneShot(_winGingle, 0.2f);
        _littleFireworks.SetActive(true);
        _panel.SetActive(true);
        _bobee.SetActive(true);
        Invoke("ActiveParticuleFirst", 1f);
        Invoke("FirstThrophee", 1.5f);
      
    }

    private void ActiveParticuleFirst()
    {
        _firstParticules.SetActive(true);

    }

    public void FirstThrophee()
    {
        _lightFirst.SetActive(true);
        _firstThrophee.SetActive(true);
    }

    public void Second()
    {
        audioSource.PlayOneShot(_winGingle, 0.2f);
        _secondPanel.SetActive(true);
        _littleFireworks.SetActive(true);
        audioSource.PlayOneShot(_winGingle, 0.2f);
        Invoke("MedailNd", 1.5f);


    }

    private void MedailNd()
    {
        _medailNd.SetActive(true);
        _lightParticuleNd.SetActive(true);
    }

    public void Third()
    {
        _thirdPanel.SetActive(true);
        _littleFireworks.SetActive(true);
        Invoke("MedailAnim", 1.5f);
        audioSource.PlayOneShot(_winGingle, 0.2f);


    }

    private void MedailAnim()
    {
        _medail.SetActive(true);
        _gerard.SetActive(true);

    }

    public void Loser()
    {
        audioSource.PlayOneShot(_loseGingle, 0.2f);
        _loseParticules.SetActive(true);
        _bg.GetComponent<SpriteRenderer>().color = new Color32(114, 114, 114, 225);

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
