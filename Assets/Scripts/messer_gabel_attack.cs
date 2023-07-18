using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messer_gabel_attack : WeaponBase
{
    [SerializeField] private AudioSource throwingsound;
    [SerializeField] GameObject messerGabelPrefab;
    Player player;
    public int weaponLVL;
    public float speed;
    float direction = 0f;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Attack()
    {
        if(player.GetComponent<Rigidbody2D>().velocity.x != 0f)
        {
            direction = player.GetComponent<Rigidbody2D>().velocity.x;
        }

        //throwingsound.Play();
        throwingsound.PlayOneShot(throwingsound.clip);

        if (weaponLVL == 3)
        {
            shootProjectile(false);
            shootProjectile(true);
        } else
        {
            if (direction < 0)
            {
                shootProjectile(false);
            } else
            {
                shootProjectile(true);
            }
        }
    }

    void shootProjectile(bool right)
    {
        GameObject projectile = Instantiate(messerGabelPrefab, transform.position, transform.rotation);
        projectile.transform.parent = transform;
        projectile.GetComponent<throwingMesserProjectile>().weaponLVL = weaponLVL;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (!right)
        {
            projectile.GetComponent<SpriteRenderer>().flipX = true;
            rb.velocity = -transform.right * speed;
        }
        else
        {
            projectile.GetComponent<SpriteRenderer>().flipX = false;
            rb.velocity = transform.right * speed;
        }
    }
}
