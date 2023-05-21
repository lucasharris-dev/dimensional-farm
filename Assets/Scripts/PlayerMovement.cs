using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;

    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void OnWalk(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * walkSpeed, moveInput.y * walkSpeed);
        myRigidbody.velocity = playerVelocity;
    }

}
