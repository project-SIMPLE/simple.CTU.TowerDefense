using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SubsidenceInfo
{

    public List<string> subsidences;
    public int waterLocal;
    public int waterGlobal;

    public static SubsidenceInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SubsidenceInfo>(jsonString);
    }

}


