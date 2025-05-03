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
            Vector3 offset = group.spawnOffsetStep * i;

            GameObject enemyGO = Instantiate(group.enemies[i], Vector3.zero, Quaternion.identity);
            Transform[] pathPoints = group.path.GetComponentsInChildren<Transform>();
            BaseEnemy enemy = enemyGO.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                enemy.Initialize(pathPoints, offset);
            }

            yield return new WaitForSeconds(group.spawnInterval);
        }
    }
}
