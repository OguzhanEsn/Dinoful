using System.Collections;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float flapForce = 10f;
    public float fallSpeed = 2.5f;
    public Sprite[] colorSprites;
    private int currentColorIndex = 0;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isFlapping = false;
    public TrailRenderer trailRenderer;
    private bool isGameOver = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = colorSprites[currentColorIndex];
        UpdateTrailColor();
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            isFlapping = true;
        }
        else
        {
            isFlapping = false;
        }

        if (isFlapping)
        {
            rb.velocity = new Vector2(rb.velocity.x, flapForce);
        }
        else
        {
            rb.velocity += Vector2.down * fallSpeed * Time.deltaTime;
        }
    }

    public void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colorSprites.Length;
        sr.sprite = colorSprites[currentColorIndex];
        UpdateTrailColor();
    }

    private void UpdateTrailColor()
    {
        Color currentColor = GetCurrentColor();
        trailRenderer.startColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
        trailRenderer.endColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
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
