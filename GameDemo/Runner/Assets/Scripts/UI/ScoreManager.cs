using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    private float timer = 0f;
    private int currentScore = 0;

    void Update()
    {
        // 获取玩家的游戏状态（需要引用PlayerController）
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && player.IsGameOver()) return;  // 需要添加IsGameOver方法

        timer += Time.deltaTime;
        currentScore = Mathf.FloorToInt(timer);
        scoreText.text = "Score: " + currentScore;

    }
}
