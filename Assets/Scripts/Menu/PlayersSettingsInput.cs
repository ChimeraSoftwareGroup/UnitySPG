using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayersSettingsInput : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] DialogManager _dialogManager;
    [SerializeField] GameObject _listOfPlayersCanvas;
    [SerializeField] GameObject _numberOfPlayerCanvas;
    [SerializeField] GameObject _secondsByGamesCanvas;
    [SerializeField] Button _buttonAddPlayer;
    [SerializeField] LayoutGroup _listPlayerLayoutGroup;
    [SerializeField] InputField _playerNameIF;
    [SerializeField] PlayerName _playerNameInUI;

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
        if (instance == null)
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
        _listOfPlayersCanvas.gameObject.SetActive(false);
        _numberOfPlayerCanvas.gameObject.SetActive(true);
    }
    
    // GAMES SET UPPING
    public void ReadingNumberOfGames(string _numberOfGames)
    {
        numberOfGames = _numberOfGames;
    }
    public void AddNbMiniGameToGM()
    {
        _numberOfPlayerCanvas.gameObject.SetActive(false);
        _secondsByGamesCanvas.gameObject.SetActive(true);
        if (numberOfGames == "") numberOfGames = "3";
    }
    // TIME OF GAMES SET UPPING
    public void ReadingSecondsPerGames(string _secondsPerGames)
    {
        secondsPerGames = _secondsPerGames;
    }
    public void AddSecondsByGameToGMAndStartCoopGame()
    {
        PlayerHealth.instance.SetHP(3);

        if(numberOfGames == "")
        {
            numberOfGames = "3";
        }
        int numberOfMiniGamesSelected = int.Parse(numberOfGames);

        if (secondsPerGames == "")
        {
            secondsPerGames = "20";
        }
        float timeSelectedinSeconds = float.Parse(secondsPerGames);
        _gameManager.setParametersOfCoopGame(
            nameOfPlayersList,
            true, // Is Shuffle On
            timeSelectedinSeconds, // Timer Choosed
            numberOfMiniGamesSelected  // Number of Games
            ); // EXEMPLE
        _dialogManager.StartTutorialDialog();
        _secondsByGamesCanvas.SetActive(false);
        _gameManager.GameObjectsActivationAtStartEatchGame();
        _gameManager.NewGame();
    }
}
