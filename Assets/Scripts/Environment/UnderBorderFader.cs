using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class UnderBorderFader : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float delay = 15f;

    private Material mat;
    private float timer = 0f;
    private float fadeTimer = 0f;
    private bool shouldFade = false;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        SetAlpha(0f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!shouldFade && timer >= delay)
        {
            shouldFade = true;
        }

        if (shouldFade && fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);
            SetAlpha(t);
        }
    }

    void SetAlpha(float alpha)
    {
        if (mat.HasProperty("_BaseColor"))
        {
            Color c = mat.color;
            c.a = alpha;
            mat.color = c;
        }
    }
}