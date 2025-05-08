using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class BackgroundCameraFlyIn : MonoBehaviour
{
    public float flyInDuration = 3f;
    public Vector3 targetPosition = new Vector3(0f, 20f, -30f);
    public Quaternion targetRotation = Quaternion.Euler(25f, 0f, 0f);
    public float startFOV = 80f;
    public float endFOV = 60f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = false;
        cam.fieldOfView = startFOV;

        StartCoroutine(FlyIn());
    }

    private IEnumerator FlyIn()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float startFOVValue = cam.fieldOfView;

        float elapsed = 0f;

        while (elapsed < flyInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / flyInDuration);

            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRotation, t);
            cam.fieldOfView = Mathf.Lerp(startFOVValue, endFOV, t);

            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        cam.fieldOfView = endFOV;
    }
}