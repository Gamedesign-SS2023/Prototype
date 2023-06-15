using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messer_gabel_attack : WeaponBase
{
    [SerializeField] private AudioSource throwingsound;
    //[SerializeField] float spread = 0.5f;
    [SerializeField] GameObject messerGabelPrefab;
    Player player;
    public int weaponLVL;
    public float speed;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Attack()
    {
        /*
        for (int i = 0; i < numberOfAttacks; i++)
        {
            GameObject throwingMesser = Instantiate(messerGabelPrefab);
            Vector3 Newknifepos = transform.position;

            if (numberOfAttacks > 1)
            {
                Newknifepos.y -= (spread * (numberOfAttacks-1)) / 2;
                Newknifepos.y += i * spread;
            }
            
            throwingMesser.transform.position = Newknifepos;
            throwingMesserProjectile throwingMesserProjectile = throwingMesser.GetComponent<throwingMesserProjectile>();
            throwingsound.Play();
            throwingMesserProjectile.SetDirection(player.lastHorizontalCoupledVector, player.lastVertictalCoupledVector);
            throwingMesserProjectile.damage = damage;
        }
        */

        throwingsound.Play();

        GameObject projectile = Instantiate(messerGabelPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if(player.lastHorizontalCoupledVector < 0)
        {
            projectile.GetComponent<SpriteRenderer>().flipX = true;
            rb.velocity = -transform.right * speed;
        } else
        {
            projectile.GetComponent<SpriteRenderer>().flipX = false;
            rb.velocity = transform.right * speed;
        }
    }
}
