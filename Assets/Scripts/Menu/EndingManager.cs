using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private Text textEndLoose;
    [SerializeField] private Text textEndWin;
    public GameManager _gameManager;
    [SerializeField] GameObject _fireworks;
    [SerializeField] GameObject _rain;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject _gameCanvas;
    [SerializeField] GameObject _animWin;
    [SerializeField] GameObject _animLoose;
    [SerializeField] GameObject _animWinFireworks;


    [Header("Destroy")]
    [SerializeField] GameObject _generalGameCanvas;
    [SerializeField] GameObject _dialogManager;
    [SerializeField] GameObject _spawnerManager;
    [SerializeField] GameObject _settingsByPlayer;

    public bool win = true;

    public static EndingManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EndingManager dans la scène");
            return;
        }

        instance = this;
        
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameCanvas = GameObject.Find("GeneralGameCanvas");

    }
    private void Start()
    {
        _gameCanvas.SetActive(false);
        if (_gameManager.isPlayerHasWin == true)
        {
            Wining();
        }
        else
        {
            Losing();
        }

        //if (win)
        //{
        //    Wining();
        //}else
        //{
        //    Losing();
        //}
    }

    private void Wining() 
    {
        textEndLoose.text = "YOU WIN";
        textEndWin.text = "YOU WIN";
        _fireworks.SetActive(true);
        _rain.SetActive(false);
        _animLoose.SetActive(false);
        _animWin.SetActive(true);
        Invoke("AnimWinFireworks", 1f);

    }
    private void AnimWinFireworks()
    {
        _animWinFireworks.SetActive(true);

    }
    private void Losing() 
    {
        textEndLoose.text = "YOU LOSE";
        textEndWin.text = "YOU LOSE";
        _rain.SetActive(true);
        _fireworks.SetActive(false);
        _animLoose.SetActive(true);
        _animWin.SetActive(false);

        bg.GetComponent<SpriteRenderer>().color = new Color32(114, 114, 114, 225);

    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        //SceneManager.UnloadScene(4);
    }
}
