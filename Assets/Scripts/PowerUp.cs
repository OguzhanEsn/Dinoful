using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Size }
    public PowerUpType powerUpType;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
               // StartCoroutine(player.ActivatePowerUp(powerUpType, duration));
                Destroy(gameObject);
            }
        }
    }
}
