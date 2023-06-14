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
    public int weaponLVL;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Attack()
    {
        Debug.Log("hi");
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
        
    }
}
