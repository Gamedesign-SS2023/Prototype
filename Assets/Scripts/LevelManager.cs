using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    [Tooltip("Input needs to be in SECONDS")]
    public int levelEnd;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    private float timeStamp = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeStamp += Time.deltaTime;

        setTimer(timeStamp);
        setLevel0(timeStamp);

        if (timeStamp >= levelEnd)
        {
            enabled = false;
            //add game ending
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

            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
