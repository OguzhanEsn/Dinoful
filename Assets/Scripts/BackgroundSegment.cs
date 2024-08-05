using UnityEngine;

public class BackgroundSegment : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    public Renderer backgroundRenderer;

    private void Update()
    {
        Vector2 offset = new Vector2(Time.time * backgroundSpeed, 0);
        backgroundRenderer.material.mainTextureOffset = offset;
    }
}
