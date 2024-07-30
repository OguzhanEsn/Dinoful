using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Color color;
    public float fallSpeed = 2f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(gameObject);
        }
    }
}
