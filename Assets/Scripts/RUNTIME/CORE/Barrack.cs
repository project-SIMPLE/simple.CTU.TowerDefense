using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour, ISpawner, IDamageable 
{
    [Header("Basic Info")]
    [SerializeField] private string uniqueName;
    [SerializeField] private int lvl;

    [Header("Stats")]
    [SerializeField] private bool freeSpawn;
    [SerializeField] private int health;
    
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Miscellaneous")]
    [SerializeField] private float workRadius;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private LayerMask spawnTriggerLayerMask;

    // runtime privates
    private int currentHealh;
    private float currentRate;

    // Getters
    public int Health {
        get { return currentHealh; } 
    }
    public string SpawnName {
        get { return spawnPrefab.name; }
    }
    public float SpawnRate {
        get { return spawnRate; }
    }

    void Start()
    {
        currentHealh = health;
        currentRate = spawnRate;
    }

    void Update()
    {
        if (!IsDead())
        {
            currentRate -= Time.deltaTime;
            if (currentRate <= 0)
            {
                if (CanSpawn())
                {
                    Spawn();
                    currentRate = spawnRate;
                }
            }
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, workRadius, transform.forward, Mathf.Infinity, targetLayerMask);
        foreach(RaycastHit hit in hits)
        {
            Debug.Log(hit.transform.name);
            Destroy(hit.transform.gameObject);
        }
    }

    public bool IsDead()
    {
        return currentHealh <= 0;
    }

    public void Spawn()
    {
        Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        if (!freeSpawn) TakeDamage(1);
    }

    public bool CanSpawn()
    {
        Collider[] nearbySpawnTriggers = Physics.OverlapSphere(transform.position, workRadius, spawnTriggerLayerMask);
        return nearbySpawnTriggers.Length > 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //draw disable area
        Gizmos.DrawWireSphere(transform.position, workRadius);
    }

}
