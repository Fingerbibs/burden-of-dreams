using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public enum Polarity { Light, Dark }
    public Polarity enemyPolarity;
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    public float speed = 1f;
    private float t = 0f;

    protected Transform[] path;
    protected Vector3 pathOffset;
    private int segmentIndex = 0; // Start at the first waypoint (0 index)

    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Color tint = (enemyPolarity == Polarity.Light) ? lightColor : darkColor;

            // Use a copy of the material to avoid affecting all enemies
            renderer.material = new Material(renderer.material);
            renderer.material.color = tint;
        }
    }
    
    public void Initialize(Transform[] pathPoints, Vector3 offset)
    {
        path = pathPoints;
        transform.position = path[0].position + offset; // Ensure enemy starts at the first waypoint
        pathOffset = offset;
    }

    protected virtual void Update()
    {
        // Early exit if path is not set up correctly or if there are not enough points
        if (path == null || path.Length < 4 || segmentIndex + 3 >= path.Length)
        {
            Destroy(gameObject);
            return;
        }

        // Interpolate position using Catmull-Rom spline
        t += Time.deltaTime * speed;

        // If the interpolation has reached the end of this segment, move to the next
        if (t >= 1f)
        {
            t = 0f;
            segmentIndex++; // Move to the next segment
        }

        // Prevent accessing out of bounds: stop when we're near the last point
        if (segmentIndex + 3 >= path.Length)
        {
            transform.position = path[path.Length - 1].position; // Final position at the last waypoint
            Destroy(gameObject); // Destroy the enemy once they reach the end of the path
            return;
        }

        // Catmull-Rom interpolation between four points: previous, current, next, next-next
        Vector3 newPos = CatmullRom(
            path[segmentIndex].position,        // p0
            path[segmentIndex + 1].position,    // p1
            path[segmentIndex + 2].position,    // p2
            path[segmentIndex + 3].position,    // p3
            t
        );

        transform.position = newPos + pathOffset;
    }

    // Catmull-Rom spline interpolation between four points
    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            2f * p1 +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t
        );
    }

    public abstract void Shoot();
}