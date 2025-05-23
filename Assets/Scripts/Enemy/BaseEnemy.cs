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

    protected virtual void Start(){}

    public virtual void Initialize(Transform[] path, Vector3 offset)
    {
        movement?.Initialize(path, offset);
    }

    public void TakeDamage(int damage, Polarity bulletPolarity)
    {
        if (isDead) return;
        health -= damage;
        
        AudioManager.Instance.PlayBossHit();
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
        // Destroy beam if this is a BeamerEnemy
        if (this is BeamerEnemy beamer && beamer.currentBeam != null)
        {
            Destroy(beamer.currentBeam);
        }
        
        StartCoroutine(Disintegrate());
    }

    private IEnumerator Disintegrate()
    {
        material = renderer.material;
        if (material == null) yield break;

        AudioManager.Instance.PlayDisintegrate();

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

    public bool IsInsideBounds()
    {
        Vector3 p = transform.position;
        return p.x >= -5f && p.x <= 5f
            && p.y >= -100f && p.y <= 100f
            && p.z >= -6f && p.z <= 6f;
    }

    protected virtual void Update(){}

    public abstract void Shoot();
}