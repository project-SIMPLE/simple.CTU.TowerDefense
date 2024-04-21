using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private float attackInterval = 5f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private int attackDamage = 1;

    [Header("Miscellaneous")]
    public Material waterColor;
    public LayerMask targetLayerMask;

    // runtime privates
    private int currentHealh;
    private float currentInterval;
    
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
        currentInterval = attackInterval;
        var navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent) navAgent.speed = moveSpeed;
    }

    void Update()
    {
        if (IsDead()) return;

        currentInterval -= Time.deltaTime;
        if (currentInterval <= 0)
        {
            Attack();
            currentInterval = attackInterval;
        }
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
        //gameObject.GetComponent<Renderer>().material = waterColor;
    }

    public bool IsDead()
    {
        return (currentHealh <= 0);
    }


    private void Attack()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, attackRange, targetLayerMask);

        foreach (var target in nearbyTargets)
        {
            var health = target.GetComponent<IDamageable>();
            if (health != null) DealDamage(health);
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //draw disable area
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
