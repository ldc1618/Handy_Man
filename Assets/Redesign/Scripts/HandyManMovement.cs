using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for player "states" i.e. actions that the player can perform but not concurrently
public enum HandyManState {
    run,
    grab,
    toss
}

// Enum for the direction Handy Man is facing to activate the correct hitbox
public enum HandyManDirection {
    front,
    back,
    left,
    right
}

public class HandyManMovement : MonoBehaviour {

    public HandyManState currentState;
    public HandyManDirection currentDirection;
    public GameObject FrontHitbox;
    public GameObject BackHitbox;
    public GameObject LeftHitbox;
    public GameObject RightHitbox;
    public float speed;
    private Rigidbody2D handyManRigidBody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        currentState = HandyManState.run;                   // Set the state to run to start
        currentDirection = HandyManDirection.front;         // Set the direction to front to start
        handyManRigidBody = GetComponent<Rigidbody2D>();    // Get the player's rigid body
        animator = GetComponent<Animator>();

        // Set the direction to down to start
        animator.SetFloat("x", 0);
        animator.SetFloat("y", -1);
    }

    // Update is called once per frame -- May need to split this into Update and FixedUpdate functions if movement gets stuttery
    void Update() {
        change = Vector3.zero;  // Reset movement to 0 so accumulation doesn't occur
        // Get both x and y axis inputs for movement
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // Change Handy Man's state based on the actions being performed
        if (Input.GetButtonDown("Attack") && currentState != HandyManState.grab) {
            StartCoroutine(GrabCo());
        }
        else if (currentState == HandyManState.run) {
            UpdateAnimationAndMove();  // Update Handy Man's animation state based on criteria
        }
    }

    /// <summary>
    /// Coroutine function for the grab mechanic that stops the player from doing other
    /// actions while it is grabbing
    /// </summary>
    /// <returns> Waits until grab animation is done </returns>
    private IEnumerator GrabCo() {
        animator.SetBool("grabbing", true);
        currentState = HandyManState.grab;
        yield return null;
        animator.SetBool("grabbing", false);
        yield return new WaitForSeconds(.33f);  // Wait for grab animation to end
        currentState = HandyManState.run;

        // Turn off all hitboxes
        FrontHitbox.SetActive(false);
        BackHitbox.SetActive(false);
        RightHitbox.SetActive(false);
        LeftHitbox.SetActive(false);
    }

    void UpdateAnimationAndMove() {
        // Move Handy Man only if there is input to do so
        if (change != Vector3.zero)
        {
            UpdateDirection();
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

    /// <summary>
    /// Update the state for the direction Handy Man faces, and then activate the corresponding
    /// hitbox
    /// </summary>
    void UpdateDirection() {
        if (change.x > 0) {
            currentDirection = HandyManDirection.right;
        }
        else if (change.x < 0) {
            currentDirection = HandyManDirection.left;
        }
        if (change.y > 0) {
            currentDirection = HandyManDirection.back;
        }
        else if (change.y < 0) {
            currentDirection = HandyManDirection.front;
        }

        switch (currentDirection) {
            case HandyManDirection.front:
                FrontHitbox.SetActive(true);
                break;
            case HandyManDirection.back:
                BackHitbox.SetActive(true);
                break;
            case HandyManDirection.right:
                RightHitbox.SetActive(true);
                break;
            case HandyManDirection.left:
                LeftHitbox.SetActive(true);
                break;
        }
    }

    void MoveHandyMan() {
        // Move Handy Man's position based on the x and y input with the speed and time passed
        handyManRigidBody.MovePosition(
            transform.position + speed * Time.fixedDeltaTime * change.normalized
        );
    }
}
