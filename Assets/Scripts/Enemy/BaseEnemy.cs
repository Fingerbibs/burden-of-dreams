using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyMovement movement;
    protected EnemyPolarity polarity;

    [Header("Basic Enemy Info")]
    public int health = 1;  // Health of the enemy
    public BulletPattern deathPattern;

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

    public void TakeDamage(int damage, Polarity bulletPolarity)
    {
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            if(bulletPolarity == polarity.polarity)
                ReleaseReward();
                
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void ReleaseReward()
    {
        deathPattern.Fire(transform, polarity.polarity);
    }

    protected virtual void Update(){}

    public abstract void Shoot();
}