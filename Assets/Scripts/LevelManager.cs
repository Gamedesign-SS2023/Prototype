using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Timer")]
    public int setLevel;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    [Tooltip("Input needs to be in SECONDS")]
    public int levelEnd;

    [Header("Enemies")]
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject enemyDPrefab;
    [Tooltip("GameObject the enemies are supposed to spawn in")]
    public GameObject enemiesParent;

    [Header("EXP")]
    public Slider expSlider;
    public TextMeshProUGUI level;

    [Header("Barrels")]
    public bool barrelSpawnActive = true;
    public GameObject barrelPrefab;
    [Tooltip("GameObject the barrels are supposed to spawn in")]
    public GameObject barrelParent;

    [Header("Kills")]
    public TextMeshProUGUI kills;
    public int killsValue = 0;

    [Header("Enmity - Stamina")]
    [Tooltip("What route does the current run qualify as?")]
    public int req = 0;
    [Tooltip("currently chosen route")]
    public int active = 0;
    [Tooltip("Already acquired enmity route?")]
    public bool enmity = false;
    [Tooltip("Already acquired stamina route?")]
    public bool stamina = false;

    private float timeStamp = 0;
    private bool gameOver;

    void Start()
    {
        if (barrelSpawnActive)
        {
            InvokeRepeating("barrelSpawn", 0, 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeStamp += Time.deltaTime;

        setTimer(timeStamp);

        switch(setLevel)
        {
            case 1:
                setLevel1(timeStamp);
                break;
            default:
                setLevel0(timeStamp);
                break;
        }

        if (timeStamp >= levelEnd)
        {
            if (!gameOver)
            {
                gameOver = true;
                GetComponent<GameOver>().GameOverPanel(false);
            }
            
        }
    }

    void setTimer(float timeStamp)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeStamp);
        timerText.text = time.ToString(@"mm\:ss");
    }

    void setLevel0(float timeStamp)
    {
        if (timeStamp >= 0 && timeStamp < 60) enemyWave(enemyAPrefab, 10);
        if (timeStamp >= 60 && timeStamp < 120) enemyWave(enemyAPrefab, 50);
        if (timeStamp >= 120 && timeStamp < 180) enemyWave(enemyAPrefab, 100);
    }

    void setLevel1(float timeStamp)
    {
        if (timeStamp >= 0 && timeStamp < 10) enemyWave(enemyAPrefab, 5);
        if (timeStamp >= 10 && timeStamp < 30) enemyWave(enemyAPrefab, 20);
        if (timeStamp >= 30 && timeStamp < 45)
        {
            enemyWave(enemyAPrefab, 20);
            enemyWave(enemyBPrefab, 10);
        }
        if (timeStamp >= 45 && timeStamp < 60)
        {
            enemyWave(enemyAPrefab, 5);
            enemyWave(enemyBPrefab, 25);
        }
        if (timeStamp >= 60 && timeStamp < 80)
        {
            enemyWave(enemyAPrefab, 20);
            enemyWave(enemyBPrefab, 10);
        }
        if (timeStamp >= 80 && timeStamp < 120) enemyWave(enemyAPrefab, 25);
        if (timeStamp >= 120 && timeStamp < 150) enemyWave(enemyCPrefab, 25);
        if (timeStamp >= 150 && timeStamp < 180)
        {
            enemyWave(enemyCPrefab, 20);
            enemyWave(enemyBPrefab, 30);
            enemyWave(enemyAPrefab, 10);
        }
        if (timeStamp >= 180 && timeStamp < 240)
        {
            enemyWave(enemyBPrefab, 5);
            enemyWave(enemyAPrefab, 15);
        }
        if (timeStamp >= 240 && timeStamp < 300)
        {
            enemyWave(enemyDPrefab, 15);
            enemyWave(enemyBPrefab, 15);
        }
        if (timeStamp >= 300 && timeStamp < 330)
        {
            enemyWave(enemyDPrefab, 30);
            enemyWave(enemyAPrefab, 20);
        }
        if (timeStamp >= 330 && timeStamp < 360)
        {
            enemyWave(enemyDPrefab, 10);
            enemyWave(enemyBPrefab, 20);
            enemyWave(enemyAPrefab, 10);
        }
        if (timeStamp >= 360 && timeStamp < 390)
        {
            enemyWave(enemyDPrefab, 15);
            enemyWave(enemyBPrefab, 30);
        }
        if (timeStamp >= 390 && timeStamp < 420)
        {
            enemyWave(enemyBPrefab, 20);
            enemyWave(enemyCPrefab, 20);
        }
        if (timeStamp >= 420 && timeStamp < 450)
        {
            enemyWave(enemyBPrefab, 10);
            enemyWave(enemyCPrefab, 15);
            enemyWave(enemyAPrefab, 15);
        }
        if (timeStamp >= 450 && timeStamp < 480)
        {
            enemyWave(enemyDPrefab, 10);
            enemyWave(enemyCPrefab, 10);
            enemyWave(enemyBPrefab, 10);
            enemyWave(enemyAPrefab, 10);
        }
        if (timeStamp >= 480 && timeStamp < 580) enemyWave(enemyAPrefab, 100);
        if (timeStamp >= 580 && timeStamp < 600) { /* STOP SPAWNING */ };
    }

    void enemyWave(GameObject enemy, int amountEnemies)
    {
        amountEnemies = amountEnemies - GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        for (int i = 0; i < amountEnemies; i++)
        {
            int side = UnityEngine.Random.Range(0, 4);
            Vector3 spawnPos = new Vector3(0f, 0f, 0);

            switch (side)
            {
                case 0: //top
                    spawnPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0f, 1f), 1.1f, 100f));
                    break;
                case 1: //right
                    spawnPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector3(1.1f, UnityEngine.Random.Range(0f, 1f), 100f));
                    break;
                case 2: //bottom
                    spawnPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0f, 1f), -0.1f, 100f));
                    break;
                case 3: //left
                    spawnPos = UnityEngine.Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, UnityEngine.Random.Range(0f, 1f), 100f));
                    break;
            }

            GameObject spawn = Instantiate(enemy, spawnPos, Quaternion.identity);
            spawn.transform.SetParent(enemiesParent.transform);
        }
    }

    public void updateExperienceBar(int currentEXP, int nextLevelUp)
    {
        expSlider.maxValue = nextLevelUp;
        expSlider.value = currentEXP;
    }

    public void setLevelText(int lvl)
    {
        level.text = "LVL" + lvl;
    }
    public void setKillsText(int kill)
    {
        kills.text = "Kills" + kill;
    }

    void barrelSpawn()
    {
        Vector3 spawnPos = new Vector3(
            UnityEngine.Random.Range(-50f, 50f),
            UnityEngine.Random.Range(-50f, 50f),
            0);

        GameObject spawn = Instantiate(barrelPrefab, spawnPos, Quaternion.identity);
        spawn.transform.SetParent(barrelParent.transform);
    }
}
