using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for player "states" i.e. actions that the player can perform but not concurrently
public enum HandyManState {
    run,
    grab,
    toss,
    kick
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
    public Grab grab;                           // Use this to update the Grab script with the correct direction Handy Man is facing
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

        // Set the grab component and default direction
        grab = gameObject.GetComponent<Grab>();
        grab.Direction = new Vector2(0, -1);
    }

    // Update is called once per frame -- May need to split this into Update and FixedUpdate functions if movement gets stuttery
    void Update() {
        change = Vector3.zero;  // Reset movement to 0 so accumulation doesn't occur
        // Get both x and y axis inputs for movement
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // Change Handy Man's state based on the actions being performed
        if (Input.GetButtonDown("Kick") && currentState != HandyManState.kick) {
            StartCoroutine(KickCo());
        }
        else if (currentState == HandyManState.run) {
            UpdateAnimationAndMove();  // Update Handy Man's animation state based on criteria
        }
    }

    /// <summary>
    /// Coroutine function for the kick mechanic that stops the player from doing other
    /// actions while it is kicking
    /// </summary>
    /// <returns> Waits until kick animation is done </returns>
    private IEnumerator KickCo() {
        ActivateHitbox();
        animator.SetBool("kicking", true);
        currentState = HandyManState.kick;
        yield return null;
        animator.SetBool("kicking", false);
        yield return new WaitForSeconds(.5f);  // Wait for kick animation to end
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
            // Stop Handy Man from sliding
            handyManRigidBody.velocity = Vector3.zero;

            animator.SetBool("moving", false);
        }
    }

    /// <summary>
    /// Update the state for the direction Handy Man is facing
    /// </summary>
    void UpdateDirection() {
        if (change.x > 0) {
            currentDirection = HandyManDirection.right;
            grab.Direction = new Vector2(1, 0);
        }
        else if (change.x < 0) {
            currentDirection = HandyManDirection.left;
            grab.Direction = new Vector2(-1, 0);
        }
        if (change.y > 0) {
            currentDirection = HandyManDirection.back;
            grab.Direction = new Vector2(0, 1);
        }
        else if (change.y < 0) {
            currentDirection = HandyManDirection.front;
            grab.Direction = new Vector2(0, -1);
        }
    }

    /// <summary>
    /// Activate the correct hitbox given the direction Handy Man is facing
    /// </summary>
    void ActivateHitbox() {
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
