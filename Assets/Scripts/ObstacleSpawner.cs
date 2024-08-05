using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject largeObstaclePrefab;
    public GameObject[] smallObstaclePrefabs; // Array of small obstacle prefabs
    public GameObject colorChangerPrefab;
    public float spawnRate = 1f;
    public float spawnXPosition = 10f;
    public float bottomYPosition1 = -4f;
    public float bottomYPosition2 = -3f;
    public float topYPosition1 = 4f;
    public float topYPosition2 = 3f;
    public float minObstacleDistance = 2f; // Minimum distance between obstacles
    public int minObstaclesBetweenColorChangers = 2; // Minimum number of obstacles between color changers

    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private bool spawnColorChangerNext = false;
    private float largeObstacleWidth;

    private void Start()
    {
        // Determine the width of the large obstacle to help with spacing
        largeObstacleWidth = largeObstaclePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        InvokeRepeating("SpawnObstacle", spawnRate, spawnRate);
    }

    private void SpawnObstacle()
    {
        Vector2 spawnPosition;

        if (spawnColorChangerNext)
        {
            // Ensure color changer is spawned at the top or bottom of the screen
            spawnPosition = GetPositionForColorChanger();
            GameObject colorChanger = Instantiate(colorChangerPrefab, spawnPosition, Quaternion.identity);
            SetRandomColor(colorChanger);
            spawnColorChangerNext = false; // Reset flag
        }
        else
        {
            // Randomly choose between large obstacle, small obstacle, or nothing
            if (Random.value < 0.5f) // 50% chance to spawn a large obstacle
            {
                spawnPosition = GetPositionForLargeObstacle();
                GameObject largeObstacle = Instantiate(largeObstaclePrefab, spawnPosition, Quaternion.identity);
                spawnedObstacles.Add(largeObstacle);

                // Decide if the next obstacles should be color changers or small obstacles
                if (Random.value < 0.3f) // 30% chance to spawn a color changer next
                {
                    spawnColorChangerNext = true;
                }
            }
            else if (Random.value < 0.7f) // 70% chance to spawn a small obstacle
            {
                spawnPosition = GetPositionForSmallObstacle();
                GameObject smallObstacle = smallObstaclePrefabs[Random.Range(0, smallObstaclePrefabs.Length)];
                Instantiate(smallObstacle, spawnPosition, Quaternion.identity);
            }

            // Optionally, spawn a color changer after a certain number of obstacles
            // Adjust the probability or conditions as needed
            if (Random.value < 0.3f) // 30% chance to spawn a color changer
            {
                spawnColorChangerNext = true;
            }
        }

        // Remove off-screen obstacles
        spawnedObstacles.RemoveAll(obstacle => obstacle != null && obstacle.transform.position.x < -10f);
    }

    private Vector2 GetPositionForLargeObstacle()
    {
        Vector2 spawnPosition;
        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };

        // Choose a random Y position from the allowed positions
        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

        return spawnPosition;
    }

    private Vector2 GetPositionForSmallObstacle()
    {
        // Choose a random Y position from the allowed positions
        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };
        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        return new Vector2(spawnXPosition, spawnYPosition);
    }

    private Vector2 GetPositionForColorChanger()
    {
        // Choose a random Y position from the allowed positions
        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };
        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        return new Vector2(spawnXPosition, spawnYPosition);
    }

    private void SetRandomColor(GameObject colorChanger)
    {
        // Assuming your color changer prefab has a SpriteRenderer
        SpriteRenderer spriteRenderer = colorChanger.GetComponent<SpriteRenderer>();
        Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
        spriteRenderer.color = colors[Random.Range(0, colors.Length)];
    }
}
