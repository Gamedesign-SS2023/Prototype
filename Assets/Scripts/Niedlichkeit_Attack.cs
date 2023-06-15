using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Niedlichkeit_Attack : WeaponBase
{
    [SerializeField] float radius = 3f;
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
        spawnniedlichkeit.transform.position= transform.position;
        spawnniedlichkeit.transform.parent = transform;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < hit.Length; i++)
        { 
            Damageable d = hit[i].GetComponent<Damageable>();
            if(d != null)
            {
                GameObject.Find("CutenessHit").GetComponent<AudioSource>().Play();
                if (weaponLVL == 3)
                {
                    d.TakeDamage(damage + 1);
                }
                else
                {
                    d.TakeDamage(damage);
                }
            }
        }
    }
}
