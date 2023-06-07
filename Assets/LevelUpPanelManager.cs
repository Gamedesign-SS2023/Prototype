using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Pausemanager pausemanager;
    [SerializeField] List<UpgradeButton> upgradebuttons;
    Player player;

    private void Awake()
    {
        pausemanager = GetComponent<Pausemanager>();
        player = GetComponent<Player>();
    }

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

    public void Clean()
    {
        for (int i = 0; i < upgradebuttons.Count; i++)
        {
            upgradebuttons[i].Clean();
        }
    }

    public void Upgrade(int pressedbutton)
    {
       GameManager.instance.playerTransform.GetComponent<Player>().Upgrade(pressedbutton);
        ClosePanel();
    }

    public void ClosePanel()
    {
        for (int i = 0; i < upgradebuttons.Count; i++)
        {
            upgradebuttons[i].gameObject.SetActive(false);
        }
        pausemanager.UnPauseGame();
        panel.SetActive(false);
    }
}
