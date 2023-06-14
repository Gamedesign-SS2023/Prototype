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
    public TextMeshProUGUI upgradeDesc;
    public TextMeshProUGUI upgradeLVL;

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

        if(upgradeData.type == "buff")
        {
            upgradeDesc.text = upgradeData.description;
        }
        if(upgradeData.type == "weapon")
        {
            int lvl = 0;

            switch (upgradeData.upgradeName)
            {
                case "Schleim":
                    lvl = GameObject.Find("Player").GetComponent<Attack>().weaponLVL+1;
                    break;
                case "Niedlichkeit":
                    lvl = GameObject.Find("wpn_cuteness").GetComponent<Niedlichkeit_Attack>().weaponLVL;
                    break;
                case "Messer und Gabel":
                    lvl = GameObject.Find("wpn_knifefork").GetComponent<messer_gabel_attack>().weaponLVL;
                    break;
            }
            
            switch(lvl)
            {
                case 0:
                    upgradeDesc.text = upgradeData.description;
                    break;
                case 1:
                    upgradeDesc.text = upgradeData.descLVL1;
                    break;
                case 2:
                    upgradeDesc.text = upgradeData.descLVL2;
                    break;
                case 3:
                    upgradeDesc.text = upgradeData.descLVL3;
                    break;
            }

            if(lvl == 0)
            {
                upgradeLVL.text = "Unlock";
            } else
            {
                upgradeLVL.text = "LVL"+lvl.ToString();
            }
        }
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
