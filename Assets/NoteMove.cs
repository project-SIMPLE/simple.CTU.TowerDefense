using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float speed = 5f;
    // Biến static điều khiển toàn bộ note
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.CompareTag("GroupTree"))
        {
             Destroy(gameObject, 60f); 
        }
        else if (gameObject.CompareTag("GroupRocks"))
        {
            Destroy(gameObject, 300f);
        }
        else {
             Destroy(gameObject, 15f); 
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;
        // Di chuyển NGƯỢC lại hướng Z local của Note (Z-)
        transform.Translate(-transform.forward * speed * Time.deltaTime, Space.World);
    }
}
