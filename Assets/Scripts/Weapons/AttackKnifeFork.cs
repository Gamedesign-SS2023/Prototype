using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKnifeFork : WeaponBase
{
    [SerializeField] private AudioSource throwingsound;
    [SerializeField] GameObject messerGabelPrefab;
    public int weaponLVL;
    public float speed;

    private void Awake()
    {
    }

    public override void Attack()
    {
        //throwingsound.Play();
        throwingsound.PlayOneShot(throwingsound.clip);

        if (weaponLVL == 3)
        {
            shootProjectile(false);
            shootProjectile(true);
        } else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                shootProjectile(false);
            } else
            {
                shootProjectile(true);
            }
        }
    }

    void shootProjectile(bool left)
    {
        GameObject projectile = Instantiate(messerGabelPrefab, transform.position, transform.rotation);
        projectile.transform.SetParent(gameObject.transform);
        projectile.GetComponent<ProjectileKnifeFork>().weaponLVL = weaponLVL;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        projectile.GetComponent<SpriteRenderer>().flipX = left;
        int horizontal = left ? (-1) : 1;
        rb.velocity = horizontal * transform.right * speed;
    }
}
