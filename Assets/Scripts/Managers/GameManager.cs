using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _screenDeath;
    [SerializeField] private GameObject _winScreen;

    public static bool isDead;


    //private Scene scene;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _screenDeath = GameObject.Find("GameOverMenu").GetComponent<GameObject>();
        _winScreen = GameObject.Find("WinMenu").GetComponent<GameObject>();
        isDead = false;
        _screenDeath.SetActive(false);
        _winScreen.SetActive(false);
        

    }
    private void Start()
    {
        //scene = SceneManager.GetActiveScene();
        FlutterUnityIntegration.NativeAPI.OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        print("On ENABLE GAMEMANAGER");
    }

    private void Update()
    {
       
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
    public void ReturnMasterScene(string message)
    {
        print("Return to Master " + message);
        SceneManager.LoadScene(0);
    }
    public void ResetScene()
    {
        print("Reset Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        _screenDeath.SetActive(true);
        isDead = true;

    }

}
