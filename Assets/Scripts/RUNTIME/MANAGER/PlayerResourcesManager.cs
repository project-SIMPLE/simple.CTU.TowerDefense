using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesManager : MonoBehaviour, ISupply
{
    [Header("Stats")]
    [SerializeField] private float workRadius = 10f;
    [SerializeField] private float refillInterval = 2f;
    [SerializeField] private int refillAmount = 1;

    [Header("Miscellaneous")]
    [SerializeField] private LayerMask targetLayerMask;


    // runtime privates
    private int currentAmount;
    private float currentInterval;

    // Getters
    public int CurrentAmount { 
        get { return currentAmount; }
    }
    public float WorkRadius { 
        get { return workRadius; }
    }
    public float RefillInterval { 
        get { return refillInterval; }
    }
    public int RefillAmount { 
        get { return refillAmount; }
    }

    void Start()
    {
        currentAmount = 0;
        currentInterval = refillInterval;
    }

    void Update()
    {
        currentInterval -= Time.deltaTime;
        if (currentInterval <= 0)
        {
            Refill();
            currentInterval = refillInterval;
        }
    }

    // for subtract resoucres when build a construction
    public bool Supply(int amount)
    {
        if (currentAmount >= amount)
        {
            currentAmount -= amount;
            return true;
        }
        return false;
    }

    public void Refill()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, workRadius, targetLayerMask);
        currentAmount += refillAmount * nearbyTargets.Length;
    }

    public bool IsEmpty()
    {
        return currentAmount <= 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //draw disable area
        Gizmos.DrawWireSphere(transform.position, workRadius);
    }

}
