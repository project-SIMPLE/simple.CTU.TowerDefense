using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerVR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Va chạm với: " + other.gameObject.name);
        if (other.CompareTag("Note"))
        {
            //Debug.Log("Va chạm với Enemy!");
            StartCoroutine(PauseNotesCoroutine());
        }
    }

    IEnumerator PauseNotesCoroutine()
    {
        // Dừng
        NoteMove.isPaused = true;

        // Đợi 1 giây
        yield return new WaitForSeconds(1f);

        // Tiếp tục
        NoteMove.isPaused = false;
    }

}
