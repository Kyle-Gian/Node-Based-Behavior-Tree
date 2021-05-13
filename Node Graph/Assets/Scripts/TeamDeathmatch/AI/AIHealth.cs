//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
