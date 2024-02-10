using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Vector2 inputVector;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRender;
    private Animator playerAnimator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRender = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 nextVector = inputVector.normalized * moveSpeed * Time.fixedDeltaTime; // FixedDeltaTime은 FixedUpdate(물리 프레임마다)에 써야한다
        playerRigidbody.MovePosition(playerRigidbody.position + nextVector);
    }

    private void LateUpdate()
    {
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
}
