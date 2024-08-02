using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float spawnInterval = 60f;

    private float nextSpawn = 0f;

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            SpawnBoss();
            nextSpawn = Time.time + spawnInterval;
        }
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, new Vector3(0, 6, 0), Quaternion.identity);
    }
}
