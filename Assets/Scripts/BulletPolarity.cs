using UnityEngine;

public class BulletPolarity : MonoBehaviour
{
    public Polarity bulletPolarity;
    private Renderer bulletRenderer;

    void Start()
    {
        bulletRenderer = GetComponent<Renderer>();

        // Set the initial color based on the player's polarity
        UpdateBulletColor();
    }

    public void UpdateBulletColor()
    {
        if (bulletPolarity == Polarity.Light)
        {
            bulletRenderer.material.color = Color.white;  // Light bullet
        }
        else
        {
            bulletRenderer.material.color = Color.black;  // Dark bullet
        }
    }
}
