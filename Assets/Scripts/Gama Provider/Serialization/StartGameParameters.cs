using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StartGameParameters
{
    public int time_prep;
    public int time_def;

    public static StartGameParameters CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<StartGameParameters>(jsonString);
    }

}