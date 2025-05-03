using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;
    private float cooldownTimer;
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    protected override void Update()
    {
        
        base.Update();
        cooldownTimer -= Time.deltaTime;
        
        if(cooldownTimer <0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
