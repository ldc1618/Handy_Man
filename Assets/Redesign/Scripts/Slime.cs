using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyAI {

    public Transform target;            // Identifies the player so the slime can target it
    public float chaseRadius;           // The radius around the slime where it will chase the player
    public float attackRadius;          // Radius that the slimes attacks will use
    public Transform homePosition;      // The position that the slime will return to after the player leaves its radius
    private Rigidbody2D slimeRigidBody; // Reference to the slimes rigidbody for movement

    // Start is called before the first frame update
    void Start() {
        target = GameObject.FindWithTag("Player").transform;    // Get location of the player
        slimeRigidBody = GetComponent<Rigidbody2D>();           // Get slimes Rigidbody
        currentState = EnemyState.idle;
    }

    void FixedUpdate() {
        CheckDistance();
    }

    void CheckDistance() {
        var dist = Vector3.Distance(target.position, transform.position);
        if (dist <= chaseRadius && dist > attackRadius && (currentState == EnemyState.idle || currentState == EnemyState.run) && currentState != EnemyState.stagger) {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            slimeRigidBody.MovePosition(temp);
            ChangeState(EnemyState.run);
        }
    }

    private void ChangeState(EnemyState newState) {
        if (currentState != newState) {
            currentState = newState;
        }
    }
}
