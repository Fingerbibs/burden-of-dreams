using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;           // The object to look at
    public float radius = 10f;         // Radius of the orbit
    public float height = 5f;          // Height offset from the target
    public float rotationSpeed = 20f;  // Degrees per second

    private float angle = 0f;

    void Update()
    {
        if (target == null) return;

        angle += rotationSpeed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        // Calculate new position on the circle
        float x = target.position.x + Mathf.Cos(radians) * radius;
        float z = target.position.z + Mathf.Sin(radians) * radius;
        float y = target.position.y + height;

        transform.position = new Vector3(x, y, z);
        transform.LookAt(target);
    }
}
