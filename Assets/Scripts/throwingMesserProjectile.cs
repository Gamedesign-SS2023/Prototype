using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class throwingMesserProjectile : MonoBehaviour
{
    //Vector3 direction;
    //[SerializeField] float speed;
    [SerializeField] public float damage;
    //public float timeToLive = 3;
    int hits;
    public int weaponLVL;

    /*
    public void SetDirection(float x,float y)
    {
        if (x < 0)
        {
            //Vector3 scale = transform.localScale;
            //scale.x = scale.x * -1;
            //transform.localScale = scale;
            x = -1f;
        }else if(x > 0){
            x = 1f;
        }

        if (y < 0)
        {
            y = -1f;
        }else if(y > 0){
            y = 1f;
        }

        direction = new Vector3(x,y);

    }

    private void Update()
    {

        transform.position += direction * speed*Time.deltaTime;
        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.7f);

            foreach (Collider2D c in hit)
            {
                Damageable d = c.GetComponent<Damageable>();
                if (d != null)
                { 
                    d.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
        timeToLive -= Time.deltaTime;
        if(timeToLive < 0)
        {
            Destroy(gameObject);
        }

    }
*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            GameObject.Find("KnifeForkHit").GetComponent<AudioSource>().Play();
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (weaponLVL >= 2)
            {
                enemy.TakeDamage(damage + 10);
            }
            else
            {
                enemy.TakeDamage(damage);
            }
            //enemy.CreateDamage(damage.ToString(), 0);

            hits = (weaponLVL >= 1) ? hits + 1 : hits + 2;
            if (hits == 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
