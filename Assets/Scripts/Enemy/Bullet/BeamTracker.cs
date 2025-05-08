using UnityEngine;
using System.Collections;

public class BeamTracker : MonoBehaviour
{
    [HideInInspector] public Transform origin;
    [Header("Extension Settings")]
    public float extendDuration = 0.2f;
    public float fullLength      = 15f;

    [Header("Tracking Settings")]
    public float trackingDuration = 1.5f;    // total time it will track (including during extension)
    public float rotationSpeed    = 20f;      // degrees per second

    private Vector3 originalScale;
    private Vector3 originPosition;
    private Transform player;
    private float elapsed = 0f;

    void Start()
    {
        // Cache values
        originalScale  = transform.localScale;
        originPosition = transform.position;

        // Start at zero length
        transform.localScale = new Vector3(originalScale.x, originalScale.y, 0f);

        // Find player
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj) player = pObj.transform;

        StartCoroutine(ExtendAndTrack());
    }

    private IEnumerator ExtendAndTrack()
    {
        while (elapsed < Mathf.Max(extendDuration, trackingDuration))
        {
            float dt = Time.deltaTime;
            elapsed += dt;

            // 1) Compute new length
            float tExtend = Mathf.Clamp01(elapsed / extendDuration);
            float zLength = Mathf.Lerp(0f, fullLength, tExtend);
            transform.localScale = new Vector3(originalScale.x, originalScale.y, zLength);

            // 2) Reposition beam so its base stays at originPosition
            transform.position = originPosition + transform.forward * (zLength / 2f);

            // 3) Track player (only while within trackingDuration)
            if (player != null && elapsed <= trackingDuration)
            {
                Vector3 toPlayer = (player.position - originPosition).normalized;
                Quaternion targetRot = Quaternion.LookRotation(toPlayer);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * dt
                );
            }

            yield return null;
        }

        // (Optional) destroy beam after both phases complete
        Destroy(gameObject);
    }
}