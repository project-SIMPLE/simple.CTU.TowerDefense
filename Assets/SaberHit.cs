using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberHit : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float destroyDelay = 1.5f;
    private int hitCount = 0; // Biến đếm số note đã chém
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
        if (other.CompareTag("Note"))
        {
            //Destroy(other.gameObject);
            // Tăng số lần va chạm
            hitCount++;
            Debug.Log("Note bị chém! Tổng số: " + hitCount);

             Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Bật vật lý
                rb.isKinematic = false;

                // Tính hướng đẩy từ Saber đến Note
                Vector3 forceDir = (other.transform.position - transform.position).normalized;

                // Áp lực đẩy
                rb.AddForce(forceDir * knockbackForce, ForceMode.Impulse);
            }


        }
    }
}
