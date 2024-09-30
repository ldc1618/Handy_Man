using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    run,
    attack,
    stagger
}

public class EnemyAI : MonoBehaviour {

    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public EnemyState currentState;

    public void Knock(Rigidbody2D enemy, float knockTime) {
        StartCoroutine(KnockbackCo(enemy, knockTime));
    }

    private IEnumerator KnockbackCo(Rigidbody2D enemy, float knockTime) {
        if (enemy != null) {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            enemy.velocity = Vector2.zero;
        }
    }
}
