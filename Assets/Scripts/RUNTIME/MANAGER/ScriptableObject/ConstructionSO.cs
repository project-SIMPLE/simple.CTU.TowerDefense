using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Construction1", menuName = "ScriptableObjects/ConstructionSO")]
public class ConstructionSO : ScriptableObject
{
    public GameObject modelBuildPrefab;
    public GameObject finalPrefab;
    public int cost;
    [TextAreaAttribute]
    public string description;
    public bool useIdentityRotation;

}
