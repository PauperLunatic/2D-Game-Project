using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f; // Range of the player's attack
    public int attackDamage = 10; // Damage dealt per attack
    public Transform attackPoint; // Point from where the attack originates
    public LayerMask enemyLayer; // Layer to identify enemies

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Assuming Fire1 is the attack button
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red; // Set the Gizmo color for clarity
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Draw the attack area
    }
}