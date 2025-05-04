using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyMovement movement;
    protected EnemyPolarity polarity;

    public int health = 1;  // Health of the enemy
    private bool isDead = false;

    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        polarity = GetComponent<EnemyPolarity>();
    }

    public void Initialize(Transform[] path, Vector3 offset)
    {
        movement?.Initialize(path, offset);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    protected virtual void Update(){}

    public abstract void Shoot();
}