using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOUSE : MonoBehaviour
{
    public Animator anim;
    private float currentSubsidenceLevel = 0f;

    public float SubsidenceScore
    {
        get { return currentSubsidenceLevel; } 
        set { currentSubsidenceLevel = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Test Animation
        if(Input.GetKeyDown("4"))
        {
            anim.Play("AM_HouseCollapsed", -1,0f);
        }

        if (currentSubsidenceLevel > 1.1f)
        {
            anim.Play("AM_HouseCollapsed", -1,0f);
        }
    }
}
