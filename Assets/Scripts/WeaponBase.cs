using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public float timer;
    public bool unlocked = false;
    public float cooldown;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void unlock()
    {
        unlocked = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Update()
    {
        //InvokeRepeating("Attack", 0, cooldown);
        
        if(unlocked)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                Attack();
                timer = cooldown;
            }
        }
    }

    public abstract void Attack();
    
}
