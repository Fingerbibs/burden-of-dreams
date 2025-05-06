using UnityEngine;

[CreateAssetMenu(fileName = "TriFormationPattern", menuName = "Bullet Patterns/Tri Formation Pattern")]
public class TriFormationPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public float spacing = 0.5f; // Distance between bullets in the triangle
    public float speed = 10f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        // Define local triangle positions (relative to origin)
        Vector3[] localOffsets = new Vector3[]
        {
            new Vector3(0, 0, 0),                             // Center bullet
            new Vector3(-spacing, 0, -spacing),              // Left-back
            new Vector3(spacing, 0, -spacing),               // Right-back
        };

        foreach (Vector3 offset in localOffsets)
        {
            Vector3 worldPosition = origin.TransformPoint(offset);

            // Create Bullet & assign polarity
            GameObject bullet = Instantiate(projectilePrefab, worldPosition, origin.rotation);
            changeBulletPolarity(bullet, polarity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = origin.forward * speed;
        }
    }
}