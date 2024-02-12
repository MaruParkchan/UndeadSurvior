using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("## Game Object ##")]
    public static GameManager instance;
    public PoolManager poolManager;
    public Player player;
    [Header("## Game Control ##")]
    public float gameTime;
    public float maxGameTIme = 2 * 10f;
    [Header("## Player Info ##")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 200, 300, 410, 530, 670} ;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

    public void KillUp()
    {
        kill++;
    }
}
