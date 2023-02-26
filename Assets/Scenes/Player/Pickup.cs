using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform holdSpot;  // for the holding spot of enemies
    public LayerMask pickUpMask; // filter for just in the pickup layer

    public Vector3 Direction {get; set;} // find which way player is facing
    private GameObject itemHolding; // store picked up enemy here

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {  // dropping logic
            if (itemHolding) { // check if already holding item
                itemHolding.transform.position = transform.position + Direction;    // drops the item
                itemHolding.transform.parent = null; // disconnects dropped enemy from parent function
                if (itemHolding.GetComponent<Rigidbody2D>()) {
                    itemHolding.GetComponent<Rigidbody2D>().simulated = true;
                }
                itemHolding = null;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (itemHolding) {
                    StartCoroutine(ThrowItem(itemHolding));
                    itemHolding = null;
            }            
            else {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, 1.0f, pickUpMask); // (player position, player direction facing(multiply to look farther away), radius, get only items you can pickup
            if (pickUpItem) {
                itemHolding = pickUpItem.gameObject;    // actually pick up the item
                itemHolding.transform.position = holdSpot.position; // move the picked up item to the holding position
                itemHolding.transform.parent = transform;    // set parent of picked up item to the player character
                if (itemHolding.GetComponent<Rigidbody2D>()) {// if we have rigidbody2d on item, make it so it follows the player
                    itemHolding.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }
        }
        }
    }

    IEnumerator ThrowItem(GameObject item) {
        // Get the trow vector
        Vector3 start = item.transform.position;
        Vector3 end = item.transform.position + Direction * 3;
        item.transform.parent = null;

        // 'Animate' the item moving to the end throw location
        for (int i = 0; i < 25; i++) {
            item.transform.position = Vector3.Lerp(start, end, i * 0.04f);
            yield return null;
        }

        Destroy(item, 0.2f);
    }
}
