using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerHealth _ph;
    private GameManager _gameManager;

    private void Start()
    {
        _ph = GameObject.Find("GameManager").GetComponentInParent<PlayerHealth>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PlayerTakeDamage()
    {
        print("In playerTakeDamage");
        _ph.UpdateHealthbar(1);
        _gameManager.GameOver();
        gameObject.SetActive(false);
    }

    public void SpecialInteraction(int damageReceived)
    {

    }
}
