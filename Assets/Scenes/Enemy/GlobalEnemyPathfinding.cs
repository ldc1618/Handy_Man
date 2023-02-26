using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnemyPathfinding : MonoBehaviour
{
    private Transform player; // defines target
    public float speed; // choose speed 

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);  
        Vector2 direction = player.position - transform.position; 
        // Makes enemy rotate - Might want later
        //direction.Normalize();  // keeps direction and length when turning
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    // finds angle from enemy to player

        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime); // moves toward player
        // Makes enemy rotate - Might want later
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle); // rotates enemy to player position
    }
}
