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
        speed += difficultyIncreaseRate * Time.deltaTime;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
