using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animatorCon;
    public float health; // 현재체력 
    private float maxHealth; // 최대체력 

    bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator enemyAnimator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
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
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        enemyAnimator.runtimeAnimatorController = animatorCon[spawnData.spriteType];
        health = spawnData.health;
        maxHealth = spawnData.health;
        speed = spawnData.speed;     
    }

}
