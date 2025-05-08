using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    [Header("Enemies")]
    [Tooltip("Enemy prefabs to spawn in this group")]
    public GameObject[] enemies;

    [Header("Timing")]
    [Tooltip("Time before this group begins spawning")]
    public float groupSpawnDelay = 0f;

    [Tooltip("Time between each enemy spawn in this group")]
    public float spawnInterval = 0.25f;

    [Header("Pathing")]
    [Tooltip("Path GameObject that holds the waypoints")]
    public GameObject path;

    [Tooltip("Step offset applied per enemy (e.g. 0, 0, 2 for z spacing)")]
    public Vector3 spawnOffsetStep = Vector3.zero;

    [Header("Path Offset")]
    [Tooltip("Offset to apply to the entire path (e.g., move the path to a different position)")]
    public Vector3 pathOffset = Vector3.zero; 
}