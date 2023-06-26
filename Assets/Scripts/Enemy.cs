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
    public GameObject EXPPrefab;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        targetObject = target.gameObject;
    }

    private void Start()
    {
        maxhealth = GameObject.Find("Player").GetComponent<Player>().level * maxhealth;
        health = maxhealth;
    }
    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if(direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
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

        CreateDamage(damageAmount.ToString(), 0);

        if (health <= 0)
        {
            StartCoroutine(DieElaborately());
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
        damagenum.transform.position = new Vector3(transform.position.x,transform.position.y + 1,0);
        damagenum.GetComponent<Damage>().Init(damageNumstr,type);

    }
    /*
    void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);

        //GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(gameObject);
    }
    */

    IEnumerator DieElaborately()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("EnemyDeath").GetComponent<AudioSource>().Play();

        Color c = GetComponent<SpriteRenderer>().color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.02f);
        }

        //GetComponent<LootBag>().InstantiateLoot(transform.position);
        Instantiate(EXPPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
