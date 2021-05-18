//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 17/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public int _currentHealth;
    public int _maxHealth;
    public HealthBar _healthBar;
    private void Start()
    {
        _healthBar = transform.GetComponentInChildren<HealthBar>();
        _currentHealth = _maxHealth;
        
        _healthBar.SetMaxHealth(_maxHealth);
        
    }

    public void Hurt(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
        if (_currentHealth <= 0)
            Death();
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }
}
