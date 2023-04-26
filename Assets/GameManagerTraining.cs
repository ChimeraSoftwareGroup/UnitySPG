using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerTraining : GameManager
{
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerDead = false;

    [Header("Game Parameters Set up by player")]
    [SerializeField] public float timeOfEachGame = 20;
    [SerializeField] public int numberOfGames = 3; //Donnée en dur tant que le back n'estpas connecté

    [Header("Canvas")]
    [SerializeField] private GameObject _startGameCanvas;
    [SerializeField] private GameObject _screenDeath;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Countdown _countdown;

    [Header("Ingame Managers")]
    [SerializeField] private SpawnerManager _spawnerManager;

    [Header("Player Conf for Tutos")]
    [SerializeField] private bool _doWeShowTutorial = true;

    [Header("Sounds and musics")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip LostMiniGameSound;
    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ChoiceGameTraining _gameTraining;

    private bool _gameHasStarted = false;
    private bool _thePartyIsFinished = false;
    private bool _isWining = true;

    private int sceneActiveID;
    private int _miniGameFinished = 0;
    private int _hpPlayer = 1;

    private List<int> sceneIndexes = new List<int>();



    //Gaming Scenes
    //sceneIndexes.Add(7); // Sneuk
    //sceneIndexes.Add(8); // Froggy
    //sceneIndexes.Add(9); // Bobee
    //sceneIndexes.Add(10); // Giraffe
    //sceneIndexes.Add(11); // Brina
    //sceneIndexes.Add(12); // Sanic
    //sceneIndexes.Add(13); // Falleine


    public override void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    private void Update()
    {
        if (!_isWining)
        {
            _winScreen.SetActive(false);
        }
        #region ForTestGames

        if (Input.GetKeyDown(KeyCode.H) && _gameHasStarted)
        {
            _player.PlayerTakeDamage();
        }


        if (Input.GetKeyDown(KeyCode.J) && _gameHasStarted)
        {
            WinBR();
        }

        #endregion

        if (isMiniGameFinished && _isWining)
        {
            WinBR();
        }

        if (
            _countdown.isCountdownFinish
            && !_thePartyIsFinished
            && !_gameHasStarted
        )
        {
            _player.gameObject.SetActive(true);
            _spawnerManager.ActivateSpawners();
            _timer.gameObject.SetActive(true);
            _timer.GetComponent<Timer>().StartTimer();
            _gameHasStarted = true;
        }
    }
    public override void NewGame()
    {
        _gameHasStarted = false;
        isMiniGameFinished = false;
        GameObjectsActivationAtStartEatchGame();
        SceneManager.LoadScene(_gameTraining._gameChoose);

        if (_spawnerManager) _spawnerManager.gameObject.SetActive(false);
        _screenDeath.SetActive(false);
        _countdown.StartCountDown();
        _timer.GetComponent<Timer>().SetTimer(timeOfEachGame);
    }

    private void UpdatePlayerGameObject(Player newActivePlayerFromScene)
    {
        _player = newActivePlayerFromScene;
        _timer.GetComponent<Timer>().StopTimer();
        _timer.gameObject.SetActive(false);
        _player.gameObject.SetActive(false);
    }

    #region Update Managers
    public override void NotificationPlayerAndSceneHasChanged(Player newSceneNewPlayer) // Update de tous les managers
    {
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

    #endregion

    public override void GameOver()
    {

        audioSource.PlayOneShot(LostMiniGameSound);
        _screenDeath.SetActive(true);
        _countdown.isCountdownFinish = false;
        _player.gameObject.SetActive(false);
        _isPlayerDead = true;
        _thePartyIsFinished = true;
        Invoke("StopGame", 3f);
    }

    public void WinBR()
    {
        if (_isWining)
        {
            isMiniGameFinished = false;
            _player.gameObject.SetActive(false);

            _winScreen.SetActive(true);
            _thePartyIsFinished = true;
            isPlayerHasWin = true;

            Invoke("StopGame", 3f);
        }
      
    }
   

    private void StopGame() // Pour ne pas avoir 2 fois les DDOL (Don't destroy on Load)
    {
        _isWining = false;
        _screenDeath.SetActive(false);
        _winScreen.SetActive(false);
        SceneManager.LoadScene(0);
        _startGameCanvas.SetActive(false);
        _healthBar.SetActive(false);
        _timer.SetActive(false);
    }

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
    public override void LoseMiniGame()
    {
        isPlayerHasWin = false;
        _isWining = false;
        GameOver();
    }
    #region Classes GM Useless




    public override void setParametersOfCoopGame(List<string> nameOfPlayeList, bool isShuffleOn, float timeSelectedInSeconds, int numberOfMiniGamesSelected)
    {
        throw new System.NotImplementedException();
    }

 

    #endregion
}
