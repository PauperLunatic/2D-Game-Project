using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static float totalHealth = 1f;
    public int maxHealth = 10;
    public int health;

    void Start()
    {
        health = maxHealth;   
    }

public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
