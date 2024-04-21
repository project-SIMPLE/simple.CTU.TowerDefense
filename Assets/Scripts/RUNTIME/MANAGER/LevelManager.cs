using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WaveStep 
{
    Preparation,
    Defense
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private List<EnemySpawner> spawns;

    // runtime privates
    private bool finished = false;
    private WaveStep currentWaveStep;
    private int currentWave;
    private float currentTime;
    private int currentWaveSpawnIndex;

    // Getters
    public bool Finished {
        get { return finished; }
    } 
    public int CurrentWave {
        get { return currentWave + 1; }
    } 
    public float CurrentTime {
        get { return currentTime; }
    }
    public string CurrentWaveStep {
        get { return currentWaveStep.ToString(); }
    } 

    void Start()
    {
        currentWaveStep = WaveStep.Preparation;
        currentWave = 0;
        currentTime = waves[0].preparationTime;
        currentWaveSpawnIndex = 0;
    }
    
    void Update()
    {
        if (finished) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (!MoveToDefenseStep())
                MoveToNextWave();
        }
        else
        {
            ExecuteDefenseStep();
        }  
        //Debug.Log("Wave"+(currentWave+1)+"-Step-"+currentWaveStep+"-Time: "+currentTime);      
    }

    bool MoveToDefenseStep()
    {
        if (currentWaveStep == WaveStep.Preparation)
        {
            currentTime = waves[currentWave].waveTime;
            currentWaveStep = WaveStep.Defense;
            return true;
        }
        return false;
    }

    bool MoveToNextWave()
    {
        if(currentWaveStep == WaveStep.Defense)
        {
            currentWave++;
            if (currentWave > waves.Count-1)
            {
                finished = true;
            }
            else
            {
                currentWaveStep = WaveStep.Preparation;
                currentTime = waves[currentWave].preparationTime;
                currentWaveSpawnIndex = 0;            
            }
            return true;
        }
        return false;
    }

    void ExecuteDefenseStep()
    {
        if (currentWaveStep != WaveStep.Defense) return;

        int start = currentWaveSpawnIndex;
        for (int i = start; i< waves[currentWave].waveSpawns.Count; i++)
        {
            Debug.Log("check i: " + i);
            float spawnTimeFrame = waves[currentWave].waveSpawns[i].timeFrame;
            if (waves[currentWave].waveTime - currentTime >= spawnTimeFrame)
            {
                int spawnPointIndex = waves[currentWave].waveSpawns[i].spawnPoint - 1;
                spawns[ spawnPointIndex ].StartAutoSpawn(
                    waves[currentWave].waveSpawns[i].spawnEnemy.gameObject,
                    waves[currentWave].waveSpawns[i].spawnAmount
                );
                currentWaveSpawnIndex = i+1; 
            }
            else
            {
                return;
            }
        }
    }
}
