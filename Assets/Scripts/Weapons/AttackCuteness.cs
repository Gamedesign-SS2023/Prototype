using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCuteness : WeaponBase
{
    //float radius = 1f;
    [SerializeField] GameObject prefab;
    public int weaponLVL;
    public float damage;
    private AudioSource hitAudio;

    private void Start()
    {
        //Destroy(prefab, 0);
        hitAudio = GameObject.Find("CutenessHit").GetComponent<AudioSource>();
    }

    public void unlock()
    {
        unlocked = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Attack()
    {
        GameObject projectile = Instantiate(prefab, transform.position, transform.rotation);
        projectile.transform.SetParent(gameObject.transform);

        float radius = (weaponLVL >= 1) ? 2f : 1f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                //GameObject.Find("CutenessHit").GetComponent<AudioSource>().Play();
                hitAudio.PlayOneShot(hitAudio.clip);
                if (weaponLVL == 3)
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage + 1,1);
                }
                else
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage,1);
                }
            }
            if (colliders[i].gameObject.tag == "Barrel")
            {
                Destroy(colliders[i].gameObject);
            }
        }

        StartCoroutine(alive(projectile));
    }

    IEnumerator alive(GameObject projectile)
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(projectile);
    }
}
