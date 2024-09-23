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

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
