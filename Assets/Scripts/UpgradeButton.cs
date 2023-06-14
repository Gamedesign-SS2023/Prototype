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

        int lvl = 0;

        switch (upgradeData.upgradeName)
        {
            case "HP":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffHP;
                break;

            case "Geschwindigkeit":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed;
                break;

            case "kritischer Schaden":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance;
                break;

            case "Basisschaden":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage;
                break;

            case "Erfahrungsgewinn":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain;
                break;
            case "Schleim":
                lvl = GameObject.Find("Player").GetComponent<Attack>().weaponLVL;
                break;
            case "Niedlichkeit":
                if(!GameObject.Find("wpn_cuteness").GetComponent<Niedlichkeit_Attack>().unlocked)
                {
                    lvl = 4;
                } else
                {
                    lvl = GameObject.Find("wpn_cuteness").GetComponent<Niedlichkeit_Attack>().weaponLVL;
                }
                break;
            case "Messer und Gabel":
                if (!GameObject.Find("wpn_knifefork").GetComponent<messer_gabel_attack>().unlocked)
                {
                    lvl = 4;
                }
                else
                {
                    lvl = GameObject.Find("wpn_knifefork").GetComponent<messer_gabel_attack>().weaponLVL;
                }
                break;
        }

        if (upgradeData.type == "buff")
        {
            upgradeDesc.text = upgradeData.description;
        }

        if (upgradeData.type == "weapon")
        {
            switch (lvl)
            {
                case 0:
                    upgradeDesc.text = upgradeData.descLVL1;
                    break;
                case 1:
                    upgradeDesc.text = upgradeData.descLVL2;
                    break;
                case 2:
                    upgradeDesc.text = upgradeData.descLVL3;
                    break;
                case 4:
                    upgradeDesc.text = upgradeData.description;
                    break;
            }
        }

        if (lvl == 4)
        {
            upgradeLVL.text = "Unlock";
        }
        else
        {
            upgradeLVL.text = "LVL" + (lvl + 1).ToString();
        }
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
