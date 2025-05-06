using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyMovement movement;
    protected EnemyPolarity polarity;
    protected Renderer renderer;
    protected Material material;

    [Header("Basic Enemy Info")]
    public int health = 1;  // Health of the enemy
    public BulletPattern deathPattern;

    private bool isDead = false;

    // Disintegrate Parameters
    private float fromHeight = 8f;
    private float toHeight = 5f;
    private float duration = 0.35f;

    protected virtual void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
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
        StartCoroutine(Disintegrate());
    }

    private IEnumerator Disintegrate()
    {
        material = renderer.material;
        if (material == null) yield break;

        float t = 0f;
        while (t < duration)
        {
            float h = Mathf.Lerp(fromHeight, toHeight, t / duration);
            material.SetFloat("_CutoffHeight", h);
            t += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_CutoffHeight", toHeight);

        Destroy(gameObject);
    }

    public void ReleaseReward()
    {
        deathPattern.Fire(transform, polarity.polarity);
    }

    protected virtual void Update(){}

    public abstract void Shoot();
}