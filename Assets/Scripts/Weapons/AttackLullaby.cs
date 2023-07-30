using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLullaby : WeaponBase
{
    [SerializeField] private AudioSource throwingsound;
    [SerializeField] GameObject lullabyPrefab;
    public int weaponLVL;
    public float speed;
    Vector2 dir;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Attack()
    {
        throwingsound.PlayOneShot(throwingsound.clip);
        if (weaponLVL >=2)
        {
            shootProjectile(false);
            shootProjectile(true);
        }
        else
        {
            if (player.lastHorizontalVector < 0)
            {
                shootProjectile(false);
            }
            else
            {
                shootProjectile(true);
            }
        }

    }

    void shootProjectile(bool left)
    {
        GameObject projectile = Instantiate(lullabyPrefab, transform.position, transform.rotation);
        projectile.transform.SetParent(gameObject.transform);
        projectile.GetComponent<ProjectileLullaby>().weaponLVL = weaponLVL;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        projectile.GetComponent<SpriteRenderer>().flipX = !left;
        int horizontal = left ? (1) : -1;
        rb.velocity = horizontal * transform.right * speed;

    }
}
