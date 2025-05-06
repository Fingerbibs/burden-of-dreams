using UnityEngine;

[CreateAssetMenu(fileName = "CirclePattern", menuName = "Bullet Patterns/Circle Pattern")]
public class CirclePattern : BulletPattern
{
    public GameObject projectilePrefab;
    public int bulletCount = 8;
    public float radius = 1f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * Mathf.PI * 2f / bulletCount;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            // Create bullet and assign polarity
            GameObject bullet = Instantiate(projectilePrefab, origin.position + dir * radius, Quaternion.LookRotation(dir));
            changeBulletPolarity(bullet, polarity);
        }
    }
}