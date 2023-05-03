using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   // public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float health, maxhealth, moveSpeed;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    GameObject loot;


    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxhealth;
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;

        }
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Damage Amount: {damageAmount}");
        health -= damageAmount;
        Debug.Log($"Health is now: {health}");
        if (health <= 0)
        {
            Die();
        }
        
    }

    void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Die();
        }

    }
}
