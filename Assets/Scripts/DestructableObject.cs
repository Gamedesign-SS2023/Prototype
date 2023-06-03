using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, Damageable
{
    public void TakeDamage(int amount)
    {
        Destroy(gameObject);
    }
}
