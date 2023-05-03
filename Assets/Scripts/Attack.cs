using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapon;
    public float weaponSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("autoAttack", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void autoAttack()
    {
        // calculate closest enemy to attack
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return; //don't attack if there are no enemies around

        GameObject closestEnemy = enemies[0];
        for (int i = 1; i < enemies.Length; i++)
        {
            float distanceNew = Vector2.Distance(enemies[i].transform.position, transform.position);
            float distanceOld = Vector2.Distance(closestEnemy.transform.position, transform.position);
            if (distanceNew < distanceOld) closestEnemy = enemies[i];
        }

        // shoot projectile
        GameObject projectile = Instantiate(weapon, transform.position, transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * weaponSpeed;
    }
}
