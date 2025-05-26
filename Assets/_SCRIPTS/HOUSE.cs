using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOUSE : MonoBehaviour
{
    public Animator anim;
    AnimatorStateInfo stateInfo;
    private float SubsidenceScore = 0.0f;
    public AudioSource crackSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Tự động lấy AudioSource gắn trên GameObject
        crackSound = GetComponent<AudioSource>();
        crackSound.volume = 0.4f;  // Âm lượng từ 0.0 (im lặng) đến 1.0 (to nhất)
        crackSound.spatialBlend = 1.0f;       // 3D âm thanh
        crackSound.minDistance = 5f;          // Dưới 5m: âm thanh rõ
        crackSound.maxDistance = 30f;         // Trên 30m: hầu như im lặng
        crackSound.rolloffMode = AudioRolloffMode.Logarithmic;
        // Kiểm tra có gắn AudioSource không
        if (crackSound == null)
        {
            Debug.LogWarning("Không tìm thấy AudioSource trên " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Test Animation
        if(Input.GetKeyDown("4"))
        {
             crackSound.Play();
             StartCoroutine(DoSomethingWithDelay());
             
             //PlayPartOfAudio(3.0f, 5.0f);
           
        }
        if(Input.GetKeyDown("1"))
        {
            anim.Play("AM_House_0", -1,0f);
        }
        SubsidenceScore = SubsidenceManager.currentSubsidenceLevel;
        //Debug.Log("static value: " + SubsidenceManager.currentSubsidenceLevel);
        Debug.Log("SubsidenceScore: " + SubsidenceScore);
       
        if (SubsidenceScore == 1.2f)  //GAMA 2.
        {
            if (!stateInfo.IsName("AM_HouseCollapsed"))
            {
                crackSound.Play();
                StartCoroutine(DoSomethingWithDelay());
                //anim.Play("AM_HouseCollapsed");
                //PlayPartOfAudio(3.0f, 5.0f);
                //Debug.Log("Active Animation AM_HouseCollapsed");
            }

        }
    }

 IEnumerator DoSomethingWithDelay()
    {
        yield return new WaitForSeconds(3f); // Delay 3 giây
        anim.Play("AM_HouseCollapsed", -1,0f);
    }

    IEnumerator PlayPartOfAudio(float startTime, float duration)
    { 
        //Phát âm thanh chỉ trong khoảng thời gian ngắn
        crackSound.time = startTime;
        crackSound.Play();

        yield return new WaitForSeconds(duration);
        crackSound.Stop();
    }
}
