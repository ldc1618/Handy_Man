using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    public SpriteRenderer playerSR;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerSR.enabled = true;
        playerMovement.enabled = true;
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health <= 0) {
            playerSR.enabled = false;
            playerMovement.enabled = false;
        }
    }
}
