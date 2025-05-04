using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float gameDuration = 60f;
    [SerializeField] private TMP_Text timerText;
    

    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject button;
    [SerializeField] private Button  restartButton;
    [SerializeField] private Button nextCredit;

    private float currentTime;
    private bool isGameActive;

    void Start()
    {
        // ��駤�һ���
        restartButton.onClick.AddListener(RestartGame);
        nextCredit.onClick.AddListener(LoadCredit);

        // ��͹ Panel �͹�������
        gameOverPanel.SetActive(false);

        StartGame();
    }

    public void StartGame()
    {
        currentTime = gameDuration;
        isGameActive = true;
        Time.timeScale = 1f; // �����������
    }

    void Update()
    {
        if (!isGameActive) return;

        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateTimerDisplay();
            EndGame();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";

        if (currentTime <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 0f; // ��ش������
        gameOverPanel.SetActive(true);
        ScoreText.SetActive(true);
        button.SetActive(true);

        
        // ��Ǩ�ͺ����մ�ҹ�����������
        nextCredit.gameObject.SetActive(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1);


    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadCredit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
