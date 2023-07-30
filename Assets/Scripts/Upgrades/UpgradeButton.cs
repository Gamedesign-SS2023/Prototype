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

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        upgradeName.text = upgradeData.upgradeName;

        int lvl = 0;

        switch (upgradeData.id)
        {
            case "hp":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffHP;
                break;

            case "speed":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed;
                break;

            case "critchance":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance;
                break;

            case "base damage":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage;
                break;

            case "xpgain":
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain;
                break;
            case "slime":
                lvl = GameObject.Find("wpn_slime").GetComponent<AttackSlime>().weaponLVL;
                break;
            case "cuteness":
                if(!GameObject.Find("wpn_cuteness").GetComponent<AttackCuteness>().unlocked)
                {
                    lvl = 4;
                } else
                {
                    lvl = GameObject.Find("wpn_cuteness").GetComponent<AttackCuteness>().weaponLVL;
                }
                break;
            case "knifefork":
                if (!GameObject.Find("wpn_knifefork").GetComponent<AttackKnifeFork>().unlocked)
                {
                    lvl = 4;
                }
                else
                {
                    lvl = GameObject.Find("wpn_knifefork").GetComponent<AttackKnifeFork>().weaponLVL;
                }
                break;
            case "firestaff":
                if (!GameObject.Find("wpn_firestaff").GetComponent<AttackFirestaff>().unlocked)
                {
                    lvl = 4;
                }
                else
                {
                    lvl = GameObject.Find("wpn_firestaff").GetComponent<AttackFirestaff>().weaponLVL;
                }
                break;
            case "lullaby":
                if (!GameObject.Find("wpn_lullaby").GetComponent<AttackLullaby>().unlocked)
                {
                    lvl = 4;
                }
                else
                {
                    lvl = GameObject.Find("wpn_lullaby").GetComponent<AttackLullaby>().weaponLVL;
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
