using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private List<Transform> wayPoints;

    // runtime privates
    private int spawnCount = 10;
    private int minSpawnCount = 5;
    private int maxSpawnCount = 20;
    private int count = 0;

    // Getters
    public string SpawnName
    {
        get { return spawnPrefab.name; }
    }
    public float SpawnRate
    {
        get { return spawnRate; }
        set { spawnRate = value; }
    }

    public void Spawn()
    {
        if (!spawnPrefab) return;

        GameObject spawn = Instantiate(spawnPrefab, transform.position, Quaternion.identity, this.gameObject.transform);
        spawn.GetComponent<EnemyController>().SetDestination(wayPoints);
        count++;
        if (count >= spawnCount)
        {
            CancelInvoke();
        }
    }

    public void StartAutoSpawn(GameObject spawn, int amount)
    {
        spawnPrefab = spawn;
        spawnCount = spawnRate == 0 ? minSpawnCount : Mathf.Max(minSpawnCount, Mathf.Min(maxSpawnCount, (int)(amount / spawnRate)));
        count = 0;
        InvokeRepeating("Spawn", .5f, spawnRate);
    }

}

