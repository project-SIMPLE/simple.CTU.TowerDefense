using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    public float speed;

    public float startHealth = 5f;
    public float health;

    public bool isDead = false;

    public TextMeshProUGUI hp;
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        hp.text = health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hp.text = health.ToString();
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

}
