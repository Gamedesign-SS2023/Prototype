using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Enemy : MonoBehaviour //, Damageable
{
    // public static event Action<Enemy> OnEnemyKilled;
    //Attribute vom Enemy
    [SerializeField] float health;
    [SerializeField] float maxhealth;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;

    GameObject player;

    public GameObject damagePre;
    public GameObject EXPPrefab;
    public GameObject EXPGenocide;
    public GameObject EXPPacifist;

    private AudioSource deathAudio;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        maxhealth = player.GetComponent<Player>().level * maxhealth;
        health = maxhealth;

        deathAudio = GameObject.Find("EnemyDeath").GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if(gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
        } else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f,0f,0f);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            /*
            if (player==null)
            {
                player=targetObject.GetComponent<Player>();
            }
            */
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void TakeDamage(float damageAmount, int type)
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

        CreateDamage(damageAmount.ToString(), type);

        if (health <= 0)
        {
            StartCoroutine(DieElaborately(type));
        }

    }

    public void CreateDamage(string damageNumstr,int type)
    {
        GameObject damagenum = ObjectPool.Instance.Get(damagePre);
        damagenum.transform.position = new Vector3(transform.position.x,transform.position.y + 1,0);
        damagenum.GetComponent<Damage>().Init(damageNumstr,type);

    }

    IEnumerator DieElaborately(int type)
    {
        transform.gameObject.tag = "Untagged"; //stop including in weapon's targeting

        Color death = Color.red;
        switch (type)
        {
            case 1:
                death = Color.green;
                break;
            case 2:
                death = Color.red;
                break;
            default:
                death = Color.white;
                break;
        }

        GetComponent<SpriteRenderer>().color = death;
        yield return new WaitForSeconds(0.2f);

        //GameObject.Find("EnemyDeath").GetComponent<AudioSource>().Play();
        deathAudio.PlayOneShot(deathAudio.clip);

        Color c = GetComponent<SpriteRenderer>().color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.02f);
        }

        switch (type)
        {
            case 1:
                Instantiate(EXPPacifist, transform.position, Quaternion.identity);
                player.GetComponent<Player>().Heal(0.25f);
                break;
            case 2:
                Instantiate(EXPGenocide, transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(EXPPrefab, transform.position, Quaternion.identity);
                break;
        }

        Destroy(gameObject);
    }
}
