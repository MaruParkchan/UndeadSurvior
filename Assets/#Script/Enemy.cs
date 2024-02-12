using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animatorCon;
    public float health; // 현재체력 
    private float maxHealth; // 최대체력 

    bool isLive;

    Rigidbody2D enemyRigid;
    SpriteRenderer spriter;
    Animator enemyAnimator;
    WaitForFixedUpdate wait;
    Collider2D coll;

    private void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!isLive || enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVector = target.position - enemyRigid.position;
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime;
        enemyRigid.MovePosition(enemyRigid.position + nextVector); // 현재위치 + 다음위치
        enemyRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = target.position.x < enemyRigid.position.x;

    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        enemyRigid.simulated = true;
        spriter.sortingOrder = 2;
        enemyAnimator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        enemyAnimator.runtimeAnimatorController = animatorCon[spawnData.spriteType];
        health = spawnData.health;
        maxHealth = spawnData.health;
        speed = spawnData.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !isLive)
            return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if (health > 0)
        {
            enemyAnimator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            enemyRigid.simulated = false;
            spriter.sortingOrder = 1;
            enemyAnimator.SetBool("Dead", true);
            GameManager.instance.KillUp();
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // new 할당을 계속 하면 가비지 컬렉터가 발생
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVector = (transform.position - playerPos).normalized;
        enemyRigid.AddForce(dirVector * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

}
