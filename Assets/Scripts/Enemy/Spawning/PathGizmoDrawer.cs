using UnityEngine;

public class PathGizmoDrawer : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    [Range(5, 50)] public int resolution = 20; // Spline resolution per segment

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Transform[] points = GetComponentsInChildren<Transform>();
        if (points.Length < 4) return;

        for (int i = 0; i < points.Length - 3; i++)
        {
            Vector3 p0 = points[i].position;
            Vector3 p1 = points[i + 1].position;
            Vector3 p2 = points[i + 2].position;
            Vector3 p3 = points[i + 3].position;

            Vector3 prevPoint = CatmullRom(p0, p1, p2, p3, 0f);
            for (int j = 1; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 newPoint = CatmullRom(p0, p1, p2, p3, t);
                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }

        // Optionally, draw small spheres at each waypoint
        for (int i = 1; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i].position, 0.1f);
        }
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            2f * p1 +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t
        );
    }
}
