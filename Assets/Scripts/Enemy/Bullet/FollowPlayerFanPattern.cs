using UnityEngine;

[CreateAssetMenu(fileName = "FollowPlayerFanPattern", menuName = "Bullet Patterns/Follow Player Fan Pattern")]
public class FollowPlayerFanPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 5; // Number of fan directions
    public float spreadAngle = 45f; // Degrees across the fan
    public float speed = 10f;
    public float clusterSpacing = 0.5f; // Vertical distance between bullets in a cluster
    public int clusterSize = 3; // Bullets per cluster (vertically)

    public override void Fire(Transform origin, Polarity polarity)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPosition = player.transform.position;
        Vector3 playerDirection = (playerPosition - origin.position).normalized;

        float halfSpread = spreadAngle * 0.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate the spread angle for each bullet
            float angle = Mathf.Lerp(-halfSpread, halfSpread, bulletCount == 1 ? 0.5f : (float)i / (bulletCount - 1));

            // Rotate the forward direction by this angle around the Y-axis
            Quaternion spreadRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 baseDirection = spreadRotation * playerDirection;

            // Calculate vertical clustering offsets
            float totalOffset = (clusterSize - 1) * clusterSpacing;
            Vector3 clusterStartOffset = -baseDirection.normalized * (totalOffset / 2f);

            for (int j = 0; j < clusterSize; j++)
            {
                // Add vertical offsets along the Y-axis
                Vector3 offset = clusterStartOffset + baseDirection.normalized * j * clusterSpacing;
                Vector3 spawnPos = origin.position + offset;

                // Instantiate the bullet at the spawn position with the correct direction
                GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(baseDirection));
                changeBulletPolarity(bullet, polarity);

                // Apply velocity to move the bullet
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null) rb.linearVelocity = baseDirection * speed;
            }
        }
    }
}