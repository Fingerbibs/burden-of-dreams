using UnityEngine;
using System.Linq;
using System.Collections;

public class PatternedEnemySpawner : MonoBehaviour
{
    public EnemyWave wave;
    private float timer = 0f;
    private int nextEventIndex = 0;

    void Update()
    {
        timer += Time.deltaTime;

        // Wait for next event
        while (nextEventIndex < wave.events.Length && timer >= wave.events[nextEventIndex].time)
        {
            SpawnEvent evt = wave.events[nextEventIndex];
            // Spawn enemy group
            StartCoroutine(SpawnEnemyGroup(evt.enemyGroup));
            nextEventIndex++;
        }
    }

    // Handles group enemy spawning
    IEnumerator SpawnEnemyGroup(EnemyGroup group)
    {
        yield return new WaitForSeconds(group.groupSpawnDelay);

        // For every enemy in the group initialize their path and wait between intervals to spawn the next.
        for (int i = 0; i < group.enemies.Length; i++)
        {
            GameObject enemy = Instantiate(group.enemies[i]);
            enemy.GetComponent<BaseEnemy>().Initialize(
                group.path.GetComponentsInChildren<Transform>().Skip(0).ToArray()
            );
            yield return new WaitForSeconds(group.spawnInterval);
        }
    }
}
