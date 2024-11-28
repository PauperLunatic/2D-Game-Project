    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health of the enemy
    private int currentHealth;

    public float damageAmount = 0.1f; // How much damage the enemy deals on contact
    public bool isDamagingOverTime = false; // True if the enemy should cause damage over time
    public float damageRate = 0.5f; // Time interval for damage over time
    private float nextDamageTime = 0f;

    public Transform pointA; // First patrol point
    public Transform pointB; // Second patrol point
    public float speed = 2f; // Movement speed

    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointA;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Patrol();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");

        // Increase the player's score
        Scoring.totalScore += 10; // Award 10 points (adjust as needed)
        Debug.Log("Score increased to: " + Scoring.totalScore);

        // Update the score UI (if applicable)
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.scoreText.text = "Score: " + Scoring.totalScore;
            }
        }

        // Optionally, play a death animation or particle effect
        // animator.SetTrigger("Die");

        Destroy(gameObject); // Remove enemy from scene
    }


    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.healthBar.Damage(damageAmount);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamagingOverTime && collision.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.healthBar.Damage(damageAmount);
                nextDamageTime = Time.time + damageRate;
            }
        }
    }
}
