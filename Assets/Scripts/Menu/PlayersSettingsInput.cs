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
    [SerializeField] GameObject _gameCanvas;

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
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayersSettingsInput dans la scène");
            return;
        }
        instance = this;
    }

    public void ReadNameOfPlayers(string name)
    {
        playerName = name;
        if (name == null) print("NO NAME == NULL");
    }
    public void ReadingNumberOfGames(string _numberOfGames)
    {
        numberOfGames = _numberOfGames;
        if (_numberOfGames == "") numberOfGames = "3";
        Debug.Log("numberOfGames " + numberOfGames);
        GameManager.instance.numberOfGames = int.Parse(numberOfGames);
    }

    public void ReadingSecondsPerGames(string _secondsPerGames)
    {
        secondsPerGames = _secondsPerGames;
        Debug.Log(secondsPerGames); 
        if (_secondsPerGames == "") secondsPerGames = "20";
        try{
            GameManager.instance.timeOfEachGameChosenByPlayers = float.Parse(secondsPerGames);
        }
        catch{
            GameManager.instance.timeOfEachGameChosenByPlayers = 20;
        }
    }

   
    public void GoChooseNumberOfGames()
    {
        _listOfPlayersCanvas.gameObject.SetActive(false);
        AddNbMiniGameToGM();
        GameManager.instance.ActiveGameCanvas();
        GameManager.instance.NewGame();

        // _numberOfPlayerCanvas.gameObject.SetActive(true);
    }

    public void AddNbMiniGameToGM()
    {
        print("AddNbMiniGameToGM");
        _secondsByGamesCanvas.gameObject.SetActive(true);
        _numberOfPlayerCanvas.gameObject.SetActive(false);

    }
    public void AddSecondsByGameToGM()
    {
        print("FinishSetUp");
        FinishSetUp();
        _secondsByGamesCanvas.SetActive(false);
    }

    public void AddPlayerAtList()
    {
        nameOfPlayersList.Add(playerName);
        AddPlayerInListPlayerUI();
        _countPlayer++;

        // We set back data to null
        _playerNameIF.text = "";
        // Ajouter le GameObject à la VerticalLayoutGroup
       // playerName.SetParent(_listPlayerLayoutGroup.transform, false);

        //foreach (string playerName in nameOfPlayersList)
        //{
        //    Debug.Log(playerName);
        //}
    }
    
    private void AddPlayerInListPlayerUI()
    {
        PlayerName playerNameToAddToUI = Instantiate(_playerNameInUI, _listPlayerLayoutGroup.transform.position, Quaternion.identity);
        playerNameToAddToUI.gameObject.GetComponent<Text>().text = nameOfPlayersList[nameOfPlayersList.Count - 1];
        playerNameToAddToUI.gameObject.transform.parent = _listPlayerLayoutGroup.gameObject.transform;
    }

    private void FinishSetUp()
    {
        _gameManager.setParametersOfCoopGame(
            nameOfPlayersList,
            true,
            5,
            5
            ); // EXEMPLE
        _gameCanvas.gameObject.SetActive(true);
        _dialogManager.StartTutorialDialog();
    }
}
