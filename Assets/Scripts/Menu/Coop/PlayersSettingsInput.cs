using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class PlayersSettingsInput : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] GameManager _gameManager;
    [SerializeField] DialogManager _dialogManager;

    [Header("Canvas")]
    [SerializeField] GameObject _listOfPlayersCanvas;
    [SerializeField] GameObject _numberOfPlayerCanvas;
    [SerializeField] GameObject _secondsByGamesCanvas;

    [SerializeField] Button _buttonAddPlayer;
    [SerializeField] LayoutGroup _listPlayerLayoutGroup;
    [SerializeField] InputField _playerNameIF;
    [SerializeField] PlayerName _playerNameInUI;
    [SerializeField] GameObject _buttonCloseModeCoop;
    [SerializeField] GameObject _playerList;
    [SerializeField] GameObject _allSettings;
    [SerializeField] GameObject _closeButton;

    [Header("Errors")]
    [SerializeField] GameObject _errorCanvasPlayerList;
    [SerializeField] GameObject _errorCanvasNumberOfGame;
    [SerializeField] GameObject _errorCanvasSeconds;

    [Header("Settings")]
    public string playerName;
    public string numberOfGames;
    public string secondsPerGames;

    public static PlayersSettingsInput instance;

    [SerializeField] GameObject playerNameInput;
    [SerializeField] GameObject numberOfPlayerInput;
    [SerializeField] GameObject secondsByGamesInput;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip errorSound;


    List<string> nameOfPlayersList = new List<string>();
    private int _countPlayer = 0;
    private Vector3 lastValidPosition;

    private void Awake() 
    {
        if (instance == null) // Singleton : pour pouvoir appeler l'instance de ce script n'importe où
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

   
    private void Update()
    {
        lastValidPosition = transform.position;

    }

    // Players Set Upping
    public void ReadNameOfPlayers(string name)
    {
        playerName = name;
        if (name == null) print("NO NAME == NULL");
    }

    public void AddPlayerAtList()
    {
        nameOfPlayersList.Add(playerName);
        AddPlayerInListPlayerUI();
        _countPlayer++;
        _playerNameIF.text = "";
        audioSource.PlayOneShot(sound);

    }

    private void AddPlayerInListPlayerUI()
    {
        PlayerName playerNameToAddToUI = Instantiate(_playerNameInUI, _listPlayerLayoutGroup.transform.position, Quaternion.identity);
        playerNameToAddToUI.gameObject.GetComponent<Text>().text = nameOfPlayersList[nameOfPlayersList.Count - 1];
        playerNameToAddToUI.gameObject.transform.parent = _listPlayerLayoutGroup.gameObject.transform;
    }

    public void FinishPlayerListAndStartSetUpGamesNumber()
    {
        if(nameOfPlayersList.Count == 0)
        {
            audioSource.PlayOneShot(errorSound);

            _errorCanvasPlayerList.SetActive(true);
            return;
        }
        _playerList.SetActive(false);


        Invoke("HideInputPlayerList", 3f);
        _numberOfPlayerCanvas.gameObject.SetActive(true);
    }

    public void HideInputPlayerList()
    {
        _listOfPlayersCanvas.SetActive(false);

    }
    public void CloseErrorPage()
    {
        _errorCanvasPlayerList.SetActive(false);
        audioSource.PlayOneShot(sound);

    }

    // GAMES SET UPPING
    public void ReadingNumberOfGames(string _numberOfGames)
    {

        numberOfGames = _numberOfGames;
        print("numberOfGames " + numberOfGames);

    }
    public void AddNbMiniGameToGM()
    {
        print("Add nb mini game");

        if (numberOfGames == "" || numberOfGames == " ")
            {
            audioSource.PlayOneShot(errorSound);

            _errorCanvasNumberOfGame.SetActive(true);
            return;
        }
        else if (int.Parse(numberOfGames) < nameOfPlayersList.Count)
        {
            audioSource.PlayOneShot(errorSound);

            _errorCanvasNumberOfGame.SetActive(true);
            return;
        }
        else
        {
            print(numberOfGames);
            audioSource.PlayOneShot(sound);

            Invoke("CloseNbGames", 3f);
            _secondsByGamesCanvas.gameObject.SetActive(true);
        }

       
        
       
    }

    private void CloseNbGames()
    {
        
        _numberOfPlayerCanvas.gameObject.SetActive(false);

    }
    public void CloseErrorGamesPage()
    {
        _errorCanvasNumberOfGame.SetActive(false);
        audioSource.PlayOneShot(sound);

    }
    private int intParse(string numberOfGames)
    {
        throw new NotImplementedException();
    }

    // TIME OF GAMES SET UPPING
    public void ReadingSecondsPerGames(string _secondsPerGames)
    {
        secondsPerGames = _secondsPerGames;
    }

    public void CloseErrorSecondsPage()
    {
        _errorCanvasSeconds.SetActive(false);
        audioSource.PlayOneShot(sound);

    }


    public void AddSecondsByGameToGMAndStartCoopGame()
    {
        bool _allGood = true;
            int numberOfMiniGamesSelected = int.Parse(numberOfGames);
        float timeSelectedinSeconds = float.Parse(secondsPerGames);


        if (secondsPerGames == "" || secondsPerGames == " " || timeSelectedinSeconds < 20)
            {
            audioSource.PlayOneShot(errorSound);

            _errorCanvasSeconds.SetActive(true);
            return;
        }
        _allSettings.SetActive(false);
        _closeButton.SetActive(false);
        _buttonCloseModeCoop.SetActive(false);
        _secondsByGamesCanvas.SetActive(false);
        PlayerHealth.instance.SetHP(3);

        _gameManager.setParametersOfCoopGame(
                nameOfPlayersList,
                true, // Is Shuffle On
                timeSelectedinSeconds, // Timer Choosed
                numberOfMiniGamesSelected  // Number of Games
                );
            if (_allGood)
            {
                _dialogManager.StartTutorialDialog();
                _gameManager.GameObjectsActivationAtStartEatchGame();
                _gameManager.NewGame();
            }
        
       

    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MenueScene");
        audioSource.PlayOneShot(sound);

    }
}
