    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject Drop;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Weapon")
        {
            Transform t = Instantiate(Drop).transform;
            t.position = transform.position;
            Destroy(gameObject);
        }
    }
}
