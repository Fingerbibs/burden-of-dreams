using UnityEngine;

public class BulletBounds : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
