using UnityEngine;
using UnityEngine.UI;

public class SuperMeter : MonoBehaviour
{
    [Header("Player")]
    public float superMeter = 0f;
    public float superMeterMax = 100f;
    public GameObject homingMissilePrefab; // Assign in Inspector

    [Header("UI")]
    public Slider superMeterSlider; // Reference to the UI slider for super meter
    public SuperMeter3D superMeter3D;

    void Start()
    {
        // Initialize the super meter UI slider
        if (superMeterSlider != null)
        {
            superMeterSlider.maxValue = superMeterMax;
            superMeterSlider.value = superMeter;
        }
    }

    public int GetChargeCount()
    {
        return Mathf.FloorToInt(superMeter / 10f);
    }

    public void IncreaseSuperMeter(float amount)
    {
        superMeter += amount;
        if (superMeter > superMeterMax)
            superMeter = superMeterMax; // Ensure it doesn't exceed the max value

        // Update the super meter UI slider
        if (superMeterSlider != null)
            superMeterSlider.value = superMeter;
        
        superMeter3D.UpdateMeter(superMeter);
    }


    public void FireSuper(Vector3 firePosition, Polarity playerPolarity)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int chargeCount = GetChargeCount();
        if (chargeCount <= 0 || enemies.Length == 0) return;

        int missilesToFire = Mathf.Min(chargeCount, enemies.Length);
        float spreadAngle = 90f; // total angle of spread
        float angleStep = (missilesToFire > 1) ? spreadAngle / (missilesToFire - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < missilesToFire; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 spawnPos = firePosition + direction.normalized * 1.5f; // offset from player

            GameObject missile = Instantiate(homingMissilePrefab, firePosition, Quaternion.identity);
            HomingMissile hm = missile.GetComponent<HomingMissile>();
            BulletPolarity hmPolarity = missile.GetComponent<BulletPolarity>();
            hmPolarity.bulletPolarity = playerPolarity;

            hm.SetTarget(enemies[i]); // Make sure this method exists in your HomingMissile script
        }

        superMeter -= missilesToFire * 10f;
        if (superMeterSlider != null)
            superMeterSlider.value = superMeter;

    }

}
