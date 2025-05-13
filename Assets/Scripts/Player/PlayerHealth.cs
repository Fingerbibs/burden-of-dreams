using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f;

    private PlayerPolarityController polarityController;
    private Polarity currentPolarity;

    private void Start()
    {
        if (polarityController == null)
            polarityController = GetComponent<PlayerPolarityController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet")) // Bullet Collision
        {
            BulletPolarity bullet = other.GetComponent<BulletPolarity>();
            currentPolarity = polarityController.currentPolarity;
            
            if (bullet.bulletPolarity == currentPolarity)
            {
                // Absorb bullet (e.g., increase energy, destroy bullet)
                Destroy(other.gameObject);
            }
            else
            {
                // Take damage (destroy bullet)
                HandleHit();
                Destroy(other.gameObject);
                Debug.Log("Player collided with Bullet");
            }
        }
        else if (other.CompareTag("Enemy")) //Enemy Collision
        {
            Debug.Log("Player collided with enemy");
            HandleHit();
        }
        else if (other.CompareTag("EnemyBeam"))
        {
            BulletPolarity bullet = other.GetComponent<BulletPolarity>();
            currentPolarity = polarityController.currentPolarity;

            if (bullet.bulletPolarity != currentPolarity)
            {
                // Take damage (destroy bullet)
                HandleHit();
                Debug.Log("Player collided with Bullet");
            }
        }
        else if (other.CompareTag("Boss"))
        {
            HandleHit();
        }
    }

    void HandleHit()
    {
        GameManager.Instance.PlayerDied(gameObject);
    }
}
