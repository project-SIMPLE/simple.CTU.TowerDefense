using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IDamage
{
    [Header("Basic Info")]
    [SerializeField] private string uniqueName;
    [SerializeField] private int lvl;

    [Header("Stats")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private int attackRate = 1;
    [SerializeField] private float attackTime = 1.0f;

    [Header("Miscellaneous")]
    [SerializeField] private LayerMask targetLayerMask;

    // runtime privates
    private float currentRate = 0f;
    private float currentATime = 0f;
    private bool isActive = false;


    // Getters
    public float Range { 
        get { return 0; }
    }
    public int Damage { 
        get { return attackDamage; } 
    }
    public bool Active {
        get { return isActive; }
    }

    void Start()
    {
        currentRate = attackRate;
    }

    void Update()
    {
        if (isActive)
        {
            Block();
        }
        else
        {
            currentRate -= Time.deltaTime;
            if (currentRate <= 0)
            {
                isActive = true;
                currentRate = attackRate;
                currentATime = attackTime;
            }
        } 
    }

    public void Block()
    {
        currentATime-= Time.deltaTime;
        if (currentATime <= 0)
        {
            isActive = false;
            return;
        }

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (HasValidTarget(collider.gameObject))
            {
                var target = collider.gameObject.GetComponent<IDamageable>();
                if (target != null) 
                {
                    DealDamage(target);
                    if (target.IsDead()) Destroy(collider.gameObject);
                }
                    
            }
        }
    }

    public bool HasValidTarget(GameObject target)
    {
        return (targetLayerMask == (targetLayerMask | (1 << target.layer)));
    }

    public void DealDamage(IDamageable target)
    {
        target.TakeDamage(attackDamage);
    }

}
