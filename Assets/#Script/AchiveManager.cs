using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    private void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;
        switch(achive)
        {
            case Achive.UnlockPotato:
            isAchive = GameManager.instance.kill >= 10;
            break;
            case Achive.UnlockBean:
            isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTIme;
            break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);// 해금했으니 데이터 1 집어넣기 
        }
    }
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1; // 업적이 달성했는지? 1이면 ok
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }
}
