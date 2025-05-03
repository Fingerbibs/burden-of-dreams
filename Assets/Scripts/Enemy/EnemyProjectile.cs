using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); //Destroy after lifeTime
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

}