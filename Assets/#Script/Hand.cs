using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;
    Vector3 rightPos = new Vector3(0.315f, -0.121f, 0);
    Vector3 rightPosReverse = new Vector3(-0.222f,  -0.121f, 0);
    Quaternion leftRot = Quaternion.Euler(0,0, 35);
    Quaternion leftRotReverse = Quaternion.Euler(0,0, -135);
    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; // 자기 자신도 포함이라 0,1,2 1번 인덱스
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft) // 근접 무기
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
