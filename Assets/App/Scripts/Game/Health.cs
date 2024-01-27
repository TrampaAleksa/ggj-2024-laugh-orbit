using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthChangedEvent : UnityEvent<int> { }

[System.Serializable]
public class ZeroHealthEvent : UnityEvent { }

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100; 
    [SerializeField]
    private int currentHealth;

    public HealthChangedEvent OnHealthChanged;
    public ZeroHealthEvent OnZeroHealth;

    private void Start()
    {
        CurrentHealth = maxHealth; 
    }

    public int CurrentHealth
    {
        get => currentHealth;
        private set
        {
            int adjustedValue = Mathf.Clamp(value, 0, maxHealth); 
            currentHealth = adjustedValue;
            OnHealthChanged?.Invoke(currentHealth); 

            if (currentHealth <= 0)
            {
                OnZeroHealth?.Invoke(); 
            }
        }
    }

    public int MaxHealth => maxHealth;

    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
    }

    public void SetHealth(int amount)
    {
        CurrentHealth = amount;
    }

    public void RemoveHealth(int amount)
    {
        CurrentHealth -= amount;
    }
}