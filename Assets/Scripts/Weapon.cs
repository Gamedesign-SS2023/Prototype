using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public string weaponType;
    public int weaponLVL;
    int hits;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            GameObject.Find("SlimeHit").GetComponent<AudioSource>().Play();

            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if(weaponLVL >= 2)
            {
                enemy.TakeDamage(damage+20);
            } else
            {
                enemy.TakeDamage(damage);
            }
            //enemy.CreateDamage(damage.ToString(), 0);

            //if not lvl3 then set hits to 2 to destroy gameobject upon first hit
            hits = (weaponLVL == 3) ? hits + 1 : hits + 2;
            if (hits == 2)
            {
                Destroy(gameObject);
            }
        }
        if (collider.gameObject.tag == "Fence")
        {
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Barrel")
        {
            Destroy(collider.gameObject);
        }
    }
    
    public float GetDamage()
    {
        return damage;
    }
}
