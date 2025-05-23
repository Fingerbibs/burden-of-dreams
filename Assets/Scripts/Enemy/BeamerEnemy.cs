using UnityEngine;

public class BeamerEnemy : BaseEnemy
{
    [Header("Beamer Enemy Info")]
    public float fireCooldown = 1f;
    public BulletPattern pattern;
    public float turnSpeed = 60f; // degrees per second

    private Vector3 lastPosition;
    private const float movementThreshold = 0.01f;

    public GameObject currentBeam;
    private bool hasFired = false;

    private Transform player;

    public override void Initialize(Transform[] path, Vector3 offset)
    {
        base.Initialize(path, offset); // Keep base movement/path logic

        if (currentBeam != null && currentBeam.scene.IsValid())
        {
            Destroy(currentBeam);
            currentBeam = null;
        }

        // Beamer-specific setup
        currentBeam = null;
        fireCooldown = 2f;
        hasFired = false;

    }

    protected override void Start()
    {
        lastPosition = transform.position;
    }

    protected override void Update()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
        base.Update();

        // 1) Rotate toward the player each frame
        if (player != null)
        {
            Vector3 toPlayer = (player.position - transform.position).normalized;
            Quaternion target = Quaternion.LookRotation(toPlayer);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                target,
                turnSpeed * Time.deltaTime
            );
        }

        bool isStationary = (transform.position - lastPosition).sqrMagnitude < movementThreshold * movementThreshold;
        
        if(!isStationary && hasFired)
        {
            hasFired = false;
            Destroy(currentBeam);
        }
        // 2) Handle firing
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f && pattern != null && isStationary && hasFired == false && IsInsideBounds())
        {
            if (pattern is BeamPattern beamPattern) // Safely cast to BeamPattern
            {
                hasFired = true;
                currentBeam = beamPattern.FireAndReturn(transform, polarity.polarity);
            }
            fireCooldown = pattern.fireRate;
        }

        lastPosition = transform.position;
    }

    public override void Shoot() { }
}
