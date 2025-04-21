using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaterPumpUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    IDamageable damageable;
    // public Image enemy_UI_1;
    // public Image enemy_UI_2;
    // public Image enemy_UI_Clean;
    
    // Start is called before the first frame update
    void Start()
    {
       damageable = GetComponent<IDamageable>();
    //    enemy_UI_1.enabled = true;
    //    enemy_UI_2.enabled = false;
    //    enemy_UI_Clean.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int health = damageable.Health;
        if (health <= 0) 
        {
            // enemy_UI_1.enabled = false;
            // enemy_UI_2.enabled = false;
            // enemy_UI_Clean.enabled = true;
            hp.text = "0";
        }
        else if(health ==1)
        {
            // enemy_UI_1.enabled = false;
            // enemy_UI_2.enabled = true;
            // enemy_UI_Clean.enabled = false;
            hp.text = "1";
        }
        else hp.text = health.ToString();
    }
}
