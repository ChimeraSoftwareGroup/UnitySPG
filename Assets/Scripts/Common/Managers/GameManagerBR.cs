using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManagerBR : MonoBehaviour
{
    // GAME MANAGER MODE BATTLE ROYALE

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerDead = false;
    [SerializeField] public bool isPlayerHasWin;

    [Header("Game Parameters Set up by player")]
    [SerializeField] public float timeOfEachGameChosenByPlayers = 10;
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


    public bool isMiniGameFinished;
    private bool _gameHasStarted = false;
    private bool _thePartyIsFinished = false;
    private bool _isNewSceneReadyToPlay;

    private int sceneIndex = 2; // 2 is the index of landing page (Coop)
    private int sceneActiveID;
    private int[] indexList = new int[] { 6, 7, 8 };
    private int _miniGameFinished = 0;
    private int _hpPlayer = 3;

    private List<int> sceneIndexes = new List<int>();

    public static GameManagerBR instance;
    private void Awake()
    {
        if (instance == null) // Singleton : pour pouvoir appeler l'instance de ce script n'importe où
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
       initGameManager();
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
    

    /** 
    * Lancer le mode coop - les initialisations
    */

    private void initGameManager()
    {
        if(numberOfGames > 3) numberOfGames = 3;
        if(timeOfEachGameChosenByPlayers <= 0) timeOfEachGameChosenByPlayers = 20;
    }

    public void InputSettingsByPlayer()
    {
        _settingsByPlayerCanvas.gameObject.SetActive(true);
    }

    public void setParametersOfBRGame(
       int _nbMiniGames)
    {
        numberOfGames = _nbMiniGames;
    }

    /** 
    * Nouveau Mini jeu
    */

    [System.Obsolete]
    public void NewGame()
    {
        _gameHasStarted = false;
        _isNewSceneReadyToPlay = false;
        isMiniGameFinished = false;
        PrepareNextGameAndResetTimer();
        if(_spawnerManager) _spawnerManager.gameObject.SetActive(false);
        _screenDeath.SetActive(false);
        _countdown.StartCountDown();
        _timer.GetComponent<Timer>().SetTimer(timeOfEachGameChosenByPlayers);
    }

    private void PrepareNextGameAndResetTimer()
    {
        // RemoveSceneOfTheList(); // Maybe not remove the game previously done for Coop
        ShuffleGame();
        SceneManager.LoadScene(sceneIndex);
        GameObjectsActivationAtStartEatchGame();
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

    /** 
    * Gagner - Perdre au mode coop
    */

    public void GameOver()
    {
        SceneManager.LoadScene(4);
        audioSource.PlayOneShot(looseSound);

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
            SceneManager.LoadScene(4);
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

    public void NotificationPlayerAndSceneHasChanged(Player newSceneNewPlayer) // Update de tous les managers
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

    public void LoseMiniGame()
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
