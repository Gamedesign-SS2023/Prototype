using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;
    [SerializeField] WeaponData startingweapon;
    List<WeaponBase> weapons;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
    }
    private void Start()
    {
        AddWeapon(startingweapon);
    }

    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab,weaponObjectContainer);
        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);
        Player player=GetComponent<Player>();
        if (player != null)
        {
            player.AddUpgradesIntoListOfAvailableUpgrades(weaponData.upgrades);
        }
    }

    public void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.WeaponData == upgradeData.WeaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
