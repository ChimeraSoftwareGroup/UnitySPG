using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private Text textEnd;
    public GameManager _gameManager;
    [SerializeField] GameObject _fireworks;
    [SerializeField] GameObject _rain;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject _gameCanvas;

    [Header("Destroy")]
    [SerializeField] GameObject _generalGameCanvas;
    [SerializeField] GameObject _dialogManager;
    [SerializeField] GameObject _spawnerManager;
    [SerializeField] GameObject _settingsByPlayer;


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
    }

    private void Wining() 
    {
        textEnd.text = "YOU WIN";
        _fireworks.SetActive(true);
        _rain.SetActive(false);

    }
    private void Losing() 
    {
        textEnd.text = "YOU LOSE";
        _rain.SetActive(true);
        _fireworks.SetActive(false);

        bg.GetComponent<SpriteRenderer>().color = new Color32(114, 114, 114, 225);

    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        //SceneManager.UnloadScene(4);
    }
}
