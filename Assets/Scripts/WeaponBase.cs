using System;
using UnityEngine;

/*
public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}
*/

public abstract class WeaponBase : MonoBehaviour
{
    Player playerMove;

    //public WeaponData WeaponData;
    public float timer;
    //public float cooldown;
    //public WeaponStats stats;
    //public Vector2 vectorofattack;
    //[SerializeField] DirectionOfAttack attackDirection;
    public bool unlocked = false;

    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;

    private void Awake()
    {
        playerMove = GetComponent<Player>();
    }

    private void Start()
    {
        //InvokeRepeating("Attack", 0, cooldown);
    }

    public void unlock()
    {
        unlocked = true;
    }

    public void Update()
    {
        //InvokeRepeating("Attack", 0, cooldown);
        
        if(unlocked)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                Attack();
                timer = timeToAttack;
            }
        }
    }

    /*
    public void ApplyDamage(Collider2D[] colliders)
    {
        //int damage = GetDamage();
        for(int i = 0; i < colliders.Length; i++)
        {
            Damageable d = colliders[i].GetComponent<Damageable>();
            if(d != null)
            {
                d.TakeDamage(stats.damage);
            }
        }
    }
    */

    /*
    public virtual void SetData(WeaponData wd)
    {
        WeaponData = wd;
        stats = new WeaponStats(wd.stats.damage,wd.stats.timeToAttack,wd.stats.numberOfAttacks);
    }
    */
    public abstract void Attack();
    /*
    public void Upgrade(UpgradeData upgradeData)
    {
        stats.Sum(upgradeData.WeaponUpgradeStats);
    }
    */
    /*
    public void UpdateVectorOfAttack()
    {
        if(attackDirection == DirectionOfAttack.None)
        {
            vectorofattack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorofattack.x = playerMove.lastHorizontalCoupledVector;
                vectorofattack.y = playerMove.lastVertictalCoupledVector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorofattack.x = playerMove.lastHorizontalDeCoupledVector;
                vectorofattack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorofattack.x = 0f;
                vectorofattack.y = playerMove.lastVertictalDeCoupledVector;
                break;
        }
        vectorofattack = vectorofattack.normalized;

    }
    */
    
}
