using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn_Scene1_VU2 : MonoBehaviour
{
    public float speed = 2f;

    public float pushForce = 5f;

    //private bool isStopped = false; // thêm cờ trạng thái dừng

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (NoteMove.isPaused == true)
        {
             transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rbThis = GetComponent<Rigidbody>();
        Rigidbody rbOther = other.attachedRigidbody;

        if (rbThis != null && rbOther != null)
        {
            // Vector từ đối tượng này sang đối tượng kia
            Vector3 dirToOther = (other.transform.position - transform.position).normalized;
            Vector3 dirToThis = -dirToOther;

            // Đẩy nhẹ cả hai
            rbThis.AddForce(dirToThis * pushForce, ForceMode.Impulse);
            rbOther.AddForce(dirToOther * pushForce, ForceMode.Impulse);

           
        }
    }

    // Applies an upwards force to all rigidbodies that enter the trigger.
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Đang Va chạm với Enemy!");
    }

}
