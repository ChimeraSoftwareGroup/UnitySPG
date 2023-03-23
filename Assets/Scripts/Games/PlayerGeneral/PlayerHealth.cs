using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    [SerializeField] int _currentHealth = 3;
    public HealthBar _healthBar;
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        _healthBar.SetMaxHealth(_maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.H))
        {
            UpdateHealthbar(1);
        }
    }

    public void UpdateHealthbar(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
    }
}
