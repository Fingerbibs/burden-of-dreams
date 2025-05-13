using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private float delay;
    private ExplosiveMinePattern pattern;
    private Vector3 spawnPosition;
    private Polarity polarity;

    public void Initialize(float delay, ExplosiveMinePattern pattern)
    {
        this.delay = delay;
        this.pattern = pattern;
        
        polarity = this.GetComponent<BulletPolarity>().bulletPolarity;

        // Start the explosion after the delay
        Invoke(nameof(Explode), delay);
    }

    private void Explode()
    {
        spawnPosition = transform.position;
        // Fire bullets in a circular pattern upon explosion
        for (int i = 0; i < pattern.explosionBulletCount; i++)
        {
            float angle = (i / (float)pattern.explosionBulletCount) * Mathf.PI * 2f;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            // Create the explosion bullet
            GameObject explosionBullet = Instantiate(pattern.projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
            Rigidbody rb = explosionBullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = direction * pattern.explosionSpeed;

            // Apply the polarity to the explosion bullet
            pattern.changeBulletPolarity(explosionBullet, this.polarity); // Correctly assign the polarity
        }

        // Destroy the original mine bullet after explosion
        Destroy(gameObject);
    }
}