using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;

    private Transform[] path;
    private Vector3 pathOffset;
    private int segmentIndex = 0;
    private float t = 0f;
    private bool isWaiting = false;
    private bool isInitialized = false;

    public void Initialize(Transform[] pathPoints, Vector3 offset)
    {
        if (pathPoints == null || pathPoints.Length < 4)
        {
            Debug.LogError("Invalid path for enemy movement.");
            Destroy(gameObject);
            return;
        }

        path = pathPoints;
        pathOffset = offset;
        transform.position = path[0].position + offset;
        isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized || isWaiting) return;

        if (segmentIndex + 3 >= path.Length)
        {
            transform.position = path[path.Length - 1].position + pathOffset;
            Destroy(gameObject);
            return;
        }

        t += Time.deltaTime * speed;

        Vector3 newPos = CatmullRom(
            path[segmentIndex].position,
            path[segmentIndex + 1].position,
            path[segmentIndex + 2].position,
            path[segmentIndex + 3].position,
            t
        );

        transform.position = newPos + pathOffset;

        if (t >= 1f)
        {
            t = 0f;

            // Check if the next anchor (p2) has a Wait component
            Wait wp = path[segmentIndex + 2].GetComponent<Wait>();
            if (wp != null && wp.waitTime > 0f)
            {
                StartCoroutine(WaitAtWaypoint(wp.waitTime));
                return; // Prevent incrementing now
            }

            segmentIndex++;
        }
    }

    IEnumerator WaitAtWaypoint(float waitTime)
    {
        isWaiting = true;

        // Wait without teleporting or changing position
        yield return new WaitForSeconds(waitTime);

        segmentIndex++;
        isWaiting = false;
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