using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public RuntimeAnimatorController[] animators; // An array of animators for different colors
    private int currentColorIndex = 0;

    public int totalLives = 3;
    private int currentLives;
    public Animator hurtAnimator;

    private Animator animator;

    private bool isMoving = false;
    private bool facingRight = true;

    public TrailRenderer trailRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentLives = totalLives;
        ChangeColor(currentColorIndex); // Set initial color
        SetIdleState();
    }

    private void Update()
    {
        HandleMovementAndColorChange();
        UpdateAnimationState();
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

        if (isMoving)
        {
            FlipSprite(-moveDirection);
        }
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

    private void UpdateAnimationState()
    {
        if (isMoving)
        {
            SetWalkingState();
        }
        else
        {
            SetIdleState();
        }
    }


    private void SetWalkingState()
    {
        animator.SetBool("isWalking", true);
    }

    private void SetIdleState()
    {
        animator.SetBool("isWalking", false);
    }

    private void FlipSprite(float moveDirection)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            if (obstacle.color == GetCurrentColor())
            {
                Destroy(other.gameObject);
            }
            else
            {
                LoseLife();
            }
        }
    }

    private void LoseLife()
    {
        currentLives--;
        hurtAnimator.SetTrigger("Hurt"); // Play hurt animation

        if (currentLives <= 0)
        {
            // Handle game over logic here
        }
    }

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
