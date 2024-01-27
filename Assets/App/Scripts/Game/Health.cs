using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthChangedEvent : UnityEvent<int> { }

[System.Serializable]
public class ZeroHealthEvent : UnityEvent { }

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth;

    // Unity Events
    public HealthChangedEvent OnHealthChanged;
    public ZeroHealthEvent OnZeroHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            OnHealthChanged?.Invoke(currentHealth); // Invoke the UnityEvent for health change

            if (currentHealth <= 0)
            {
                OnZeroHealth?.Invoke(); // Invoke the UnityEvent when health reaches zero
            }
        }
    }

    // Adds health and triggers the health changed event
    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
    }

    // Sets health to a specific value and triggers the health changed event
    public void SetHealth(int amount)
    {
        CurrentHealth = amount;
    }

    // Removes health and triggers the health changed and potentially the zero health event
    public void RemoveHealth(int amount)
    {
        CurrentHealth -= amount;
    }
}