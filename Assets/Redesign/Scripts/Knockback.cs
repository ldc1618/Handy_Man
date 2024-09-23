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
        if (collision.gameObject.CompareTag("enemy")) {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();

            if (enemy != null) {
                enemy.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                Vector2 diff = enemy.transform.position - transform.position;
                diff = diff.normalized * strength;
                enemy.AddForce(diff, ForceMode2D.Impulse);
                StartCoroutine(KnockbackCo(enemy));
            }
        }
    }

    private IEnumerator KnockbackCo(Rigidbody2D enemy) {
        if (enemy != null) {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<EnemyAI>().currentState = EnemyState.idle;
        }
    }
}
