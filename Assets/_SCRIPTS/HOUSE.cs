using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOUSE : MonoBehaviour
{
    public Animator anim;
    AnimatorStateInfo stateInfo;
    public float SubsidenceScore = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Test Animation
        if(Input.GetKeyDown("4"))
        {
            anim.Play("AM_HouseCollapsed", -1,0f);
        }
        if(Input.GetKeyDown("1"))
        {
            anim.Play("AM_House_0", -1,0f);
        }
        SubsidenceScore = SubsidenceManager.currentSubsidenceLevel;
        //Debug.Log("static value: " + SubsidenceManager.currentSubsidenceLevel);
        Debug.Log("SubsidenceScore: " + SubsidenceScore);
       
        if (SubsidenceScore > 1.0f)
        {
            if (!stateInfo.IsName("AM_HouseCollapsed"))
            {
                anim.Play("AM_HouseCollapsed");
                Debug.Log("Active Animation AM_HouseCollapsed");
            }

        }
    }
}
