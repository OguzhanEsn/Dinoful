using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 1.5f;
    public float difficultyIncreaseRate = 0.1f;
    private float timeToSpawn = 0f;

    private void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnObstacle();
            timeToSpawn = Time.time + spawnInterval;
            spawnInterval = Mathf.Max(spawnInterval - difficultyIncreaseRate, 0.5f); // Decrease interval with a limit
        }
    }

    private void SpawnObstacle()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
        return colors[Random.Range(0, colors.Length)];
    }
}
