using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileLullaby : MonoBehaviour
{
    [SerializeField] public float damage;
    int hits;
    public int weaponLVL;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            
            if (weaponLVL >= 3)
            {
                enemy.TakeDamage(damage++, 1);
                if (!enemy.slowed)
                {
                    enemy.slowed = true;
                    enemy.moveSpeed -= enemy.moveSpeed / 2;
                    enemy.moveSpeed -= enemy.moveSpeed / 2;
                }
            }
            else if(weaponLVL >= 1)
            {
                enemy.TakeDamage(damage++, 1);
                if (!enemy.slowed)
                {
                    enemy.slowed = true;
                    enemy.moveSpeed -= enemy.moveSpeed / 2;
                }     
            }
            else
            {
                enemy.TakeDamage(damage, 1);
                if (!enemy.slowed)
                {
                    enemy.slowed = true;
                    enemy.moveSpeed -= enemy.moveSpeed / 3;
                }   
            }
        }
        if (collider.gameObject.tag == "Fence")
        {
            Destroy(gameObject);
        }
    }
}
