using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    public Transform subject;  // What the cam is following
    public float camFollowSpeed;  // How fast ("smooth") the camera follows
    public Vector2 maxPos;  // Max camera position so it doesn't go out of bounds
    public Vector2 minPos;  // Min camera position so it doesn't go out of bounds

    // Start is called before the first frame update
    void Start() {
        
    }

    // LateUpdate is called once per frame last
    void FixedUpdate() {
        // Compare cam position to what it's following's position, move it if different
        if (transform.position != subject.position) {
            // Create a position vector so that the z position of the cam is unchanged
            Vector3 movePosition = new Vector3(subject.position.x,
                                               subject.position.y, 
                                               transform.position.z);

            // Ensure the cam doesn't move out of the min or max boundaries
            movePosition.x = Mathf.Clamp(movePosition.x, minPos.x, maxPos.x);
            movePosition.y = Mathf.Clamp(movePosition.y, minPos.y, maxPos.y);

            // Move the cam to the subject's position
            transform.position = Vector3.Lerp(transform.position, movePosition, camFollowSpeed);
        }
    }
}
