using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDir;
    private Animator animator;
    private Pickup pickup;

    // Initialize at starting fram
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pickup = gameObject.GetComponent<Pickup>();
        pickup.Direction = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = Vector2.zero;
        ProcessInputs();
    }

    void FixedUpdate()
    {
        // physics calculation
        Move();
    }
    
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY);

        if (moveDir != Vector2.zero) {
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);
            animator.SetBool("moving", true);
            pickup.Direction = moveDir.normalized;
        }
        else {
            animator.SetBool("moving", false);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
