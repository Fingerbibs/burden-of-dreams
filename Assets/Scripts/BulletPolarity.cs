using UnityEngine;

public class BulletPolarity : MonoBehaviour
{
    public Polarity bulletPolarity;
    private Renderer bulletRenderer;

    void Start()
    {
        bulletRenderer = GetComponent<Renderer>();
        
        // Clone the material instance so this bullet doesn't affect others
        bulletRenderer.material = new Material(bulletRenderer.material);
        
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
