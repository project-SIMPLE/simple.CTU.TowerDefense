using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public GameObject river;
	private Vector3 offset;

    void Start()
    {
        offset = transform.position - river.transform.position;
    }

    void LateUpdate ()
	{
        //lift the boat up with the water - nâng thuyền lên theo dòng nước
		transform.position = river.transform.position + offset;
	}
}
