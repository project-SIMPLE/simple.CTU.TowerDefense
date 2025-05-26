using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    private float SubsidenceScore = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SubsidenceScore = SubsidenceManager.currentSubsidenceLevel;
        if (SubsidenceScore == 1f) 
        {
             gameObject.SetActive(false);
        }
    }
}
