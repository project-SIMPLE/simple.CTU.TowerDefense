using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSound : MonoBehaviour
{
    public AudioSource audioSound;
    // Start is called before the first frame update
    void Start()
    {
        // Tự động lấy AudioSource gắn trên GameObject
        audioSound = GetComponent<AudioSource>();
        audioSound.volume = 1.0f;  // Âm lượng từ 0.0 (im lặng) đến 1.0 (to nhất)
        audioSound.spatialBlend = 1.0f;       // 3D âm thanh
        audioSound.minDistance = 1.26f;          // Dưới 5m: âm thanh rõ
        audioSound.maxDistance = 3.36f;         // Trên 30m: hầu như im lặng
        audioSound.rolloffMode = AudioRolloffMode.Logarithmic;
    }

    // Update is called once per frame
    void Update()
    {
        //audioSound.Play();
    }

    
}
