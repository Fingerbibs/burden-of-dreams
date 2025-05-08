using UnityEngine;

[CreateAssetMenu(fileName = "BeamPattern", menuName = "Bullet Patterns/Beam Pattern")]
public class BeamPattern : BulletPattern
{
    public GameObject beamPrefab;
    public float beamLength = 15f;
    public float rotationSpeed = 20f; // degrees per second
    public float trackingDuration = 1.5f;

    public override void Fire(Transform origin, Polarity polarity)
    {
        GameObject beam = Instantiate(beamPrefab, origin.position, origin.rotation);
        changeBulletPolarity(beam, polarity);
        FireAndReturn(origin, polarity);
    }

    public GameObject FireAndReturn(Transform origin, Polarity polarity)
    {
        GameObject beam = Instantiate(beamPrefab, origin.position, origin.rotation);
        changeBulletPolarity(beam, polarity);

        BeamTracker tracker = beam.GetComponent<BeamTracker>();
        if (tracker != null)
        {
            tracker.fullLength = beamLength;
            tracker.origin = origin;
        }

        return beam;
    }
}