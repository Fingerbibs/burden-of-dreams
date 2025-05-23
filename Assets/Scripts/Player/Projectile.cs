using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;
    

    private BulletPolarity bulletPolarity;

    private void Awake()
    {
        bulletPolarity = GetComponent<BulletPolarity>();
        if (bulletPolarity == null)
        {
            Debug.LogError("BulletPolarity component is missing on the projectile.");
        }
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); //Destroy after lifeTime
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && IsInsideBounds())
        {
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            EnemyPolarity enemyPolarity = enemy.GetComponent<EnemyPolarity>();

            if(bulletPolarity.bulletPolarity != enemyPolarity.polarity)
            {
                 enemy.TakeDamage(damage * 2, bulletPolarity.bulletPolarity);
                 Destroy(gameObject);
            }
            else
            {
                enemy.TakeDamage(damage, bulletPolarity.bulletPolarity);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Boss") && IsInsideBounds())
        {
            FinalBoss boss = other.GetComponent<FinalBoss>();
            boss.TakeDamage(damage);
                Destroy(gameObject);
        }
    }

    public bool IsInsideBounds()
    {
        Vector3 p = transform.position;
        return p.x >= -5f && p.x <= 5f
            && p.y >= -10f && p.y <= 10f
            && p.z >= -8f && p.z <= 7f;
    }

}