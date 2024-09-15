using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRooms : MonoBehaviour {

    public Vector2 cameraMaxChange;     // Amount to increase max camera bounds by
    public Vector2 cameraMinChange;     // Amount to increase min camera bounds by
    public Vector3 playerChange;        // Amount to move player into the new room
    private CamMovement cam;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.GetComponent<CamMovement>();  // Get the camera object to manipulate it
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// This is called when the box collider is triggered via contact. It ensures that the
    /// collision was with the player and then switched to the other room accordingly.
    /// </summary>
    /// <param name="collision"> An object in the game, checks if it is the player </param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            cam.minPos += cameraMinChange;
            cam.maxPos += cameraMaxChange;
            collision.transform.position += playerChange;
        }
    }
}
