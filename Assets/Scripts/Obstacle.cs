using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float difficultyIncreaseRate = 0.1f;
    private float speed;

    void Start()
    {
        speed = initialSpeed;
    }

    void Update()
    {
        // Increase speed gradually over time
        speed += difficultyIncreaseRate * Time.deltaTime;

        // Move the obstacle to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroy the obstacle if it goes off-screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
