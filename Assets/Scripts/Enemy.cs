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

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            float damageNum = collision.GetComponent<Weapon>().GetDamage();
            //string weapontype = collision.GetComponent<Weapon>().GetType();
            CreateDamage(damageNum.ToString());

        }
    }

    void CreateDamage(string damageNumstr)
    {
       GameObject damagenum = ObjectPool.Instance.Get(damagePre);
       damagenum.transform.position = transform.position;
       damagenum.GetComponent<Damage>().Init(damageNumstr);
    }
    void Die()
    {
        
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}
