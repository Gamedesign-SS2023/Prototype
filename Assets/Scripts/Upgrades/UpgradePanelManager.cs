using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Pausemanager pausemanager;
    [SerializeField] List<UpgradeButton> upgradebuttons;
    public List<UpgradeData> upgrades;
    public GameObject optionPrefab;
    public AudioSource popUpSound;
    public AudioSource clickSound;
    public GameObject empty;

    private void Awake()
    {
        pausemanager = GetComponent<Pausemanager>();
    }

    public List<UpgradeData> Shuffle(List<UpgradeData> listToShuffle)
    {
        var rnd = new System.Random();
        var shuffledList = listToShuffle.OrderBy(_ => rnd.Next()).ToList();
        return shuffledList;
    }

    public void OpenPanel()
    {
        //popUpSound.Play();
        popUpSound.PlayOneShot(popUpSound.clip);

        pausemanager.PauseGame();
        panel.SetActive(true);

        if(upgrades.Count == 0)
        {
            upgradebuttons[0].gameObject.SetActive(false);
            empty.SetActive(true);
        } else
        {
            if(upgrades.Count > 1)
            {
                upgrades = Shuffle(upgrades);
            }

            for (int i = 0; i < upgradebuttons.Count; i++)
            {
                if (i < upgrades.Count)
                {
                    upgradebuttons[i].gameObject.SetActive(true);
                    upgradebuttons[i].Set(upgrades[i]);
                }
                else
                {
                    upgradebuttons[i].gameObject.SetActive(false);
                }
            }
        }
    }
  
    public void Select(int pressed)
    {
        //clickSound.Play();
        clickSound.PlayOneShot(clickSound.clip);

        int lvl = 0;

        if(pressed == 4)
        {
            GameObject.Find("Player").GetComponent<Player>().Heal(20f);
        } else
        {
            switch (upgrades[pressed].id)
            {
                case "hp":
                    GameObject.Find("Buffs").GetComponent<Buffs>().buffHP++;
                    lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffHP;

                    GameObject.Find("Player").GetComponent<Player>().maxhp *= (1.1f * lvl);

                    break;

                case "speed":
                    GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed++;
                    lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed;

                    GameObject.Find("Player").GetComponent<Player>().moveSpeed += lvl * 2;

                    break;

                case "critchance":
                    GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance++;
                    lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance;
                    break;

                case "basedamage":
                    GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage++;
                    lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage;
                    break;

                case "xpgain":
                    GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain++;
                    lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain;
                    break;

                case "slime":
                    GameObject.Find("wpn_slime").GetComponent<AttackSlime>().weaponLVL++;
                    lvl = GameObject.Find("wpn_slime").GetComponent<AttackSlime>().weaponLVL;

                    if (lvl == 1)
                    {
                        GameObject.Find("wpn_slime").GetComponent<AttackSlime>().cooldown -= 0.2f;
                    }

                    break;

                case "cuteness":

                    AttackCuteness cuteness = GameObject.Find("wpn_cuteness").GetComponent<AttackCuteness>();

                    if (cuteness.unlocked)
                    {
                        cuteness.weaponLVL++;
                        lvl = cuteness.weaponLVL;
                    }
                    else
                    {
                        cuteness.unlock();
                    }

                    if (lvl == 1)
                    {
                        cuteness.transform.localScale += new Vector3(1.5f, 1.5f, 0);
                    }

                    if (lvl == 2)
                    {
                        GameObject.Find("wpn_cuteness").GetComponent<AttackCuteness>().cooldown -= 0.3f;
                    }

                    break;

                case "knifefork":

                    AttackKnifeFork knifefork = GameObject.Find("wpn_knifefork").GetComponent<AttackKnifeFork>();
                    if (knifefork.unlocked)
                    {
                        knifefork.weaponLVL++;
                        lvl = knifefork.weaponLVL;
                    }
                    else
                    {
                        knifefork.unlock();
                    }
                    break;
            }
        }

        if (lvl == 3)
        {
            upgrades.RemoveAt(pressed);
        }

        pausemanager.UnPauseGame();
        panel.SetActive(false);
    }
}
