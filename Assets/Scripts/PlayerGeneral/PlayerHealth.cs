using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealthBar _healthBar;
    [SerializeField] int _maxHealth = 3;

    public int currentHealth;
    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void SetHP(int hp)
    {
        _healthBar.SetHealth(hp);
    }


    public void UpdateHealthbar(int damage)
    {
        
        currentHealth -= damage;
        GameManager.instance.DisablePlayerAfterDamage(); 
        _healthBar.SetHealth(currentHealth);
    }
}
