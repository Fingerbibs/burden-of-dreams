using UnityEngine;

[CreateAssetMenu(fileName = "BulletPattern", menuName = "Scriptable Objects/BulletPattern")]
public abstract class BulletPattern : ScriptableObject
{
    public float fireRate = 1f;

    public abstract void Fire(Transform origin, Polarity polarity);
}
