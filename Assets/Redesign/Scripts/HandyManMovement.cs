using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandyManMovement : MonoBehaviour {
    public float speed;
    private Rigidbody2D handyManRigidBody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        handyManRigidBody = GetComponent<Rigidbody2D>();  // Get the player's rigid body
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        change = Vector3.zero;  // Reset movement to 0 so accumulation doesn't occur
        // Get both x and y axis inputs for movement
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        Debug.Log(change);
        UpdateAnimation();  // Update Handy Man's animation state based on criteria
    }

    void UpdateAnimation() {
        // Move Handy Man only if there is input to do so
        if (change != Vector3.zero)
        {
            MoveHandyMan();
            animator.SetFloat("x", change.x);
            animator.SetFloat("y", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveHandyMan() {
        // Move Handy Man's position based on the x and y input with the speed and time passed
        handyManRigidBody.MovePosition(
            transform.position + change.normalized * speed * Time.fixedDeltaTime
        );
    }
}
