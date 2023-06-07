using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public float timer;
    public WeaponStats stats;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = stats.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        WeaponData = wd;
        stats = new WeaponStats(wd.stats.damage,wd.stats.timeToAttack,wd.stats.numberOfAttacks);
    }
    public abstract void Attack();

    public void Upgrade(UpgradeData upgradeData)
    {
        stats.Sum(upgradeData.WeaponUpgradeStats);
    }
}
