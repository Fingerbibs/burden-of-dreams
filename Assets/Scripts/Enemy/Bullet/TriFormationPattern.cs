using UnityEngine;

[CreateAssetMenu(fileName = "TriFormationPattern", menuName = "Bullet Patterns/Tri Formation Pattern")]
public class TriFormationPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public float spacing = 0.5f; // Distance between bullets in the triangle
    public float speed = 10f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPosition = player.transform.position;

        // Define world-space triangle offsets (fixed pattern)
        Vector3[] offsets = new Vector3[]
        {
            new Vector3(0, 0, 0),                             // Center bullet
            new Vector3(-spacing, 0, -spacing),              // Left-back
            new Vector3(spacing, 0, -spacing),               // Right-back
        };

        foreach (Vector3 offset in offsets)
        {
            // Spawn bullets at world position + fixed offset (ignores rotation)
            Vector3 spawnPos = origin.position + offset;

            // Aim at the player
            Vector3 direction = (playerPosition - spawnPos).normalized;

            GameObject bullet = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
            changeBulletPolarity(bullet, polarity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = direction * speed;
        }
    }
}