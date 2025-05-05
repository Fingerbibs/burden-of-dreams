using UnityEngine;

public class EnemyPolarity : MonoBehaviour
{
    public Polarity polarity;
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Color tint = (polarity == Polarity.Light) ? lightColor : darkColor;
            renderer.material = new Material(renderer.material);
            renderer.material.color = tint;
        }
    }
}
