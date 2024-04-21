using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int health = 2;

    // runtime privates
    private int currentHealh;

    // Getters
    public int Health { 
        get { return currentHealh; } 
    }

    void Start()
    {
        currentHealh = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealh -= damage;
        if(currentHealh <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealh <= 0;
    }
}
