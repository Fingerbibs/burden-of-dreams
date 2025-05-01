using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject buildingPrefab;
    public float spawnInterval = 0.05f;
    public float spawnXRange = 10f;
    public float spawnYRange = 4f;
    public float spawnZ = 15f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBuilding();
            timer = spawnInterval;
        }
    }

    void SpawnBuilding()
    {
        // Grab a random height and position to spawn the buildings
        float randomX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPosition = new Vector3(randomX, spawnYRange, spawnZ);

        // Calculate Z tilt based on X position
        float tiltZ = 0f;
        if (randomX > 0)
            tiltZ = -10f;
        else if (randomX < 0)
            tiltZ = 10f;

        // Rotate
        Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 90f), tiltZ);

        GameObject building = Instantiate(buildingPrefab, spawnPosition, rotation);

        float scaleX = Random.Range(2f, 9f);
        float scaleY = Random.Range(5, 17f);  // Thin if you're using Y for visual height
        float scaleZ = Random.Range(3f, 6f);

        building.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

    }
}
