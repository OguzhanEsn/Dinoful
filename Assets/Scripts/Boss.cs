using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float colorChangeInterval = 5f;
    public int health = 10;

    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
    private int currentColorIndex = 0;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            sr.color = colors[currentColorIndex];
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ThrownObstacle"))
        {
            ThrownObstacle thrownObstacle = other.GetComponent<ThrownObstacle>();
            if (thrownObstacle != null && thrownObstacle.color == sr.color)
            {
                TakeDamage();
                Destroy(other.gameObject); // Destroy the obstacle after it hits the boss
            }
        }
    }

    private void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
