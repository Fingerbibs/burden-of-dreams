using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuperMeter : MonoBehaviour
{
    [Header("Player Super Meter")]
    public float superMeter = 0f;
    public float superMeterMax = 100f;
    public int baseDamage = 10;
    public LineRenderer linePrefab;

    [Header("UI")]
    public Slider superMeterSlider; // Reference to the UI slider for super meter
    public SuperMeter3D superMeter3D;
    private MissileDialogue missileDialogue;

    void Start()
    {
        // Initialize the super meter UI slider
        if (superMeterSlider != null)
        {
            superMeterSlider.maxValue = superMeterMax;
            superMeterSlider.value = superMeter;
        }

        missileDialogue = gameObject.GetComponent<MissileDialogue>();
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
        missileDialogue.toTerminal();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int chargeCount = GetChargeCount();
        if (chargeCount <= 0 || enemies.Length == 0) return;

        int missilesToFire = Mathf.Min(chargeCount, enemies.Length);

        for (int i = 0; i < missilesToFire; i++)
        {
            BaseEnemy target = enemies[i].GetComponent<BaseEnemy>();
            EnemyPolarity targetPolarity = target.GetComponent<EnemyPolarity>();

            // Apply damage based on polarity logic
            int damage = (targetPolarity.polarity != playerPolarity) ? baseDamage * 2 : baseDamage;
            target.TakeDamage(damage, playerPolarity);

            // Draw laser line
            StartCoroutine(DrawLaserLine(firePosition, target.transform.position));
        }

        // Subtract charge
        superMeter -= missilesToFire * 10f;
        if (superMeterSlider != null)
            superMeterSlider.value = superMeter;

    }

    private IEnumerator DrawLaserLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(linePrefab, start, Quaternion.identity);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        yield return new WaitForSeconds(0.2f); // Show the line briefly
        Destroy(line.gameObject);
    }

}
