using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    public TextMeshProUGUI upgradeName;

    /*
    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
    }
    */
    public void Set(PowerUps upgradeData)
    {
        icon.sprite = upgradeData.icon;
        upgradeName.text = upgradeData.upgradeName;
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
