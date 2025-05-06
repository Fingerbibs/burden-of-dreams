using UnityEngine;

[CreateAssetMenu(fileName = "BulletPattern", menuName = "Scriptable Objects/BulletPattern")]
public abstract class BulletPattern : ScriptableObject
{
    public float fireRate = 1f;

    public abstract void Fire(Transform origin, Polarity polarity);

    public void changeBulletPolarity(GameObject bullet, Polarity target)
    {
        // Set Polarity
        var bulletPolarity = bullet.GetComponent<BulletPolarity>();
        if (bulletPolarity != null)
            bulletPolarity.bulletPolarity = target;
        // Set layer
        bullet.layer = (target == Polarity.Light)
            ? LayerMask.NameToLayer("EnemyBullet_Light")
            : LayerMask.NameToLayer("EnemyBullet_Dark");
    }
}
