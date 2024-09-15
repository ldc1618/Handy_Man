using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public Transform holdSpot;      // Where to put the item when holding
    public LayerMask pickUpMask;    // Limit this to only things in the pick up layer
    private GameObject item;        // The item being held

    // The direction the player is facing, gotten from HandyManMovement
    public Vector3 Direction { get; set; }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Grab")) {

            // Attempt to grab something or drop if holding something already
            if (item) {
                item.transform.position = transform.position + Direction;           // Put the item down in the correct direction
                item.transform.parent = null;                                       // Remove Handy Man as the parent
                item.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";    // Set the sorting layer back to behind the player

                // Ensure the object no longer follows the player
                if (item.GetComponent<Rigidbody2D>()) {
                    item.GetComponent<Rigidbody2D>().simulated = true;
                }

                item = null;        // Clear the held item
            }
            else {
                Collider2D grabbedItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (grabbedItem) {
                    item = grabbedItem.gameObject;                  // Set the held item
                    item.transform.position = holdSpot.position;    // Put item in the holding spot
                    item.transform.parent = transform;              // Make Handy Man the items parent so it goes with him
                    item.GetComponent<SpriteRenderer>().sortingLayerName = "Player";    // Set sorting layer to in front of player

                    // Ensure object follows player
                    if (item.GetComponent<Rigidbody2D>()) {
                        item.GetComponent<Rigidbody2D>().simulated = false;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Throw")) {
            if (item) {
                StartCoroutine(ThrowItem(item));
                item = null;
            }
        }

        // Coroutine to make the held item get thrown
        IEnumerator ThrowItem(GameObject heldItem) {
            Vector3 start = heldItem.transform.position;
            Vector3 end = transform.position + Direction * 2;
            heldItem.transform.parent = null;
            for (int i = 0; i < 25; i++) {
                heldItem.transform.position = Vector3.Lerp(start, end, i * .04f);
                yield return null;
            }

            if (heldItem.GetComponent<Rigidbody2D>()) {
                heldItem.GetComponent<Rigidbody2D>().simulated = true;
            }

            heldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";   // Set the sorting layer back to behind the player
        }
    }
}
