using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float fireRate = 0.2f;

    private float fireCooldown = 0f;

    private PlayerPolarityController playerController;
    private Polarity currentPolarity;
    private Quaternion rotation = Quaternion.Euler(90,0,0);

    void Start()
    {
        playerController = GetComponent<PlayerPolarityController>();
    }

    void Update()
    {
        // Every 0.2 seconds a projectile may be fired
        fireCooldown -= Time.deltaTime;

        if (((Gamepad.current != null && Gamepad.current.buttonSouth.isPressed) || Input.GetKey(KeyCode.Space)) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }

        if (((Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame) || Input.GetKeyDown(KeyCode.A)) && fireCooldown <= 0f)
        {
            LaunchMissile();
        }
    }

    void Shoot()
    {
        currentPolarity = playerController.currentPolarity;
        
        GameObject bulletObj1 = Instantiate(projectilePrefab, firePoint1.position, rotation);
        BulletPolarity bullet1 = bulletObj1.GetComponent<BulletPolarity>();
        bullet1.bulletPolarity = currentPolarity;

        GameObject bulletObj2 = Instantiate(projectilePrefab, firePoint2.position, rotation);
        BulletPolarity bullet2 = bulletObj2.GetComponent<BulletPolarity>();
        bullet2.bulletPolarity = currentPolarity;

        AudioManager.Instance.PlayPlayerShoot();
    }

    void LaunchMissile()
    {
        currentPolarity = playerController.currentPolarity;

        SuperMeter superMeter = GameManager.Instance.GetComponent<SuperMeter>();
        superMeter.FireSuper(transform.position, currentPolarity);
    }
}
