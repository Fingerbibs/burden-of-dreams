using UnityEngine;

public class SideWaveEnemy : BaseEnemy
{
    public float fireCooldown = 1f;
    public BulletPattern pattern;

    protected override void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    protected override void Update()
    {


        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f && IsInsideBounds())
        {
            if (pattern != null)
            {
                pattern.Fire(transform, polarity.polarity);
            }
            fireCooldown = pattern.fireRate;
        }
    }

    public override void Shoot(){}
}
