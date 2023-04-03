using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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
    [SerializeField] GameObject _errorCanvasPlayerList;
    [SerializeField] GameObject _errorCanvasNumberOfGame;
    [SerializeField] GameObject _errorCanvasSeconds;
    [SerializeField] Button _buttonAddPlayer;
    [SerializeField] LayoutGroup _listPlayerLayoutGroup;
    [SerializeField] InputField _playerNameIF;
    [SerializeField] PlayerName _playerNameInUI;


    [Header("Settings")]
    public string playerName;
    public string numberOfGames;
    public string secondsPerGames;

    public static PlayersSettingsInput instance;

    [SerializeField] GameObject playerNameInput;
    [SerializeField] GameObject numberOfPlayerInput;
    [SerializeField] GameObject secondsByGamesInput;

    List<string> nameOfPlayersList = new List<string>();
    private int _countPlayer = 0;


    private void Awake() 
    {
        if (instance == null) // Singleton : pour pouvoir appeler l'instance de ce script n'importe où
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
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
            _errorCanvasPlayerList.SetActive(true);
            return;
        }
        _listOfPlayersCanvas.gameObject.SetActive(false);
        _numberOfPlayerCanvas.gameObject.SetActive(true);
    }
    public void CloseErrorPage()
    {
        _errorCanvasPlayerList.SetActive(false);
    }

    // GAMES SET UPPING
    public void ReadingNumberOfGames(string _numberOfGames)
    {
        numberOfGames = _numberOfGames;
    }
    public void AddNbMiniGameToGM()
    {
        print("Je passe ici");
        if(int.Parse(numberOfGames) < nameOfPlayersList.Count)
        {
            _errorCanvasNumberOfGame.SetActive(true);
            return;
        }
        else if(numberOfGames == "")
        {
            print("et là aussi");

            _errorCanvasNumberOfGame.SetActive(true);
            return;
        }
        print(numberOfGames);
        _numberOfPlayerCanvas.gameObject.SetActive(false);
        _secondsByGamesCanvas.gameObject.SetActive(true);
        
       
    }
    public void CloseErrorGamesPage()
    {
        _errorCanvasNumberOfGame.SetActive(false);
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
    }
    public void AddSecondsByGameToGMAndStartCoopGame()
    {
        PlayerHealth.instance.SetHP(3);
        int numberOfMiniGamesSelected = int.Parse(numberOfGames);

        if (secondsPerGames == "")
        {
            secondsPerGames = "20";
        }

        if(int.Parse(secondsPerGames) < 10)
        {
            _errorCanvasSeconds.SetActive(true);
            return;
        }
        float timeSelectedinSeconds = float.Parse(secondsPerGames);
        _gameManager.setParametersOfCoopGame(
            nameOfPlayersList,
            true, // Is Shuffle On
            timeSelectedinSeconds, // Timer Choosed
            numberOfMiniGamesSelected  // Number of Games
            ); 
        _dialogManager.StartTutorialDialog();
        _secondsByGamesCanvas.SetActive(false);
        _gameManager.GameObjectsActivationAtStartEatchGame();
        _gameManager.NewGame();
    }
}
