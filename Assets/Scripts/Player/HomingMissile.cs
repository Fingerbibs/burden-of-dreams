using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 360f;
    public float launchDuration = 0.3f; // Time before homing starts
    public int damage = 10;

    private BulletPolarity missilePolarity;
    private Transform target;
    private Vector3 launchDirection;
    private float launchTimer;

    void Start()
    {
        missilePolarity = gameObject.GetComponent<BulletPolarity>();
        launchDirection = transform.forward;
        launchTimer = launchDuration;
    }

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
            target = newTarget.transform;
    }

    void Update()
    {
        if (launchTimer > 0f)
        {
            // Move in initial direction before homing
            transform.position += launchDirection * speed * Time.deltaTime;
            launchTimer -= Time.deltaTime;
        }
        else if (target != null)
        {
            // Home in on target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject); // No target? Kill missile
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            EnemyPolarity enemyPolarity = enemy.GetComponent<EnemyPolarity>();

            if(missilePolarity.bulletPolarity != enemyPolarity.polarity)
            {
                 enemy.TakeDamage(damage * 2, missilePolarity.bulletPolarity);
                 Destroy(gameObject);
            }
            else
            {
                enemy.TakeDamage(damage, missilePolarity.bulletPolarity);
                Destroy(gameObject);
            }
        }
    }
}