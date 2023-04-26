using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float timeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        //enemyWave(10,20);
    }

    // Update is called once per frame
    void Update()
    {
        timeStamp += Time.deltaTime;

        if(timeStamp >= 0 && timeStamp < 60) enemyWave(enemyPrefab, 10);
        if (timeStamp >= 60 && timeStamp < 120) enemyWave(enemyPrefab, 20);
        if (timeStamp >= 120 && timeStamp < 180) enemyWave(enemyPrefab, 30);
    }

    void enemyWave(GameObject enemy, int amountEnemies)
    {
        amountEnemies = amountEnemies - GameObject.FindGameObjectsWithTag("enemy").Length;

        for (int i = 0; i < amountEnemies; i++)
        {
            //TO-DO: adjust random.range to spawn outside of visible game screen. they will move towards player on their own
            Instantiate(enemy, new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
    }
}
