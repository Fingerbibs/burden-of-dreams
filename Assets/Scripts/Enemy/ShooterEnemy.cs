using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;
    private float cooldownTimer;

    protected override void Update()
    {
        base.Update();
        cooldownTimer -= Time.deltaTime;
        
        if(cooldownTimer <0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    public override void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
