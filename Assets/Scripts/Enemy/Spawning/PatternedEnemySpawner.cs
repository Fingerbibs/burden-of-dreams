using UnityEngine;
using System.Linq;
using System.Collections;

public class PatternedEnemySpawner : MonoBehaviour
{
    public void TriggerWave(EnemyWave waveToSpawn)
    {
        StartCoroutine(SpawnWave(waveToSpawn));

    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        Debug.Log("Spawning wave");
        float lastTime = 0f;

        for (int i = 0; i < wave.events.Length; i++)
        {
            SpawnEvent evt = wave.events[i];
            float waitTime = evt.time - lastTime;

            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            lastTime = evt.time;

            for (int j = 0; j < evt.enemyGroups.Length; j++)
            {
                EnemyGroup group = evt.enemyGroups[j];
                if(group.enemies.Length == 1 && group.enemies[0].CompareTag("Boss")){
                    SpawnBoss(group.enemies[0], group.path, group.pathOffset);
                }
                else
                {
                    Vector3 po = group.pathOffset;
                    StartCoroutine(SpawnEnemyGroup(group, j, group.pathOffset));
                }
            }
        }
    }

    IEnumerator SpawnEnemyGroup(EnemyGroup group, int groupIndex, Vector3 po)
    {
        yield return new WaitForSeconds(group.groupSpawnDelay);

        // Create a temporary clone of the path
        GameObject pathInstance = Instantiate(group.path);
        pathInstance.transform.position += group.pathOffset;

        // Optionally parent it under the spawner or another container
        pathInstance.transform.SetParent(transform);

        // Get path points excluding the root
        Transform[] pathPoints = pathInstance.GetComponentsInChildren<Transform>();

        for (int i = 0; i < group.enemies.Length; i++)
        {
            Vector3 offset = (group.spawnOffsetStep * i) + (groupIndex * po);
            GameObject enemyGO = Instantiate(group.enemies[i], Vector3.zero, Quaternion.identity);
            BaseEnemy enemy = enemyGO.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                Debug.Log($"Initializing enemy {i} with path and offset {offset}.");
                enemy.Initialize(pathPoints, offset);
            }

            yield return new WaitForSeconds(group.spawnInterval);
        }
    }
    
    public void SpawnBoss(GameObject bossPrefab, GameObject pathPrefab, Vector3 pathOffset)
    {
        GameObject pathInstance = Instantiate(pathPrefab);
        pathInstance.transform.position += pathOffset;
        pathInstance.transform.SetParent(transform);

        Transform[] pathPoints = pathInstance.GetComponentsInChildren<Transform>();

        GameObject bossGO = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        FinalBoss finalBoss = bossGO.GetComponent<FinalBoss>();
        
        if (finalBoss != null)
        {
            finalBoss.Initialize(pathPoints, Vector3.zero);
        }
        else
        {
            Debug.LogWarning("Spawned boss is missing FinalBoss script.");
        }
    }
}