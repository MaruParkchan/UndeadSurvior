using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    float timer;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(1,spawnPoints.Length)].position; 
        // 1부터 시작하는 이유 자기 자신도 0으로 들어가기 때문에 1부터 시작 
    }
}
