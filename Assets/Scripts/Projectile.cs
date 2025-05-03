using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); //Destroy after lifeTime
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy != null){
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }

}
