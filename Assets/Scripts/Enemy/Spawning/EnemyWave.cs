using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Scriptable Game/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    public SpawnEvent[] events;
}
