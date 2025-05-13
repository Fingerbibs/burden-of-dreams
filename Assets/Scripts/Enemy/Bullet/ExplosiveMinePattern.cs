using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ExplosiveMinePattern", menuName = "Bullet Patterns/Explosive Mine Pattern")]
public class ExplosiveMinePattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int clusterSize = 4;                 // 4 bullets in a row
    public float clusterSpacing = 0.5f;         // Distance between each bullet
    public float speed = 10f;
    public float maxDistance = 5f;

    public float explosionDelay = 1f;
    public int explosionBulletCount = 10;
    public float explosionRadius = 5f;
    public float explosionSpeed = 10f;

    private int clusterCount = 0;

    public override void Fire(Transform origin, Polarity polarity)
    {
        Polarity currentPolarity = (clusterCount % 2 == 0) ? Polarity.Light : Polarity.Dark;
        
        PatternCoroutineRunner runner = origin.GetComponent<PatternCoroutineRunner>();
        if (runner == null)
        {
            runner = origin.gameObject.AddComponent<PatternCoroutineRunner>();
        }
        runner.Run(SpawnCluster(origin, currentPolarity));

        clusterCount++;
    }

    public IEnumerator SpawnCluster(Transform origin, Polarity polarity)
    {
        Vector3 forward = -origin.forward;
        Vector3 right = Vector3.right;

        float totalWidth = (clusterSize - 1) * clusterSpacing;

        for (int i = 0; i < clusterSize; i++)
        {
            // Position bullets evenly along X-axis (from enemy's perspective)
            Vector3 offset = right * (i * clusterSpacing - totalWidth / 2f);
            Vector3 spawnPos = origin.position + offset;

            GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(forward));
            // Alternate polarity for each bullet in the cluster
            changeBulletPolarity(bullet, polarity);

            // Handle bullet movement in the pattern
            BulletMovement bulletMovement = bullet.AddComponent<BulletMovement>();
            bulletMovement.Initialize(speed, maxDistance);

            BulletExplosion explosion = bullet.AddComponent<BulletExplosion>();
            explosion.Initialize(explosionDelay, this);

            yield return new WaitForSeconds(0.1f); // Delay between bullets
        }
    }
}

public class BulletMovement : MonoBehaviour
{
    private float speed;
    private float maxDistance;
    private Vector3 spawnPosition;

    public void Initialize(float speed, float maxDistance)
    {
        this.speed = speed;
        this.maxDistance = maxDistance;
        spawnPosition = transform.position;
    }

    void Update()
    {
        float distanceTraveled = Vector3.Distance(spawnPosition, transform.position);

        if (distanceTraveled < maxDistance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position = spawnPosition + transform.forward * maxDistance;
        }
    }
}