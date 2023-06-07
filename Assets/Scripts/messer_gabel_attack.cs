using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messer_gabel_attack : WeaponBase
{
    [SerializeField] private AudioSource throwingsound;
    [SerializeField] float spread = 0.5f;
    [SerializeField] GameObject messerGabelPrefab;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Attack()
    {

        for (int i = 0; i < stats.numberOfAttacks; i++)
        {
            GameObject throwingMesser = Instantiate(messerGabelPrefab);
            Vector3 Newknifepos = transform.position;
            if (stats.numberOfAttacks > 1)
            {
                Newknifepos.y -= (spread * (stats.numberOfAttacks-1)) / 2;
                Newknifepos.y += i * spread;
            }
            

            throwingMesser.transform.position = Newknifepos;
            throwingMesserProjectile throwingMesserProjectile = throwingMesser.GetComponent<throwingMesserProjectile>();
            throwingsound.Play();
            throwingMesserProjectile.SetDirection(player.lastHorizontalVector, 0f);
            throwingMesserProjectile.damage = stats.damage;
        }
        
    }
}
