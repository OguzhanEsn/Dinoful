using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float score;
    private bool isGameOver;

    void Start()
    {
        score = 0;
        isGameOver = false;
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
