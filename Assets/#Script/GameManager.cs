using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("## Game Object ##")]
    public static GameManager instance;
    public PoolManager poolManager;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;
    [Header("## Game Control ##")]
    public float gameTime;
    public float maxGameTIme = 2 * 10f;
    public bool isLive; // 시간 정지
    [Header("## Player Info ##")]
    public int playerId;
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

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        // 임시 테스트
        uiLevelUp.Select(playerId % 2);
        player.gameObject.SetActive(true);

        isLive = true;
        Resume();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    private void Update()
    {
        if (isLive == false)
            return;
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTIme)
        {
            gameTime = maxGameTIme;
            GameVictroy();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        Pause();
        uiResult.Lose();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);

    }

    public void GameVictroy()
    {
        StartCoroutine(GameVictroyCoroutine());
    }

    IEnumerator GameVictroyCoroutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        Pause();
        uiResult.Win();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);

    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GetExp()
    {
        if (!isLive)
            return;

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
