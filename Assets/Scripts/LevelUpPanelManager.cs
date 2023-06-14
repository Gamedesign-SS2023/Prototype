using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpPanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Pausemanager pausemanager;
    [SerializeField] List<UpgradeButton> upgradebuttons;
    //Player player;
    public List<PowerUps> powerUps;
    public GameObject optionPrefab;

    private void Awake()
    {
        pausemanager = GetComponent<Pausemanager>();
        //player = GetComponent<Player>();
    }
    /*
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pausemanager.PauseGame();
        panel.SetActive(true);
        for (int i = 0; i< upgradeDatas.Count;i++)
        {
            upgradebuttons[i].gameObject.SetActive(true);
            upgradebuttons[i].Set(upgradeDatas[i]);
        }
    }
    */

    public List<PowerUps> Shuffle(List<PowerUps> listToShuffle)
    {
        var rnd = new System.Random();
        var shuffledList = listToShuffle.OrderBy(_ => rnd.Next()).ToList();
        return shuffledList;
    }

    public void OpenPanel()
    {
        pausemanager.PauseGame();
        panel.SetActive(true);

        Shuffle(powerUps);
        for (int i = 0; i < upgradebuttons.Count; i++)
        {
            if(powerUps[i] != null)
            {
                upgradebuttons[i].gameObject.SetActive(true);
                upgradebuttons[i].Set(powerUps[i]);
            }
        }
    }
    /*
    public void Clean()
    {
        for (int i = 0; i < upgradebuttons.Count; i++)
        {
            upgradebuttons[i].Clean();
        }
    }
    */
  
    public void Select(int pressed)
    {
        Debug.Log(powerUps[pressed].upgradeName);

        int lvl = 0;
        GameObject weaponObject;

        switch (powerUps[pressed].upgradeName)
        {
            case "HP":
                GameObject.Find("Buffs").GetComponent<Buffs>().buffHP++;
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffHP;
                break;
            case "Geschwindigkeit":
                GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed++;
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffSpeed;
                break;
            case "kritischer Schaden":
                GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance++;
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffCritChance;
                break;
            case "Basisschaden":
                GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage++;
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffBaseDamage;
                break;
            case "Erfahrungsgewinn":
                GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain++;
                lvl = GameObject.Find("Buffs").GetComponent<Buffs>().buffXPGain;
                break;
            case "Schleim":
                GameObject.Find("Player").GetComponent<Attack>().weaponLVL++;
                lvl = GameObject.Find("Player").GetComponent<Attack>().weaponLVL;
                break;
            case "Niedlichkeit":

                Niedlichkeit_Attack cuteness = GameObject.Find("wpn_cuteness").GetComponent<Niedlichkeit_Attack>();
                if(cuteness.unlocked)
                {
                    cuteness.weaponLVL++;
                    lvl = cuteness.weaponLVL;
                } else
                {
                    cuteness.unlock();
                }
                
                //Debug.Log(GameObject.Find("wpn_cuteness"));
                break;
            case "Messer und Gabel":

                messer_gabel_attack knifefork = GameObject.Find("wpn_knifefork").GetComponent<messer_gabel_attack>();
                if (knifefork.unlocked)
                {
                    knifefork.weaponLVL++;
                    lvl = knifefork.weaponLVL;
                }
                else
                {
                    knifefork.unlock();
                }
                
                //GameObject.Find("wpn_knifefork").SetActive(true);
                break;
        }

        if (lvl == 3)
        {
            powerUps.RemoveAt(pressed);
        }

        pausemanager.UnPauseGame();
        panel.SetActive(false);
    }

    /*
    public void ClosePanel()
    {
        for (int i = 0; i < upgradebuttons.Count; i++)
        {
            upgradebuttons[i].gameObject.SetActive(false);
        }
        pausemanager.UnPauseGame();
        panel.SetActive(false);
    }
    */
}
