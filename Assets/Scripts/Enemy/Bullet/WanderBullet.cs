using UnityEngine;

public class WanderBullet : MonoBehaviour
{
    public float speed = 3f;
    public float directionChangeInterval = 1.5f;
    public float maxAngleChange = 30f;
    public float lifeTime = 10f;

    public Vector2 xBounds = new Vector2(-10f, 10f);
    public Vector2 zBounds = new Vector2(-5f, 5f);

    private Vector3 velocity;
    private float timer;

    public void Initialize(Vector3 initialVelocity)
    {
        velocity = initialVelocity;
        timer = directionChangeInterval;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Update position
        Vector3 nextPos = transform.position + velocity * Time.deltaTime;

        // Check X bounds
        if (nextPos.x < xBounds.x || nextPos.x > xBounds.y)
        {
            velocity.x *= -1;
            nextPos.x = Mathf.Clamp(nextPos.x, xBounds.x, xBounds.y);
        }

        // Check Z bounds
        if (nextPos.z < zBounds.x || nextPos.z > zBounds.y)
        {
            velocity.z *= -1;
            nextPos.z = Mathf.Clamp(nextPos.z, zBounds.x, zBounds.y);
        }

        // Apply direction change
        if (timer <= 0f)
        {
            float angle = Random.Range(-maxAngleChange, maxAngleChange);
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            velocity = rotation * velocity;
            timer = directionChangeInterval;
        }

        transform.position = nextPos;
    }
}