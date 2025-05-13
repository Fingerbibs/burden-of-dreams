using UnityEngine;

[CreateAssetMenu(fileName = "StraightSidePattern", menuName = "Bullet Patterns/StraightSidePattern")]
public class StraightSidePattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 5;
    public float spacing = 1f;
    public float speed = 10f;

    public int clusterSize = 3;
    public float clusterSpacing = 0.5f;

    public bool shootRight = false; // Set this to true to shoot right

    public override void Fire(Transform origin, Polarity polarity)
    {
        Vector3 forward = shootRight ? Vector3.right : Vector3.left;

        // Align the row based on direction
        Vector3 rowStartOffset = -origin.up * ((bulletCount - 1) * spacing / 2f);

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 baseOffset = rowStartOffset + origin.up * i * spacing;
            Vector3 centerPosition = origin.position + baseOffset;

            // Perpendicular to forward (horizontal cluster direction)
            Vector3 sideDirection = Vector3.Cross(Vector3.forward, forward).normalized;
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
