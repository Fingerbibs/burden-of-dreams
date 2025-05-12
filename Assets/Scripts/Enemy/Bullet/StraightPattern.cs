using UnityEngine;

[CreateAssetMenu(fileName = "TriFormationPattern", menuName = "Bullet Patterns/Straight Pattern")]
public class StraightPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 5; // Main row of bullets
    public float spacing = 1f;  // Space between bullets in the row
    public float speed = 10f;

    public int clusterSize = 3;         // Bullets per cluster
    public float clusterSpacing = 0.5f; // Side-to-side spacing within a cluster

    public override void Fire(Transform origin, Polarity polarity)
    {
        Vector3 rowStartOffset = -origin.right * ((bulletCount - 1) * spacing / 2f);

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 baseOffset = rowStartOffset + origin.right * i * spacing;
            Vector3 centerPosition = origin.position + baseOffset;
            Vector3 forward = Vector3.back;

            // Perpendicular to forward (horizontal plane)
            Vector3 sideDirection = Vector3.Cross(Vector3.up, forward).normalized;
            float totalClusterWidth = (clusterSize - 1) * clusterSpacing;
            Vector3 clusterStartOffset = -sideDirection * (totalClusterWidth / 2f);

            for (int j = 0; j < clusterSize; j++)
            {
                Vector3 clusterOffset = clusterStartOffset + sideDirection * j * clusterSpacing;
                Vector3 spawnPos = centerPosition + clusterOffset;

                GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(forward));
                changeBulletPolarity(bullet, polarity);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.linearVelocity = forward * speed;
            }
        }
    }
}
