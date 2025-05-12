using UnityEngine;

[CreateAssetMenu(fileName = "SideWavePattern", menuName = "Bullet Patterns/Side Wave Pattern")]
public class SideWavePattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int clusterSize = 3;
    public float clusterSpacing = 0.5f;
    public float speed = 10f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        // Fire from both sides: +right and -right
        FireCluster(origin, polarity, origin.right);   // Right side
        FireCluster(origin, polarity, -origin.right);  // Left side
    }

    private void FireCluster(Transform origin, Polarity polarity, Vector3 direction)
    {
        // Compute side direction for horizontal spacing (along up vector here to keep bullets stacked)
        Vector3 horizontal = Vector3.back;
        float totalWidth = (clusterSize - 1) * clusterSpacing;
        Vector3 startOffset = -horizontal * (totalWidth / 2f);

        // Offset to spawn bullets on the side (so they don’t spawn inside the enemy)
        Vector3 sideOffset = direction.normalized * 1f; // tweak this distance to match enemy width

        for (int i = 0; i < clusterSize; i++)
        {
            Vector3 offset = startOffset + horizontal * i * clusterSpacing;
            Vector3 spawnPos = origin.position + sideOffset + offset;

            GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
            changeBulletPolarity(bullet, polarity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = direction * speed;
        }
    }
}