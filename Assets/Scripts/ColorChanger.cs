using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public float speed = 5f;
    private SpriteRenderer sr;
    private int currentColorIndex = 0;
    private float colorChangeInterval = 1f; // Change color every second

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColor", colorChangeInterval, colorChangeInterval);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10f) // Arbitrary off-screen position
        {
            Destroy(gameObject);
        }
    }

    private void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % 4; // Assuming 4 colors
        sr.color = GetCurrentColor();
    }

    public Color GetCurrentColor()
    {
        switch (currentColorIndex)
        {
            case 0: return Color.blue;
            case 1: return Color.red;
            case 2: return Color.yellow;
            case 3: return Color.green;
            default: return Color.white;
        }
    }
}
