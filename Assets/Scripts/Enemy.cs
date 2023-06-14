using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    // public static event Action<Enemy> OnEnemyKilled;
    //Attribute vom Enemy
    [SerializeField] float health;
    [SerializeField] float maxhealth;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;

    Rigidbody2D rb;
    Transform target;
    GameObject targetObject;
    Player player;
    public GameObject damagePre;
    public AudioSource defaultDamage;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        targetObject = target.gameObject;
    }

    private void Start()
    {
        health = maxhealth;
    
    }
    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity=direction*moveSpeed;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            if (player==null)
            {
                player=targetObject.GetComponent<Player>();
            }
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        float critChance = 10;

        Buffs buffs = GameObject.Find("Buffs").GetComponent<Buffs>();
        if (buffs.buffBaseDamage != 0)
        {
            damageAmount = damageAmount + (buffs.buffXPGain*5);
        }
        if (buffs.buffCritChance != 0)
        {
            critChance = critChance + (buffs.buffCritChance * 5);
        }

        //Crit Chance
        if (UnityEngine.Random.Range(0, 100) <= critChance)
        {
            damageAmount = damageAmount + (damageAmount * 0.5f);
        }

        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }

    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            float damageNum = collision.GetComponent<Weapon>().GetDamage();
            //string weapontype = collision.GetComponent<Weapon>().GetType();
            CreateDamage(damageNum.ToString(),0);
        }
        if (collision.CompareTag("MesserGabel"))
        {
            float damageNum = collision.GetComponent<throwingMesserProjectile>().damage;
            //string weapontype = collision.GetComponent<Weapon>().GetType();
            CreateDamage(damageNum.ToString(),2);

        }
    }
    */

    public void CreateDamage(string damageNumstr,int type)
    {
        GameObject damagenum = ObjectPool.Instance.Get(damagePre);
        damagenum.transform.position = transform.position;
        damagenum.GetComponent<Damage>().Init(damageNumstr,type);

    }
    void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}
