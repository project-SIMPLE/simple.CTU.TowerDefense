using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerAudioSync : MonoBehaviour
{
    public GameObject[] gameObjects;

    public AudioSource musicSource;     // Nhạc nền
    public float[] beatTimes;           // Danh sách thời điểm spawn note (giây)
    //public Transform[] spawnPoints;     // Các vị trí spawn khác nhau (tùy chọn)

    private int beatIndex = 0;
    private float timer = 0f;
     
    //public AudioSource music;

    void Start()
    {
        musicSource.Play();

        // khoi tạo theo nhip nhac
        beatTimes = new float[500];  
        for (int i=0; i< 500; i++)
        {
            if (i ==0){
                beatTimes[i] = 0.537F;
                Debug.Log("beatTimes: " + beatTimes[i]);
            }
            else if (i%2==1)
            {
                beatTimes[i]+= 0.514F;
                Debug.Log("beatTimes: " + beatTimes[i]);
            }
            else 
            {
                beatTimes[i]+= 0.537F;
                Debug.Log("beatTimes: " + beatTimes[i]);
            }
        }
        

    }

    void Update()
    {
        
        if (NoteMove.isPaused != true)
        {
            timer += Time.deltaTime;

            if (timer >= beatTimes[beatIndex])
            {
                // int randomIndex = Random.Range(0, gameObjects.Length);
                // Instantiate(gameObjects[randomIndex], transform.position, transform.rotation);
                
                if (beatIndex%2==1) // giảm độ nhanh
                {
                    int randomIndex = Random.Range(0, gameObjects.Length);
                    Debug.Log("Khoi tao note");
                    Instantiate(gameObjects[randomIndex], transform.position, transform.rotation);
                    timer = 0.0f;
                } else {
                    Debug.Log("giữ nhịp");
                    timer = 0.0f;
                }
                // timer = 0.0f;
                beatIndex++;
                
            }
        }
        else return;
        
    }

    
}
