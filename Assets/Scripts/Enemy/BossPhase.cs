using UnityEngine;

[CreateAssetMenu(fileName = "BossPhase", menuName = "Scriptable Objects/BossPhase")]
public class BossPhase : ScriptableObject
{
    public float healthThreshold; // Example: 0.7f means transition when HP < 70%
    public BulletPattern bulletPattern;
    public Polarity polarity;
}
