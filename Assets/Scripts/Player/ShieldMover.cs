using UnityEngine;
using System.Collections;

public class ShieldMover : MonoBehaviour
{
    public Transform shieldTransform;
    public Vector3 targetOffset = new Vector3(0, -1f, 0);
    public float moveDuration = 1f;
    public float delayTime = 15f;

    private Vector3 initialPosition;

    void Start()
    {
        if (shieldTransform != null)
        {
            initialPosition = shieldTransform.localPosition;
            StartCoroutine(MoveShieldAfterDelay());
        }
    }

    private IEnumerator MoveShieldAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 targetPosition = initialPosition + targetOffset;
        Vector3 startPosition = shieldTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            shieldTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        shieldTransform.localPosition = targetPosition;
    }
}