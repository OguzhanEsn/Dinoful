using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public RuntimeAnimatorController[] animators; // An array of animators for different colors
    private int currentColorIndex = 0;

    public int totalLives = 3;
    private int currentLives;
    public TextMeshProUGUI lifeText;
    public Animator hurtAnimator;

    private Animator animator;

    private bool isMoving = false;
    private bool facingRight = true;

    public TrailRenderer trailRenderer;

    public float powerUpSpeedMultiplier = 2f;
    public float powerUpSizeMultiplier = 1.5f;

    private float originalSpeed;
    private Vector3 originalSize;

    public TextMeshProUGUI obstacleCountText;
    private int obstacleCount = 0;
    public GameObject thrownObstaclePrefab;
    public Transform throwPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentLives = totalLives;
        lifeText.text = currentLives.ToString();

        ChangeColor(currentColorIndex); // Set initial color


        originalSpeed = moveSpeed;
        originalSize = transform.localScale;
    }

    private void Update()
    {
        HandleMovementAndColorChange();

    }

    private void HandleMovementAndColorChange()
    {
        float moveDirection = 0;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    moveDirection = -1;
                    ChangeColor((currentColorIndex - 1 + animators.Length) % animators.Length);
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    moveDirection = 1;
                    ChangeColor((currentColorIndex + 1) % animators.Length);
                }
            }
            else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    moveDirection = -1;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    moveDirection = 1;
                }
            }
        }

        Vector3 movement = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(movement);

        isMoving = moveDirection != 0;

    }

    private void ChangeColor(int colorIndex)
    {
        currentColorIndex = colorIndex;
        animator.runtimeAnimatorController = animators[currentColorIndex];
        UpdateTrailColor();

    }
    private void UpdateTrailColor()
    {
        Color trailColor = GetCurrentColor();
        trailColor.a = .5f;
        trailRenderer.startColor = trailColor;

        trailRenderer.endColor = trailColor;
    }




    /* private void FlipSprite(float moveDirection)
     {
         if (moveDirection > 0 && !facingRight)
         {
             transform.localScale = new Vector3(-1, 1, 1);
             facingRight = true;
         }
         else if (moveDirection < 0 && facingRight)
         {
             transform.localScale = new Vector3(1, 1, 1);
             facingRight = false;
         }
     }



   /*  private void CollectObstacle(Color color)
     {
         obstacleCount++;
         UpdateObstacleCountUI();
     }

     private void UpdateObstacleCountUI()
     {
         obstacleCountText.text = "Obstacles collected: " + obstacleCount;
     }

     private void ThrowObstacle()
     {
         if (obstacleCount > 0)
         {
             GameObject thrownObstacle = Instantiate(thrownObstaclePrefab, throwPoint.position, throwPoint.rotation);
             ThrownObstacle thrownObstacleScript = thrownObstacle.GetComponent<ThrownObstacle>();
             if (thrownObstacleScript != null)
             {
                 thrownObstacleScript.color = GetCurrentColor();
                 thrownObstacle.GetComponent<SpriteRenderer>().color = thrownObstacleScript.color;
             }

             // Add force to throw the obstacle
             Rigidbody2D rb = thrownObstacle.GetComponent<Rigidbody2D>();
             if (rb != null)
             {
                 rb.AddForce(transform.right * 500f); // Adjust the force as needed
             }

             obstacleCount--;
             UpdateObstacleCountUI();
         }
     }

     public IEnumerator ActivatePowerUp(PowerUp.PowerUpType powerUpType, float duration)
     {
         switch (powerUpType)
         {
             case PowerUp.PowerUpType.Speed:
                 moveSpeed *= powerUpSpeedMultiplier;
                 break;
             case PowerUp.PowerUpType.Size:
                 transform.localScale *= powerUpSizeMultiplier;
                 break;
         }
         yield return new WaitForSeconds(duration);
         switch (powerUpType)
         {
             case PowerUp.PowerUpType.Speed:
                 moveSpeed = originalSpeed;
                 break;
             case PowerUp.PowerUpType.Size:
                 transform.localScale = originalSize;
                 break;
         }
     }

     private void LoseLife()
     {
         currentLives--;
         hurtAnimator.SetTrigger("Hurt"); // Play hurt animation
         lifeText.text = currentLives.ToString();
         if (currentLives <= 0)
         {
             return;
         }
     }*/

    private Color GetCurrentColor()
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
