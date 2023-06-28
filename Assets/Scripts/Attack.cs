using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapon;
    //public float weaponSpeed;
    public float cooldown;
    [SerializeField] private AudioSource shootingSound;
    public int weaponLVL;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("autoAttack", 0, cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (timer < timetoattack)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        autoAttack();
        */
    }

    void autoAttack()
    {
        // calculate closest enemy to attack
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return; //don't attack if there are no enemies around

        List<GameObject> closestEnemy = new List<GameObject>();
        closestEnemy.Add(enemies[0]);

        for (int i = 1; i < enemies.Length; i++)
        {
            //don't target enemies that are out of bounds / outside the fence
            if(enemies[i].transform.position.x < 50f && enemies[i].transform.position.x > -50f
                && enemies[i].transform.position.y < 50f && enemies[i].transform.position.y > -50f)
            {
                float distanceNew = Vector2.Distance(enemies[i].transform.position, transform.position);
                float distanceOld = Vector2.Distance(closestEnemy[0].transform.position, transform.position);
                if (distanceNew < distanceOld) closestEnemy.Insert(0, enemies[i]);
            }
        }

        //shootingSound.Play();
        shootingSound.PlayOneShot(shootingSound.clip);

        //shoot n projectiles per lvl
        for (int i = 0; i<=weaponLVL;i++)
        {
            if(closestEnemy.Count > weaponLVL)
            {
                shootProjectile(closestEnemy[i]);
            }
        }
    }

    void shootProjectile(GameObject closestEnemy)
    {
        GameObject projectile = Instantiate(weapon, transform.position, transform.rotation);
        projectile.GetComponent<Weapon>().weaponLVL = weaponLVL;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (closestEnemy.transform.position - transform.position);
        //rb.velocity = new Vector2(direction.x, direction.y) * weaponSpeed;
        rb.velocity = new Vector2(direction.x, direction.y);
    }
}
