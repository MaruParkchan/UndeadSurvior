using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    public float levelTime;

    float timer;
    [SerializeField] int level;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTIme / spawnData.Length;
    }

    private void Update()
    {
        if (GameManager.instance.isLive == false)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(0);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        // 1부터 시작하는 이유 자기 자신도 0으로 들어가기 때문에 1부터 시작 
    }
}

