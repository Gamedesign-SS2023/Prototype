using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Enemy")]
    public GameObject enemyPrefab;

    private float timeStamp = 0;

    // Update is called once per frame
    void Update()
    {
        timeStamp += Time.deltaTime;

        setTimer(timeStamp);
        setLevel0(timeStamp);
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
        amountEnemies = amountEnemies - GameObject.FindGameObjectsWithTag("enemy").Length;

        for (int i = 0; i < amountEnemies; i++)
        {
            //TO-DO: adjust random.range to spawn outside of visible game screen. they will move towards player on their own
            Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
    }
}
