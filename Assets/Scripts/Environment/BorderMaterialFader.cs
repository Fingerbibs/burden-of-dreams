using UnityEngine;

public class BorderMaterialFader : MonoBehaviour
{
    public float revealDuration = 1f;
    public float delay = 15f;
    public string cutoffProperty = "_HeightCutoff";
    
    public float startCutoff = 6f;   // Fully transparent
    public float endCutoff = 12f;    // Fully rendered

    private Material mat;
    private float timer = 0f;
    private float revealTimer = 0f;
    private bool shouldReveal = false;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        SetCutoff(startCutoff);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!shouldReveal && timer >= delay)
        {
            shouldReveal = true;
        }

        if (shouldReveal && revealTimer < revealDuration)
        {
            revealTimer += Time.deltaTime;
            float t = Mathf.Clamp01(revealTimer / revealDuration);
            float cutoffValue = Mathf.Lerp(startCutoff, endCutoff, t);
            SetCutoff(cutoffValue);
        }
    }

    void SetCutoff(float value)
    {
        if (mat.HasProperty(cutoffProperty))
        {
            mat.SetFloat(cutoffProperty, value);
        }
    }
}