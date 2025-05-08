using UnityEngine;
using System.Collections;

public class CameraFlyInController : MonoBehaviour
{
    public float flyInDuration = 2f;

    private Vector3 targetPosition = new Vector3(0f, 15f, 0f);
    private Quaternion targetRotation = Quaternion.Euler(90f, 0f, 0f);
    private float targetSize = 7f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(FlyIn());
    }

    private IEnumerator FlyIn()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float startSize = cam.orthographicSize;

        float elapsed = 0f;

        while (elapsed < flyInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flyInDuration);

            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRotation, t);
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

            yield return null;
        }

        // Final values
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        cam.orthographicSize = targetSize;
    }
}