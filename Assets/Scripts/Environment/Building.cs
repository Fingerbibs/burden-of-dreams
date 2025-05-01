using UnityEngine;

public class Building : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Destroy when below view
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}
