    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Enemy : MonoBehaviour
    {
        // Damage-related fields
        public float damageAmount = 0.1f; // How much damage the enemy deals on contact
        public bool isDamagingOverTime = false; // True if the enemy should cause damage over time
        public float damageRate = 0.5f; // Time interval for damage over time
        private float nextDamageTime = 0f;

        // Patrolling-related fields
        public Transform pointA; // First patrol point
        public Transform pointB; // Second patrol point
        public float speed = 2f; // Movement speed

        private Transform targetPoint; // Current target point

        void Start()
        {
            targetPoint = pointA; // Start moving towards point A
        }

        void Update()
        {
            Patrol();
        }

        private void Patrol()
        {
            // Move towards the current target point
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            // Check if the enemy has reached the target point
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // Switch target to the other point
                targetPoint = targetPoint == pointA ? pointB : pointA;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // Apply damage instantly when the player touches the enemy
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
                // Apply damage over time
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.healthBar.Damage(damageAmount);
                    nextDamageTime = Time.time + damageRate;
                }
            }
        }
    }
