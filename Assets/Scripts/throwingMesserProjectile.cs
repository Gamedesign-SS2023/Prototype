using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwingMesserProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField ]float speed;
    [SerializeField] public int damage;

    public void SetDirection(float x,float y)
    {
        direction = new Vector3(x,y);
        if (x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
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
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
