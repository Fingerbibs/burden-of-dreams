using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    [Header("Shooter Enemy Info")]
    public float fireCooldown = 1f;
    public BulletPattern pattern;
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    private bool canShoot = false;

    protected override void Update()
    {
        
        transform.Rotate(rotationSpeed * Time.deltaTime);

        
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f && IsInsideBounds())
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
