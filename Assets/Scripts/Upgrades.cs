using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Upgrades : ScriptableObject
{
    public string id;
    public string type;
    public string upgradeName;
    public Sprite icon;
    public string description;
    public string descLVL1;
    public string descLVL2;
    public string descLVL3;
}

