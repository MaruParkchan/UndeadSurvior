using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private Vector2 inputVector;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {    
        Vector2 nextVector = inputVector.normalized * moveSpeed * Time.fixedDeltaTime; // FixedDeltaTime은 FixedUpdate(물리 프레임마다)에 써야한다
        playerRigidbody.MovePosition(playerRigidbody.position + nextVector);
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
}
