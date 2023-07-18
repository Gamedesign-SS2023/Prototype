using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int healamount;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "HpDrop") {
            Player p = collision.GetComponent<Player>();
            if (p != null)
            {
                p.Heal(healamount);
                Destroy(gameObject);
            }
        }
    }

}
