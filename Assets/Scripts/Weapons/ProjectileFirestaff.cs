using System.Collections;
using UnityEngine;

public class ProjectileFirestaff : MonoBehaviour
{
    public float damage;
    public string weaponType;
    public int weaponLVL;
    public Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 1.5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                if (weaponLVL == 3)
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage + 20, 2);
                }
                else
                {
                    colliders[i].GetComponent<Enemy>().TakeDamage(damage, 2);
                }
            }
            if (colliders[i].gameObject.tag == "Barrel")
            {
                Destroy(colliders[i].gameObject);
            }
        }

        StartCoroutine("alive");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator alive()
    {
        yield return new WaitForSeconds(2f);

        Color c = GetComponent<SpriteRenderer>().color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.02f);
        }

        Destroy(gameObject);
    }

    /*
    //for any enemies that walk on the area during its ttl
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //GameObject.Find("SlimeHit").GetComponent<AudioSource>().Play();
            //hitAudio.PlayOneShot(hitAudio.clip);

            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (weaponLVL >= 3)
            {
                enemy.TakeDamage(damage + 20, 2);
            }
            else
            {
                enemy.TakeDamage(damage, 2);
            }
        }
    }
    */
}
