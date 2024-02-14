using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] itmes;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        itmes = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Pause();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void Select(int index)
    {
        itmes[index].OnClick();
    }

    void Next() //  레벨업시 아이템 창 뜰때 아이템 랜덤 출력
    {
        // 1 모든 아이템 비활성화
        foreach (Item item in itmes)
        {
            item.gameObject.SetActive(false);
        }

        // 2, 아이템 3개 활성화 
        int[] randomIndex = new int[3];
        for (int i = 0; i < 3; i++)
        {
            randomIndex[i] = Random.Range(0, itmes.Length - 1); // 포션은 안뜨게 하기 위해 -1 하였음
            for (int j = 0; j < i; j++)
            {
                if (randomIndex[i] == randomIndex[j])
                    i--;
            }
        }

        foreach(int value in randomIndex)
            Debug.Log("값 = " + value);

        for (int i = 0; i < randomIndex.Length; i++)
        {
            Item randomItem = itmes[randomIndex[i]]; // randomIndex의 아이템 n개중의 3개 넣은 데이터값 넣기

            // 3, 아이템이 만랩 찍을수도 있기 때문에 필터 
            if (randomItem.level == randomItem.data.damages.Length)
            {
                itmes[4].gameObject.SetActive(true); // 마지막 아이템 소비아이템이라 4를 넣은거 만약 더 추가할꺼면 배열로 다시 만들어야함
            }
            else
            {
                randomItem.gameObject.SetActive(true); // 만랩이 아니면 출력
            }
        }
    }
}
