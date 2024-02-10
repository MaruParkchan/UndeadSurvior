using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf) // 풀 할 오브젝트가 꺼져있다면 
            {
                select = item;
                select.SetActive(true); // 다시 재가동
                break;
            }
        }

        if(!select) // 오브젝트가 없다면 새로 할당 
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
