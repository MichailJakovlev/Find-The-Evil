using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthAmount = 10;
    [HideInInspector] private int _currentHealth;

    public event Action<int> OnHealthChanged;

    public int currentHealth
    {
        get => _currentHealth;
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
                OnHealthChanged?.Invoke(_currentHealth);
            }
        }
    }
    
    private void Awake()
    {
        _currentHealth = healthAmount;
    }

    public void Damage(int damagePoints = 5)
    {
        if (currentHealth - damagePoints <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damagePoints;
        }
    }
    
    public void Heal()
    {
        currentHealth = healthAmount;
    }

    public void Heal(int healPoints)
    {
        if (currentHealth + healPoints > healthAmount)
        {
            currentHealth = healthAmount;
        }
        else
        {
            currentHealth += healPoints;
        }
    }

}
