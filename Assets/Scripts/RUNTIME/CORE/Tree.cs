using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int health = 2;

    // runtime privates
    private int currentHealh;
    // public Animator anim;

    // Getters
    public int Health
    {
        get { return currentHealh; }
    }

    void Start()
    {
        currentHealh = health;
        // anim = GetComponent<Animator>();

    }
    // private bool created = false;
    // int tick=0;
    void Update()
    {           
        //  Debug.Log("sent to GAMA: ");
        // tick++;
        if ( GameUI.Instance != null && gameObject != null)
        {    
            // tick=0;        
            // Debug.Log("sent to GAMA: " + gameObject);

            GameUI.Instance.UpdateConstructionPosition(gameObject);
            // created = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealh -= damage;
        // if (currentHealh <= 7)
        // {
        //     anim.Play("Tree_Bad", -1,0f);
        // }
        if (currentHealh <= 0)
        {            
            // Debug.Log("DeletePlayer: ");
            //anim.Play("Tree_Die", -1,0f);

            if (GameUI.Instance != null  && gameObject != null)
            {
                GameUI.Instance.DeletePlayer(gameObject);
            }
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return currentHealh <= 0;
    }
}
