using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnerInfo
{

    public List<string> enemyspawners;
    public List<int> spawnrates;

    public static EnemySpawnerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<EnemySpawnerInfo>(jsonString);
    }

}


