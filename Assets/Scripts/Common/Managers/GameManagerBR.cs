using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerBR : GameManager 
{
    // GAME MANAGER MODE BATTLE ROYALE

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerDead = false;

    [Header("Game Parameters Set up by player")]
    [SerializeField] public float timeOfEachGame = 10;
    [SerializeField] public int numberOfGames = 3; //Donnée en dur tant que le back n'estpas connecté

    [Header("Canvas")]
    [SerializeField] private GameObject _startGameCanvas;
    [SerializeField] private GameObject _screenDeath;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _settingsByPlayerCanvas;
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

    private bool _gameHasStarted = false;
    private bool _thePartyIsFinished = false;

    private int sceneActiveID;
    private int _miniGameFinished = 0;
    private int _hpPlayer = 3;

    private List<int> sceneIndexes = new List<int>();

    public override void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    private void Start()
    {
        // Set up la Socket qui attends le retour du backend.


    }

    //Gaming Scenes
        //sceneIndexes.Add(6); // Sneuk
        //sceneIndexes.Add(7); // Froggy
        //sceneIndexes.Add(8); // Bobee
        //sceneIndexes.Add(9); // Giraffe
        //sceneIndexes.Add(10); // Brina
        //sceneIndexes.Add(11); // Sanic
        //sceneIndexes.Add(12); // Falleine

    public void StartBattleRoyale() {
        sceneIndexes.Add(6);
        sceneIndexes.Add(12);
        sceneIndexes.Add(10);
        NewGame();
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
    
    public void InputSettingsByPlayer()
    {
        _settingsByPlayerCanvas.gameObject.SetActive(true);
    }
    
    // Should be out of the GameManager
    public override void setParametersOfCoopGame(
       List<string> playerList,
       bool isTutoOn,
       float timerChoosed,
       int numberOfMiniGamesChoosed
   )
    {
    }


    /** 
    * Nouveau Mini jeu
    */

    public override void NewGame()
    {
        if (sceneIndexes.Count == 0)
        {
            GameOver();
            return;
        }
        _gameHasStarted = false;
        isMiniGameFinished = false;
        PrepareNextGameAndResetTimer();
        if(_spawnerManager) _spawnerManager.gameObject.SetActive(false);
        _screenDeath.SetActive(false);
        _countdown.StartCountDown();
        _timer.GetComponent<Timer>().SetTimer(timeOfEachGame);
    }

    private void PrepareNextGameAndResetTimer()
    {
        // RemoveSceneOfTheList(); // Maybe not remove the game previously done for Coop
        SceneManager.LoadScene(sceneIndexes[0]);
        RemoveSceneOfTheList(sceneIndexes[0]);
        GameObjectsActivationAtStartEatchGame();
        // Ne pas l'appeler si les utilisateurs ne veulent pas des tutos.
        //if (_doWeShowTutorial) DialogManager.instance.StartTutorialDialog();
    }

    private void RemoveSceneOfTheList(int sceneActive)
    {
        sceneIndexes.Remove(sceneActive);
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

    /** 
    * Gagner - Perdre au mode coop
    */

    public override void GameOver()
    {
        SceneManager.LoadScene(5);
        // audioSource.PlayOneShot(looseSound);
        _screenDeath.SetActive(false);

        _thePartyIsFinished = true;
        isPlayerHasWin = false;
        StopGame();
    }

 
    public void WinBR()
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
            SceneManager.LoadScene(5);
           // audioSource.PlayOneShot(winSound);
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

    private void WinMiniGame()
    {
        _miniGameFinished++;
        isMiniGameFinished = false;
        _spawnerManager.DeactivateSpawners();
        if (_miniGameFinished == numberOfGames)
        {
            WinBR();
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
            WinBR();
        }
        else
        {
            audioSource.PlayOneShot(LostMiniGameSound);
            _screenDeath.SetActive(true);
            _countdown.isCountdownFinish = false;
            _player.gameObject.SetActive(false);
            _isPlayerDead = true;
            
        }
    }
}
