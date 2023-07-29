using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFirestaff : WeaponBase
{
    public AudioSource summonSound;
    public GameObject prefab;
    public int weaponLVL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Attack()
    {
        summonSound.PlayOneShot(summonSound.clip);

        //shoot n projectiles per lvl
        for (int n = 0; n <= weaponLVL; n++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            GameObject projectile = Instantiate(prefab, spawnPos, Quaternion.identity);
            //projectile.transform.SetParent(gameObject.transform);
            projectile.GetComponent<ProjectileFirestaff>().weaponLVL = weaponLVL;
            projectile.GetComponent<ProjectileFirestaff>().spawnPos = spawnPos;
        }
    }

}
