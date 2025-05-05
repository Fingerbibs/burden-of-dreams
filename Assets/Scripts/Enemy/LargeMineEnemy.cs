using UnityEngine;

public class LargeMineEnemy : BaseEnemy
{
    public Vector3 rotationSpeed = new Vector3(90f, 90f, 90f); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public override void Shoot(){}
}
