using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    Player p;

    private void Awake()
    {
        p=GetComponent<Player>();
    }

    public void Equip(Item itemtoequip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }
        items.Add(itemtoequip);
        itemtoequip.Equip(p);
    }
}
