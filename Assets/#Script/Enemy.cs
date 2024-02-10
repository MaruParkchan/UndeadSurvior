using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isLive)
            return;
        Vector2 dirVector = target.position - rigid.position;
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector); // 현재위치 + 다음위치
        rigid.velocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;

    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }


}
