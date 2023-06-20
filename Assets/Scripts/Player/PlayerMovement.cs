using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float sprintSpeed = 15f;

    float moveSpeed;
    
    Keyboard keyboard;
    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    Farming farming;

    void Awake()
    {
        keyboard = Keyboard.current;
        myRigidbody = GetComponent<Rigidbody2D>();
        farming = GetComponentInChildren<Farming>();
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        Move();
    }

    void OnWalk(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        if (farming.GetPickingSeed())
        {
            return;
        }
        
        if (Sprint())
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        myRigidbody.velocity = playerVelocity;
    }

    bool Sprint()
    {
        if (keyboard.leftShiftKey.isPressed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
