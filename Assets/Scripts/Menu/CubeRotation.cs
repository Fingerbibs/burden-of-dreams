using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
