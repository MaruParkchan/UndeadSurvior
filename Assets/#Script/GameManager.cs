using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("## Game Object ##")]
    public static GameManager instance;
    public PoolManager poolManager;
    public Player player;
    public LevelUp uiLevelUp;
    [Header("## Game Control ##")]
    public float gameTime;
    public float maxGameTIme = 2 * 10f;
    public bool isLive; // 시간 정지
    [Header("## Player Info ##")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 200, 300, 410, 530, 670 };


    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        // 임시 테스트
        uiLevelUp.Select(0);
        isLive = true;
    }

    private void Update()
    {
        if (isLive == false)
            return;
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void KillUp()
    {
        kill++;
    }

    public void Pause()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
