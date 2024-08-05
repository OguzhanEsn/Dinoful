using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
        else if (other.CompareTag("ColorChanger"))
        {
            if (playerController.GetCurrentColor() != other.GetComponent<ColorChanger>().GetCurrentColor())
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
