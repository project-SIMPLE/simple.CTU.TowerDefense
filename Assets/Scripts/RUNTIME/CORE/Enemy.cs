using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable, IDamage
{
    [Header("Basic Info")]
    [SerializeField] private string uniqueName;
    [SerializeField] private int lvl;
    
    [Header("Stats")]
    [SerializeField] private int health = 2;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackSpeed = 5f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int attackDamage = 1;

    [Header("Miscellaneous")]
    public Material waterColor;
    public LayerMask targetLayerMask;

    // runtime privates
    private int currentHealh;
    
    // Getters
    public int Health { 
        get { return currentHealh; } 
    }
    public float Range {
        get { return attackRange; } 
    }
    public int Damage { 
        get { return attackDamage; } 
    }

    void Start()
    {
        currentHealh = health;
        var navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent) navAgent.speed = moveSpeed;
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
        gameObject.tag = "Water";
        gameObject.layer = LayerMask.NameToLayer("Water");
        gameObject.GetComponent<Renderer>().material = waterColor;
    }

    public bool IsDead()
    {
        return (currentHealh <= 0);
    }

    
    public bool HasValidTarget(GameObject target)
    {
        Debug.Log(target.name);
        return true;
    }

    public void DealDamage(IDamageable target)
    {
        target.TakeDamage(attackDamage);
    }

}
