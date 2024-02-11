using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public Player player;

    public float gameTime;
    public float maxGameTIme = 2 * 10f;

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
}
