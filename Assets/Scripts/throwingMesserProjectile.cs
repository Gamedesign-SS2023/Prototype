using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class throwingMesserProjectile : MonoBehaviour
{
    [SerializeField] public float damage;
    int hits;
    public int weaponLVL;
    private AudioSource hitAudio;

    private void Start()
    {
        hitAudio = GameObject.Find("KnifeForkHit").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //GameObject.Find("KnifeForkHit").GetComponent<AudioSource>().Play();
            hitAudio.PlayOneShot(hitAudio.clip);
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (weaponLVL >= 2)
            {
                enemy.TakeDamage(damage + 10,2);
            }
            else
            {
                enemy.TakeDamage(damage,2);
            }

            hits = (weaponLVL >= 1) ? hits + 1 : hits + 2;
            if (hits == 2)
            {
                Destroy(gameObject);
            }
        }
        if (collider.gameObject.tag == "Fence")
        {
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Barrel")
        {
            Destroy(collider.gameObject);
        }
    }
}
