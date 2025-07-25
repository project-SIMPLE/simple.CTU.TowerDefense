using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteSpawnerRock : MonoBehaviour
{
    public GameObject notePrefab;
    public float spawnInterval = 10.0f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Instantiate(notePrefab, transform.position, transform.rotation);
            timer = 0f;
        }
    }
}
