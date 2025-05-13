using UnityEngine;

[CreateAssetMenu(fileName = "SporadicFloatPattern", menuName = "Bullet Patterns/Sporadic Float Pattern")]
public class SporadicFloatPattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 10;
    public float speed = 3f;
    public float maxAngle = 45f; // Maximum spread angle (degrees)

    public override void Fire(Transform origin, Polarity polarity)
    {
        // Store the enemy's initial forward direction (before rotation)
        Vector3 initialDirection = new Vector3(origin.forward.x, 0f, origin.forward.z).normalized;

        for (int i = 0; i < bulletCount; i++)
        {
            // Generate a random angle within the max angle limit
            float angle = Random.Range(-maxAngle, maxAngle);

            // Rotate the initial direction by this random angle around the Y-axis
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 spreadDirection = rotation * initialDirection;

            // Make sure spreadDirection stays within the XZ plane (no vertical component)
            spreadDirection.y = 0f;

            // Instantiate the bullet with the calculated spread direction
            GameObject bullet = Instantiate(projectilePrefab, origin.position, Quaternion.LookRotation(spreadDirection));
            changeBulletPolarity(bullet, polarity);

            // Attach wandering behavior (optional)
            var wander = bullet.GetComponent<WanderBullet>();
            if (wander != null)
                wander.Initialize(spreadDirection * speed);
        }
    }
}