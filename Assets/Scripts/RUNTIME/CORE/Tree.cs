using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tree : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int health = 2;

    // runtime privates
    public static int currentHealh;
    public Animator anim;
    public TextMeshProUGUI hp;
    private int count;
    private int condition = 0;

    // Getters
    public int Health
    {
        get { return currentHealh; }
    }

    void Start()
    {
        currentHealh = health;
        anim = GetComponent<Animator>();
        Debug.Log("Tree Start");
        // Debug.Log("Tree_currentHealh"+ currentHealh);
    }
    // private bool created = false;
    // int tick=0;

     public void TakeDamage(int damage)
    {
        currentHealh -= damage;
        //Debug.Log("TakeDamage_currentHealh: " + currentHealh);
        //hp.text = currentHealh.ToString();
        

        
        if (currentHealh <= 0)
        {            
            Debug.Log("currentHealh < 0: ");
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
        if (count > 0)
        {
            hp.text = count.ToString();
        }
        else hp.text = "Tree Die";

        // Test Animation
        if(Input.GetKeyDown("1"))
        {
        anim.Play("Tree_Good", -1,0f);
        }
        if(Input.GetKeyDown("2"))
        {
        anim.Play("Tree_Bad", -1,0f);
        }
        if(Input.GetKeyDown("3"))
        {
        anim.Play("Tree_Die", -1,0f);
        }


        // Check Condition Tree
        if (count < 70 && count > 65)
        {
            //condition = 1; 
            Debug.Log("khoi dong animation Tree Bad: ");
            anim.Play("Tree_Bad");
        }
        if (count < 1 && count > -5)
        {
            //condition = 2;
            Debug.Log("khoi dong animation Tree die: ");
            anim.Play("Tree_Die",-1,0f);

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
        Debug.Log("Xoa Cay: ");
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        Debug.Log("Tree mau ve khong: ");
        return currentHealh <= 0;
    }
}
