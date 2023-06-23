using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Niedlichkeit_Attack : WeaponBase
{
    float radius = 1f;
    [SerializeField] GameObject prefab;
    public float ttl;
    public int weaponLVL;
    public float damage;

    private void Start()
    {
        Destroy(prefab, ttl);
    }
    public override void Attack()
    {
        GameObject spawnniedlichkeit = Instantiate(prefab);
        spawnniedlichkeit.transform.position = transform.position;
        spawnniedlichkeit.transform.parent = transform;
        if (weaponLVL >= 1)
        {
            radius = 2f;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            //Damageable d = hit[i].GetComponent<Damageable>();
            //if(d != null)
            //{
            if (colliders[i].gameObject.tag == "Enemy")
            {
                GameObject.Find("CutenessHit").GetComponent<AudioSource>().Play();
                if (weaponLVL == 3)
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage + 1);
                }
                else
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            if (colliders[i].gameObject.tag == "Barrel")
            {
                Destroy(colliders[i].gameObject);
            }
            //}
        }
    }
}
