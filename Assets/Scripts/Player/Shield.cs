using UnityEngine;

public class Shield : MonoBehaviour
{
    public Material shieldMaterial;
    public float colorTransitionDuration = 0.2f;

    private Color lightColor = new Color32(255,255,255,90);
    private Color darkColor = new Color32(90,90,90,255);

    private PlayerPolarityController playerPolarity;
    private Renderer shieldRenderer;

    void Start()
    {
        playerPolarity = GetComponentInParent<PlayerPolarityController>();
        shieldRenderer = GetComponent<Renderer>();
        shieldMaterial = shieldRenderer.material;

        if (playerPolarity != null)
        {
            UpdateShieldMaterial(playerPolarity.currentPolarity);
            playerPolarity.OnPolarityChanged += UpdateShieldMaterial;
        }
    }

    private void OnDestroy()
    {
        if (playerPolarity != null)
            playerPolarity.OnPolarityChanged -= UpdateShieldMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            AudioManager.Instance.PlayAbsorb();
            BulletPolarity bullet = other.GetComponent<BulletPolarity>();
            if (bullet != null && bullet.bulletPolarity == playerPolarity.currentPolarity)
            {
                Destroy(other.gameObject); // absorb bullet of same polarity

                // Increase super meter
                SuperMeter superMeter = GameManager.Instance.GetComponent<SuperMeter>();
                superMeter.IncreaseSuperMeter(1f);
            }
        }
    }

    // Udates shield material, collision layer, and changes color
    private void UpdateShieldMaterial(Polarity polarity)
    {
        Color targetColor = polarity == Polarity.Light ? lightColor : darkColor;
        SetCollisionLayer(polarity);
        StopAllCoroutines(); // In case player polarity changes rapidly
        StartCoroutine(LerpShieldColor(targetColor));
    }

    private void SetCollisionLayer(Polarity polarity)
    {
        gameObject.layer = (polarity == Polarity.Light)
            ? LayerMask.NameToLayer("Shield_Light")
            : LayerMask.NameToLayer("Shield_Dark");
    }

    private System.Collections.IEnumerator LerpShieldColor(Color targetColor)
    {
        Color startColor = shieldMaterial.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / colorTransitionDuration;
            shieldMaterial.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        shieldMaterial.color = targetColor;
    }
}