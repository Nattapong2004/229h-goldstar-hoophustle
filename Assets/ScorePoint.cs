using TMPro;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateAmmoUI();

    }

    private void UpdateAmmoUI()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = $"Score: {currentScore}";
            scoreText.text = $"Score: {currentScore}";
        }

    }

}
