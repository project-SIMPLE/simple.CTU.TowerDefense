using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsidenceManager : MonoBehaviour
{
    [SerializeField] private bool isSubsidence = false;
    [SerializeField] private float currentWaterLevel = 1f;
    [SerializeField] private float currentSubsidenceLevel = 0f;

    public void IncreaseSubsidenceLevel(float amount)
    {
        currentSubsidenceLevel += amount;
    }

    public void DecreaseWaterLevel(float amount)
    {
        currentWaterLevel -= amount;
    }

    void Update()
    {
        if (currentWaterLevel <= 0 || currentSubsidenceLevel >= 1)
        {
            isSubsidence = true;
        }
        else
        {
            isSubsidence = false;
        }

    }
}
