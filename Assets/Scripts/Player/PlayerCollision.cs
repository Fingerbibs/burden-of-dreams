using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerPolarityController polarityController; // Assign in inspector or via GetComponent

    private void Start()
    {
        if (polarityController == null)
            polarityController = GetComponent<PlayerPolarityController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // BulletPolarity bullet = other.GetComponent<BulletPolarity>();
        // if (bullet != null)
        // {
        //     if (bullet.bulletPolarity == currentPolarity)
        //     {
        //         // Absorb bullet (e.g., increase energy, destroy bullet)
        //         Destroy(other.gameObject);
        //     }
        //     else
        //     {
        //         // Take damage (destroy bullet)
        //         Destroy(other.gameObject);
        //     }
        // }

        // Enemy Collision
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with enemy");
            Die();
        }
    }

    private void Die()
    {
        // death logic here

        Debug.Log("Player died");
        Destroy(gameObject);
    }
}
