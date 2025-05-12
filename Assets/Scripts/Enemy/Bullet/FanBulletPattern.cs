using UnityEngine;

[CreateAssetMenu(fileName = "FanFormationPattern", menuName = "Bullet Patterns/Fan Formation Pattern")]
public class FanFormationPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 5; // Number of fan directions
    public float spreadAngle = 45f; // Degrees across the fan
    public float speed = 10f;
    public float clusterSpacing = 0.5f; // Distance between bullets in a cluster
    public int clusterSize = 3; // Bullets per cluster

    public override void Fire(Transform origin, Polarity polarity)
    {
        if (bulletCount < 1 || clusterSize < 1) return;

        float halfSpread = spreadAngle * 0.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Mathf.Lerp(-halfSpread, halfSpread, bulletCount == 1 ? 0.5f : (float)i / (bulletCount - 1));
            Quaternion spreadRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 baseDirection = spreadRotation * -origin.forward;

            // Get horizontal (side) direction perpendicular to bullet's direction
            Vector3 sideDirection = Vector3.Cross(Vector3.up, baseDirection).normalized;

            // Compute center offset for symmetrical cluster
            float totalOffset = (clusterSize - 1) * clusterSpacing;
            Vector3 clusterStartOffset = -baseDirection.normalized * (totalOffset / 2f);

            for (int j = 0; j < clusterSize; j++)
            {
                Vector3 offset = clusterStartOffset + sideDirection * j * clusterSpacing;
                Vector3 spawnPos = origin.position + offset;

                GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(baseDirection));
                changeBulletPolarity(bullet, polarity);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null) rb.linearVelocity = baseDirection * speed;
            }
        }
    }
}