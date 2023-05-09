using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerHealth _ph;
    public GameManager _gameManager;

    private void Start()
    {
        _ph = GameObject.Find("GameManager").GetComponent<PlayerHealth>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        NotifyGameManager();
    }

    private void Update()
    {
       // Commenter pour tester un jeu précis

        if (_ph.currentHealth == 0)
        {

            _gameManager.GameOver();
        }
    }

    private void NotifyGameManager()
    {
        _gameManager.NotificationPlayerAndSceneHasChanged(this);
    }
    
        

    
    public void PlayerTakeDamage()
    {
        _ph.UpdateHealthbar(1);
        _gameManager.LoseMiniGame();
        gameObject.SetActive(false);
    }

    IEnumerator MyFunction()
    { 
        yield return new WaitForSeconds(1);
    }

    public void SpecialInteraction(int damageReceived)
    {

    }
}
