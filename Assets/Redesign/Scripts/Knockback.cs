using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float strength;
    public float knockTime;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check for barrel to break
        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
            collision.GetComponent<Barrel>().Smash();
        }

        // Check for enemies to knockback
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

            if (hit != null) {
                Vector2 diff = hit.transform.position - transform.position;
                diff = diff.normalized * strength;
                hit.AddForce(diff, ForceMode2D.Impulse);

                // Enemy knockback
                if (collision.gameObject.CompareTag("enemy")) {
                    hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                    collision.GetComponent<EnemyAI>().Knock(hit, knockTime);
                }

                // Player knockback
                if (collision.gameObject.CompareTag("Player")) {
                    hit.GetComponent<HandyManMovement>().currentState = HandyManState.stagger;
                    collision.GetComponent<HandyManMovement>().Knock(knockTime);
                }
            }
        }
    }
}
