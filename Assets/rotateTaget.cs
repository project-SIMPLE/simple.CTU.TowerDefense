using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTaget : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
     target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(target.position, Vector3.fo, 50* Time.deltaTime);
        if(target != null)
        {
            transform.LookAt(target);
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
