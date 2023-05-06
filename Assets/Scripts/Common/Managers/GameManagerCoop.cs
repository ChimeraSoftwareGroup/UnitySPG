using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerCoop : GameManager
{
    // GAME MANAGER MODE COOP

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerDead = false;

    [Header("Game Parameters Set up by player")]
    [SerializeField] private string[] _potatoesPlayers;
    [SerializeField] public float timeOfEachGameChosenByPlayers;
    [SerializeField] public int numberOfGames;

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

    [Header("Player Conf for Tutos")]
    [SerializeField] private bool _doWeShowTutorial = true;

    [Header("Sounds and musics")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip LostMiniGameSound;
    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip winSound;

    [Header ("Coop Game - Manage Player List and Current Player")]
    private List<string> _playerNameList = new List<string>();
    private string _currentPlayerName;
    private int _idOfCurrentPlayerPlaying = 0;

    private bool _gameHasStarted = false;
    private bool _thePartyIsFinished = false;
    private bool _isNewSceneReadyToPlay;

    private int sceneIndex = 2; // 2 is the index of landing page (Coop)
    private int sceneActiveID;
    private int[] _allScenesIndex;
    private int _miniGameFinished = 0;
    private int _hpPlayer = 3;

    private List<int> sceneIndexes = new List<int>();

    public override void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null) instance = this;

        #region ListeScene
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

        //Gaming Scenes
        sceneIndexes.Add(7); // Sneuk
        sceneIndexes.Add(8); // Froggy
        sceneIndexes.Add(9); // Bobee
        sceneIndexes.Add(10); // Giraffe
        sceneIndexes.Add(11); // Brina
        sceneIndexes.Add(12); // Sanic
        sceneIndexes.Add(13); // Falleine
        sceneIndexes.Add(14); // Ladybee


        //sceneIndexes.Add(7);
        //sceneIndexes.Add(8);
        //sceneIndexes.Add(9); // FUTUR GAMES.
    }

    #endregion

    private void Start()
    {
     //  initGameManager();
    }

    private void Update()
    {
        #region ForTestGames

        if (Input.GetKeyDown(KeyCode.H) && _gameHasStarted)
        {
            _player.PlayerTakeDamage();
        }


        if (Input.GetKeyDown(KeyCode.J) && _gameHasStarted)
        {
            WinMiniGame();
        }

        #endregion
        if (isMiniGameFinished)
        {
            isMiniGameFinished = false;
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
      
        if (_playerNameList.Count == _idOfCurrentPlayerPlaying) _idOfCurrentPlayerPlaying = 0;  // Boucle pour vérifier le nom du next player
        _nextPlayerToPlayText.text = _playerNameList[_idOfCurrentPlayerPlaying];
        _idOfCurrentPlayerPlaying++;

        #region ShowPlayerOtherMethodes

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

        #endregion
    }

    /** 
    * Lancer le mode coop - les initialisations
    */

    //private void initGameManager()
    //{
    //    if(numberOfGames > 3) numberOfGames = 3;
    //    if(timeOfEachGameChosenByPlayers <= 0) timeOfEachGameChosenByPlayers = 20;

       
    //}

    public void InputSettingsByPlayer()
    {
        _settingsByPlayerCanvas.gameObject.SetActive(true);
    }

    public override void setParametersOfCoopGame(
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

    /** 
    * Nouveau Mini jeu
    */

    public override void NewGame()
    {
        print("NEW GAME CALLED");

        if (!isPlayerHasWin) 
        {
            _gameHasStarted = false;
            _isNewSceneReadyToPlay = false;
            PrepareNextGameAndResetTimer();
            if (_spawnerManager) _spawnerManager.gameObject.SetActive(false);
            _screenDeath.SetActive(false);
            _countdown.StartCountDown();
            _timer.GetComponent<Timer>().SetTimer(timeOfEachGameChosenByPlayers);
        }
        else
        {
            _screenDeath.SetActive(false);
            return;
        }

    }

    private void PrepareNextGameAndResetTimer()
    {
        // RemoveSceneOfTheList(); // Maybe not remove the game previously done for Coop
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
    private void ShuffleGame()
    {
        int randomIndex = Random.Range(0, sceneIndexes.Count);
        sceneIndex = sceneIndexes[randomIndex];
    }

    private void RemoveSceneOfTheList()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneActiveID = currentScene.buildIndex;
        sceneIndexes.Remove(sceneActiveID);
    } //Sert à remove une scene utilisée d'une liste (pour ne pas jouer 2 fois la même scène) : N'est plus utilisée

    public override void GameObjectsActivationAtStartEatchGame()
    {
        _startGameCanvas.SetActive(true);
        _timer.SetActive(true);
        _healthBar.SetActive(true);
    }

    public override void DisablePlayerAfterDamage()
    {
        _player.gameObject.SetActive(false);
    }

    /** 
    * Gagner - Perdre au mode coop
    */

    public override void GameOver()
    {
        SceneManager.LoadScene("EndingScene");
        audioSource.PlayOneShot(looseSound);

        _thePartyIsFinished = true;
        isPlayerHasWin = false;
        StopGame();
    }

    public void WinPotato()
    {
        if (PlayerHealth.instance.currentHealth == 0)
        {
            isPlayerHasWin = false;

            GameOver();
        }
        else
        {
            _thePartyIsFinished = true;
            isPlayerHasWin = true;
            SceneManager.LoadScene("EndingScene");
            audioSource.PlayOneShot(winSound);
            StopGame();
        }

    }

    private void StopGame() // Pour ne pas avoir 2 fois les DDOL (Don't destroy on Load)
    {
        _startGameCanvas.SetActive(false);
        _healthBar.SetActive(false);
        _timer.SetActive(false);
    }

    /**
     * Set Active Player is only called by the Player Himself in the scene
     * it tells the game manager that the player in the scene is loaded
     */

    public override void NotificationPlayerAndSceneHasChanged(Player newSceneNewPlayer) // Update de tous les managers
    {
        _isNewSceneReadyToPlay = true;
        
        UpdatePlayerGameObject(newSceneNewPlayer);
        UpdateSpawnerManager();
        UpdateDialogManager();
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

    /**
     * Ganger - perdre un mini jeu
     */

    public void WinMiniGame()
    {
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

    public override void LoseMiniGame()
    {
        _miniGameFinished++;
        _spawnerManager.DeactivateSpawners();
        _hpPlayer--;
        if (_hpPlayer <= 0)
        {
            isPlayerHasWin = false;
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
            print("CALLED FROM LOOSE");
            Invoke("NewGame", 3f);
        }
    }
}
