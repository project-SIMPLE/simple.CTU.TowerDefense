using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUI : MonoBehaviour
{
    private Gate gate;

    // Start is called before the first frame update
    void Start()
    {
        gate = GetComponent<Gate>();
    }

    // Update is called once per frame
    void Update()
    {
        var  renderer = gameObject.GetComponent<Renderer>();
        if (gate.Active)
           renderer.material.SetColor("_BaseColor", Color.green);
        else
            renderer.material.SetColor("_BaseColor", Color.red);
    }
}
