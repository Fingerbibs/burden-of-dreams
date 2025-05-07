using UnityEngine;
using System.Collections;

public class CubeRotation : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    private float duration = 5f;
    private float toHeight = 50f;
    private float fromHeight = 20f;

    void Start()
    {
        renderer = GetComponent<Renderer>(); 
        StartCoroutine(Disintegrate());
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Disintegrate()
    {
        material = renderer.material;
        if (material == null) yield break;

        float t = 0f;
        while (t < duration)
        {
            float h = Mathf.Lerp(fromHeight, toHeight, t / duration);
            material.SetFloat("_CutoffHeight", h);
            t += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_CutoffHeight", toHeight);
    }
}
