using UnityEngine;

[CreateAssetMenu(fileName = "RewardPattern", menuName = "Bullet Patterns/Reward Bullet Pattern")]
public class RewardPattern : BulletPattern
{
    public GameObject rewardBulletPrefab;
    public int bulletCount = 5;
    public float speed = 5f;

    private float spawnRadius = 1f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        Renderer renderer = origin.GetComponentInChildren<Renderer>();
        if (renderer != null)
            spawnRadius = renderer.bounds.extents.magnitude / 2;

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0f; // Flatten if needed

            Vector3 spawnPos = origin.position + randomOffset;
            Vector3 directionToPlayer = (player.transform.position - spawnPos).normalized;

            // Create bullet and set polarity
            GameObject bullet = Instantiate(rewardBulletPrefab, spawnPos, Quaternion.LookRotation(directionToPlayer));
            changeBulletPolarity(bullet, polarity);

            // Move towards player
            // Rigidbody rb = bullet.GetComponent<Rigidbody>();
            // if (rb != null)
            //     rb.linearVelocity = directionToPlayer * speed;

            bullet.transform.position += directionToPlayer * speed * Time.deltaTime;
        }
    }
}