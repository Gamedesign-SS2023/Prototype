using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    [Tooltip("Input needs to be in SECONDS")]
    public int levelEnd;

    [Header("Enemies")]
    public GameObject enemyPrefab;
    [Tooltip("GameObject the enemies are supposed to spawn in")]
    public GameObject enemyParent;

    [Header("EXP")]
    public Slider expSlider;
    public TextMeshProUGUI level;

    [Header("Barrels")]
    public bool barrelSpawnActive = true;
    public GameObject barrelPrefab;
    [Tooltip("GameObject the barrels are supposed to spawn in")]
    public GameObject barrelParent;

    private float timeStamp = 0;

    void Start()
    {
        if(barrelSpawnActive)
        {
            InvokeRepeating("barrelSpawn", 0, 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeStamp += Time.deltaTime;

        setTimer(timeStamp);
        setLevel0(timeStamp);

        if (timeStamp >= levelEnd)
        {
            GameObject.Find("Player").GetComponent<CharacterGameOver>().GameOver(false);
        }
    }

    void setTimer(float timeStamp)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeStamp);
        timerText.text = time.ToString(@"mm\:ss");
    }

    void setLevel0(float timeStamp)
    {
        if (timeStamp >= 0 && timeStamp < 60) enemyWave(enemyPrefab, 10);
        if (timeStamp >= 60 && timeStamp < 120) enemyWave(enemyPrefab, 50);
        if (timeStamp >= 120 && timeStamp < 180) enemyWave(enemyPrefab, 100);
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
            spawn.transform.SetParent(enemyParent.transform);
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
