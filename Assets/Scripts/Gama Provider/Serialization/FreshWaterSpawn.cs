using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FreshWaterSpawn
{

    public List<string> pumpers;
    public List<int> spawnrates;

    public static FreshWaterSpawn CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<FreshWaterSpawn>(jsonString);
    }

}


