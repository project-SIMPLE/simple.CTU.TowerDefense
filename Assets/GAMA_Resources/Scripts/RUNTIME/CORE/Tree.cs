using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tree : MonoBehaviour, IDamageable
{
    /* 
    Tree: vn() --> Cây 
    Manage the response of plants when affected by the external environment. 
    ----------------------------------
    Message By Hồng Sơn: 
    In the future, we want to expand further on the salinity tolerance of different crop species.
    */

    [Header("Stats")]
    // [SerializeField] private int health = 2;

    // runtime privates
    public static int currentHealh;
    public Animator anim;
    AnimatorStateInfo stateInfo;
    public TextMeshProUGUI hp;
    private int count;
    public AudioSource crackSound;
    private bool hasFallen = false;
    // private int condition = 0;

    // Getters
    public int Health
    {
        get { return currentHealh; }
    }

    void Start()
    {
        //currentHealh = health;
        currentHealh = 600;
        anim = GetComponent<Animator>();
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // Debug.Log("Tree_currentHealh"+ currentHealh);

        // Tự động lấy AudioSource gắn trên GameObject
        crackSound = GetComponent<AudioSource>();
        crackSound.volume = 0.1f;  // Âm lượng từ 0.0 (im lặng) đến 1.0 (to nhất)
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
    // private bool created = false;
    // int tick=0;

    public void TakeDamage(int damage)
    {
        currentHealh -= damage;
        //Debug.Log("TakeDamage_currentHealh: " + currentHealh);
        //hp.text = currentHealh.ToString();
    
        if (currentHealh <= -20)
        {
            
            Dictionary<string, string> args = new Dictionary<string, string> {
                {"idP", ConnectionManager.Instance.GetConnectionId()},
                {"idT", gameObject.GetInstanceID()+"" }};

                ConnectionManager.Instance.SendExecutableAsk("delete_tree", args);

            // Debug.Log("currentHealh < 0: ");
            // anim.Play("Tree_Die", -1,0f);

            if (GameUI.Instance != null  && gameObject != null)
            {
                GameUI.Instance.DeletePlayer(gameObject);
            }
            Die();
        }
        // return currentHealh;
    }

    void Update()
    {           
        count = currentHealh;
        //Debug.Log("VoidUpdate_currentHealh: " + count);
        // if (count > 0)
        // {
        //     hp.text = count.ToString();
        // }
        // else hp.text = "Tree Die";

        // Test Animation
        if(Input.GetKeyDown("1"))
        {
        anim.Play("Tree_Good", -1,0f);
        }
        if(Input.GetKeyDown("2"))
        {
        anim.Play("Tree_Bad", -1,0f);
        StartCoroutine(PlayPartOfAudio(0f, 2.0f));
        }
        if(Input.GetKeyDown("3"))
        {
        anim.Play("Tree_Die", -1,0f);
        StartCoroutine(PlayPartOfAudio(0f, 2.0f));
        }


        // Check Condition Tree
        if (count < 350 && count > 320)
        {
            //condition = 1; 
            // Debug.Log("khoi dong animation Tree Bad: ");
            if (!stateInfo.IsName("Tree_Bad"))
            {
                anim.Play("Tree_Bad");
                Debug.Log("Active Animation Tree_Bad");
                StartCoroutine(PlayPartOfAudio(0f, 2.0f));
            }
            //anim.Play("Tree_Bad");
        }
        if (count < 1 && count > -20)
        {
            //condition = 2;
            // Debug.Log("khoi dong animation Tree die: ");
            if (!stateInfo.IsName("Tree_Die"))
            {
                anim.Play("Tree_Die");
                Debug.Log("Active Animation Tree_Die");
                StartCoroutine(PlayPartOfAudio(0f, 2.0f));
            }
            //anim.Play("Tree_Die",-1,0f);

        }
        
        // Control animation 
        // if(condition==1)
        // {
        //     Debug.Log("khoi dong animation Tree Bad: ");
        //     anim.Play("Tree_Bad", -1,0f);
        // }
        // if(condition==2)
        // {
        //     Debug.Log("khoi dong animation Tree die: ");
        //     anim.Play("Tree_Die", -1,0f);
        // }

        //  Debug.Log("sent to GAMA: ");
        // tick++;
        // if ( GameUI.Instance != null && gameObject != null)
        // {    
        //     // tick=0;        
        //     // Debug.Log("sent to GAMA: " + gameObject);

        //     GameUI.Instance.UpdateConstructionPosition(gameObject);
        //     // created = true;
        // }
        
    }

   
    public void Die()
    {
        // Debug.Log("Xoa Cay: ");
        if ( GameUI.Instance != null )
        {     
            GameUI.Instance.CountDeadTree(); 
        }
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        // Debug.Log("Tree mau ve khong: ");
        return currentHealh <= 0;
    }

    public void Fall()
    {   //Kiểm tra và phát âm thanh gãy đổ
        if (hasFallen || crackSound == null) return;

        // Gọi animation đổ cây (nếu có)
        // GetComponent<Animator>().SetTrigger("Fall");

        // Phát âm thanh gãy đổ
        //crackSound.Play();
        StartCoroutine(PlayPartOfAudio(0f, 2.0f));
        hasFallen = true;
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
