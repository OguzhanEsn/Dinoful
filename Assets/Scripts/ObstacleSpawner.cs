using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject largeObstaclePrefab;
    public GameObject[] smallObstaclePrefabs; 
    public GameObject colorChangerPrefab;
    public float spawnRate = 1f;
    public float spawnXPosition = 10f;
    public float bottomYPosition1 = -4f;
    public float bottomYPosition2 = -3f;
    public float topYPosition1 = 4f;
    public float topYPosition2 = 3f;
    public float minObstacleDistance = 2f; 
    public int minObstaclesBetweenColorChangers = 2; 

    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private bool spawnColorChangerNext = false;
    private float largeObstacleWidth;

    private void Start()
    {
        largeObstacleWidth = largeObstaclePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        InvokeRepeating("SpawnObstacle", spawnRate, spawnRate);
    }

    private void SpawnObstacle()
    {
        Vector2 spawnPosition;

        if (spawnColorChangerNext)
        {
            spawnPosition = GetPositionForColorChanger();
            GameObject colorChanger = Instantiate(colorChangerPrefab, spawnPosition, Quaternion.identity);
            SetRandomColor(colorChanger);
            spawnColorChangerNext = false; 
        }
        else
        {
            if (Random.value < 0.5f) 
            {
                spawnPosition = GetPositionForLargeObstacle();
                GameObject largeObstacle = Instantiate(largeObstaclePrefab, spawnPosition, Quaternion.identity);
                spawnedObstacles.Add(largeObstacle);

                if (Random.value < 0.3f) 
                {
                    spawnColorChangerNext = true;
                }
            }
            else if (Random.value < 0.7f) 
            {
                spawnPosition = GetPositionForSmallObstacle();
                GameObject smallObstacle = smallObstaclePrefabs[Random.Range(0, smallObstaclePrefabs.Length)];
                Instantiate(smallObstacle, spawnPosition, Quaternion.identity);
            }

            if (Random.value < 0.3f) // 30% chance to spawn a color changer
            {
                spawnColorChangerNext = true;
            }
        }


        spawnedObstacles.RemoveAll(obstacle => obstacle != null && obstacle.transform.position.x < -10f);
    }

    private Vector2 GetPositionForLargeObstacle()
    {
        Vector2 spawnPosition;
        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };

        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

        return spawnPosition;
    }

    private Vector2 GetPositionForSmallObstacle()
    {

        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };
        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        return new Vector2(spawnXPosition, spawnYPosition);
    }

    private Vector2 GetPositionForColorChanger()
    {

        float[] possibleYPositions = { bottomYPosition1, bottomYPosition2, topYPosition1, topYPosition2 };
        float spawnYPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        return new Vector2(spawnXPosition, spawnYPosition);
    }

    private void SetRandomColor(GameObject colorChanger)
    {

        SpriteRenderer spriteRenderer = colorChanger.GetComponent<SpriteRenderer>();
        Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
        spriteRenderer.color = colors[Random.Range(0, colors.Length)];
    }
}
