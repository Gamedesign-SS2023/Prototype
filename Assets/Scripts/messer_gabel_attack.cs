using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messer_gabel_attack : MonoBehaviour
{
    [SerializeField] float timetoattack;
    float timer;

    [SerializeField] GameObject messerGabelPrefab;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (timer < timetoattack)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnMesser();
    }

    private void SpawnMesser()
    {
        GameObject throwingMesser=Instantiate(messerGabelPrefab);
        throwingMesser.transform.position = transform.position;
        throwingMesser.GetComponent<throwingMesserProjectile>().SetDirection(player.lastHorizontalVector,player.lastVertictalVector);
    }
}
