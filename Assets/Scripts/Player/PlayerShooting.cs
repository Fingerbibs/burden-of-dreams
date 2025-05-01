using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float fireRate = 0.2f;

    private float fireCooldown = 0f;

    void Update()
    {
        // Every 0.2 seconds a projectile may be fired
        fireCooldown -= Time.deltaTime;

        if(((Gamepad.current != null && Gamepad.current.buttonSouth.isPressed) || Input.GetKey(KeyCode.Space)) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(projectilePrefab, firePoint2.position, firePoint2.rotation);
    }
}
