using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // GAME MANAGER PATATE CHAUDE
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerDead = false;
    [SerializeField] public bool isPlayerHasWin;

    [Header("Game Parameters Set up by player")]
    [SerializeField] private string[] _potatoesPlayers;
    [SerializeField] public float timeOfEachGameChosenByPlayers;
    [SerializeField] public int numberOfGames;
    //[SerializeField] private int _gamesSelected;

    [Header("Canvas")]
    [SerializeField] private GameObject _startGameCanvas;
    [SerializeField] private GameObject _screenDeath;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _settingsByPlayerCanvas;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Countdown _countdown;
    [SerializeField] private GameObject _nextPlayerUI;
    [SerializeField] private Text _nextPlayerToPlayText;

    [Header("Ingame Managers")]
    [SerializeField] private SpawnerManager _spawnerManager;
    // [SerializeField] Timer _gameTimer;

    [Header("Player Conf for Tutos")]
    [SerializeField] private bool _doWeShowTutorial = true;

    [Header("Sounds and musics")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip LostMiniGameSound;
    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip winSound;

    public bool isMiniGameFinished;
    private bool _thePartyIsFinished = false;
    private bool _isNewSceneReadyToPlay;
    private int sceneIndex = 2; // 2 is the index of landing page (Potato)
    private int sceneActiveID;
    private List<int> sceneIndexes = new List<int>();

    [Header ("Coop Game - Manage Player List and Current Player")]
    private List<string> _playerNameList = new List<string>();
    private string _currentPlayerName;
    private int _idOfCurrentPlayerPlaying = 0;

    public static GameManager instance;
    private int[] _allScenesIndex;
    public bool isCamActiveFroggyScene = false;

    private int _miniGameFinished = 0;
    private int _hpPlayer = 3;
    private bool _gameHasStarted = false;



    //private Scene scene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        //for (int i = 3; i < SceneManager.sceneCountInBuildSettings; i++) {
        //    _allGamingScenesIndex[i] = i ;
        //}

        //Menus Scenes
        /*
        sceneIndexes.Add(0); // Menue
        sceneIndexes.Add(1); // Potato
        sceneIndexes.Add(2); // Landing
        sceneIndexes.Add(3); // Credits
        sceneIndexes.Add(4); // Ending
        */ // TO REMOVE

        // Gaming Scenes
        sceneIndexes.Add(5); // Sneuk
        sceneIndexes.Add(6); // Froggy
        sceneIndexes.Add(7); // Bobee
        //sceneIndexes.Add(8); // Giraffe
        //sceneIndexes.Add(7);
        //sceneIndexes.Add(8);
        //sceneIndexes.Add(9); // FUTUR GAMES.
        //sceneIndexes.Add(10); 
    }

  
    private void Start()
    {
        //scene = SceneManager.GetActiveScene();
        FlutterUnityIntegration.NativeAPI.OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        initGameManager();

        
    }

    private void Update()
    {
        if(isMiniGameFinished)
        {
            print("Je win !!!");
            WinMiniGame();
        }

        if (Input.GetKeyDown(KeyCode.H) && _gameHasStarted)
        {
            _player.PlayerTakeDamage();
        }


        if (Input.GetKeyDown(KeyCode.J) && _gameHasStarted)
        {
            WinMiniGame();
        }

        if (
            _countdown.isCountdownFinish
            && !_thePartyIsFinished
            && !_gameHasStarted
        ) {
            _player.gameObject.SetActive(true);
            _spawnerManager.ActivateSpawners();
            _timer.gameObject.SetActive(true);
            _timer.GetComponent<Timer>().StartTimer();
            _gameHasStarted = true;
        }
    }
    private void ShowNameForCurrentPlayer()
    {
       Debug.Log("_idOfCurrentPlayerPlaying " + _idOfCurrentPlayerPlaying);
       Debug.Log("id 0  " + _playerNameList[0]);
       Debug.Log("id 1  " + _playerNameList[1]);
       Debug.Log("id 2  " + _playerNameList[2]);
        if (_playerNameList.Count == _idOfCurrentPlayerPlaying) _idOfCurrentPlayerPlaying = 0;  // EW AUSSI (METHOD A) (should be good to test with prints);
        // print("_idOfCurrentPlayerPlaying " + _playerNameList[_idOfCurrentPlayerPlaying]);
        _nextPlayerToPlayText.text = _playerNameList[_idOfCurrentPlayerPlaying];
        _idOfCurrentPlayerPlaying++;

        //if (_currentPlayerName == "")
        //{
        //    _currentPlayerName = _playerNameList[0];
        //} 
        //_playerNameList.Remove(_currentPlayerName);                                          // METHOD B
        //string tempName = _currentPlayerName;
        //_currentPlayerName = _playerNameList[0]; // EW.
        //_playerNameList.Add(tempName);

        //if(ShufflePlayerOn)
        //{
        //    string nextPlayerName = _playerNameList[Random.Range(0, _playerNameList.Count)];
        //    if (_currentPlayerName == nextPlayerName) ShowNameForCurrentPlayer();            // METHOD SHUFFLE A
        //    _nextPlayerToPlayText.text = nextPlayerName;
        //}
    }
    
    private void initGameManager()
    {
        
        if(numberOfGames > 3) numberOfGames = 3;
        if(timeOfEachGameChosenByPlayers <= 0) timeOfEachGameChosenByPlayers = 20;
    }

    public void InputSettingsByPlayer()
    {
        //_gameCanvas.gameObject.SetActive(false);
        _settingsByPlayerCanvas.gameObject.SetActive(true);
    }

    [System.Obsolete]
    public void NewGame()
    {
        _gameHasStarted = false;
        _isNewSceneReadyToPlay = false;
        PrepareNextGameAndResetTimer();
        if(_spawnerManager) _spawnerManager.gameObject.SetActive(false);
        _screenDeath.SetActive(false);
        _countdown.StartCountDown();
        _timer.GetComponent<Timer>().SetTimer(timeOfEachGameChosenByPlayers);
        isMiniGameFinished = false;
        //isCamActiveFroggyScene = true; // C'est quoi ça là
        //   SetUpNewGameCanvas();
    }

    private void PrepareNextGameAndResetTimer()
    {
        // RemoveSceneOfTheList(); // Maybe not remove the game previously done for Potato
        ShuffleGame();
        SceneManager.LoadScene(sceneIndex);
        GameObjectsActivationAtStartEatchGame();
        ShowNameForCurrentPlayer();
        // Ne pas l'appeler si les utilisateurs ne veulent pas des tutos.
        if (_doWeShowTutorial) DialogManager.instance.StartTutorialDialog();
    }

    /** 
    * Gestion des scenes
    */

    private void RemoveSceneOfTheList()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneActiveID = currentScene.buildIndex;
        sceneIndexes.Remove(sceneActiveID);
    }

    private void ShuffleGame()
    {
        int randomIndex = Random.Range(0, sceneIndexes.Count);
        sceneIndex = sceneIndexes[randomIndex];
    }

    public void GameObjectsActivationAtStartEatchGame()
    {
        _startGameCanvas.SetActive(true);
        _timer.SetActive(true);
        _healthBar.SetActive(true);
    }

    public void DisablePlayerAfterDamage()
    {
        _player.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(4);
        audioSource.PlayOneShot(looseSound);

        _thePartyIsFinished = true;
        isPlayerHasWin = false;
        StopGame();
    }

    private void StopGame()
    {
        _startGameCanvas.SetActive(false);
        _healthBar.SetActive(false);
        _timer.SetActive(false);
    }

    [System.Obsolete]
    public void WinPotato()
    {
        print("WIN POTATO FUNCTION");
        print(PlayerHealth.instance.currentHealth);
        if (PlayerHealth.instance.currentHealth == 0)
        {
            isPlayerHasWin = false;

            print(PlayerHealth.instance.currentHealth);
            GameOver();
        }
        else
        {
            _thePartyIsFinished = true;
            isPlayerHasWin = true;
            SceneManager.LoadScene(4);
            audioSource.PlayOneShot(winSound);
            StopGame();
        }

    }
    /** 
   * Methods used in Flutter
   */
    public void LoadScene(string sceneID)
    {
        int sceneLevel;
        int.TryParse(sceneID, out sceneLevel);
        SceneManager.LoadScene(sceneLevel);
    }

    /**
     * Set Active Player is only called by the Player Himself in the scene
     * it tells the game manager that the player in the scene is loaded
     */
    public void NotificationPlayerAndSceneHasChanged(Player newSceneNewPlayer)
    {
        _isNewSceneReadyToPlay = true;
        
        // Ici on peut commencer à créer des fonctions
        // qui vont mettre en place la nouvelle scene.
        // Paramétrer les SpawnerManager
        // Dialog Manager
        // En sommes tous les managers du game en cours.
        // Stocker la data initiale des parties dans un manager de partie possiblement.
        // A partir d'ici notifier les manager pour qu'ils se mettent à l'état d'orgine de la partie.
        // permettant un game flow pour relancer proprement les parties.
        UpdatePlayerGameObject(newSceneNewPlayer);
        UpdateSpawnerManager();
        UpdateDialogManager();
        // ici mettre les informations relatives aux dialogs
    }

    private void UpdateSpawnerManager()
    {
        _spawnerManager.gameObject.SetActive(true);
    }

    private void UpdateDialogManager()
    {
        DialogManager.instance.StartTutorialDialog();
    }

    private void UpdatePlayerGameObject(Player newActivePlayerFromScene)
    {
        _player = newActivePlayerFromScene;
        _timer.GetComponent<Timer>().StopTimer();
        _timer.gameObject.SetActive(false);
        _player.gameObject.SetActive(false);
    }

    public void WinMiniGame()
    {
        isCamActiveFroggyScene = false;
           _miniGameFinished++;
        _spawnerManager.DeactivateSpawners();
        if (_miniGameFinished == numberOfGames)
        {
            WinPotato();
        }
        else
        {
            _countdown.isCountdownFinish = false;
            _timer.GetComponent<Timer>().StopTimer();
            _timer.gameObject.SetActive(true);
            if(_player != null)_player.gameObject.SetActive(false);
            NewGame();
        }

    }

    public void LoseMiniGame()
    {
        _miniGameFinished++;
        isCamActiveFroggyScene = false;

        _spawnerManager.DeactivateSpawners();
        _hpPlayer--;
        print("Current health " + _hpPlayer);
        if (_hpPlayer <= 0)
        {
            isPlayerHasWin = false;
            print(_hpPlayer);
            GameOver();
        }
        else if (_miniGameFinished == numberOfGames)
        {
            WinPotato();
        }
        else
        {
            audioSource.PlayOneShot(LostMiniGameSound);
            _screenDeath.SetActive(true);
            _countdown.isCountdownFinish = false;
            _player.gameObject.SetActive(false);
            _isPlayerDead = true;
            Invoke("NewGame", 3f);
        }
    }

    public void setParametersOfCoopGame(
        List<string> playerList,
        bool isTutoOn,
        float timerChoosed,
        int numberOfMiniGamesChoosed
    )
    {
        _playerNameList = playerList;
        timeOfEachGameChosenByPlayers = timerChoosed;
        numberOfGames = numberOfMiniGamesChoosed;
    }

}
