using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager Instance;  // 单例

    public TextMeshProUGUI scoreText;

    private int score = 0;
    private int totalCoins = 0;  // 总金币数

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 统计场景中所有金币的数量
        totalCoins = FindObjectsOfType<CoinCollector>().Length;
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();

        // 检查是否收集了所有金币
        if (score >= totalCoins)
        {
            Debug.Log("收集了全部金币！");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
                gameManager.WinGame();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score + " / " + totalCoins;
    }
    public int GetCurrentScore()
    {
        return score;
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
}
