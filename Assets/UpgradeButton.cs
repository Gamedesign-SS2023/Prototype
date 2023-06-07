using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
