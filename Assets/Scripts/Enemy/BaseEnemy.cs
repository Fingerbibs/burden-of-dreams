using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyMovement movement;
    protected EnemyPolarity polarity;

    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        polarity = GetComponent<EnemyPolarity>();
    }

    public void Initialize(Transform[] path, Vector3 offset)
    {
        movement?.Initialize(path, offset);
    }

    protected virtual void Update(){}

    public abstract void Shoot();
}