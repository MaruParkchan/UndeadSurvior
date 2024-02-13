using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 inputVector;
    public Scanner scanner;
    public Hand[] hands;

    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRender;
    private Animator playerAnimator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRender = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // 꺼진 오브젝트도 집어넣음 
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isLive == false)
            return;
        Vector2 nextVector = inputVector.normalized * moveSpeed * Time.fixedDeltaTime; // FixedDeltaTime은 FixedUpdate(물리 프레임마다)에 써야한다
        playerRigidbody.MovePosition(playerRigidbody.position + nextVector);
    }

    private void LateUpdate()
    {
        if (GameManager.instance.isLive == false)
            return;
        PlayerFlipReversing();
        AnimationChange();
    }

    void PlayerFlipReversing() // inputVector에 따른 Flip 반전 
    {
        if (inputVector.x != 0)
        {
            playerSpriteRender.flipX = inputVector.x < 0; // inputVector.x 가 0보다 작으면 true 
        }
    }

    void AnimationChange()
    {
        playerAnimator.SetFloat("Speed", inputVector.magnitude);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(!GameManager.instance.isLive)
        return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health <= 0)
        {
            for(int i = 2; i < transform.childCount; i++) // Area까지는 살리기 위해 i = 2
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            playerAnimator.SetTrigger("Dead");
        }
    }
}
