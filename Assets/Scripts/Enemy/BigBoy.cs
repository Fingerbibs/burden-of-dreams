using UnityEngine;

public class BigBoy : BaseEnemy
{
    public float fireCooldown = 1f;
    public BulletPattern pattern;
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    protected override void Update()
    {

        transform.Rotate(rotationSpeed * Time.deltaTime);
        
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            if (pattern != null)
            {
                pattern.Fire(transform, polarity.polarity);
            }
            fireCooldown = pattern.fireRate;
        }
    }

    public override void Shoot(){}
}
