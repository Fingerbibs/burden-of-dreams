using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public GameObject[] enemies;  // Array of enemy prefabs
    public float groupSpawnDelay = 0f;    // Time before the group starts spawning
    public float spawnInterval = 0.25f;      // Delay between each enemy in the group
    public GameObject path;       // Path for the enemies to follow
    public Vector3 spawnOffsetStep = Vector3.zero;
}