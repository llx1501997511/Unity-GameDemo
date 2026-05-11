using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // 引用UI面板
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    private float timer = 0f;
    private int currentScore = 0;

    // 引用玩家和生成器（用于重置）
    public PlayerController player;
    public Spawner spawner;

    private bool isGameRunning = false;

    void Start()
    {
        // 初始状态：显示开始面板，隐藏结束面板
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        // 游戏未开始时，停止生成器
        if (spawner != null)
            spawner.enabled = false;
    }
    private void Update()
    {
        // 获取玩家的游戏状态（需要引用PlayerController）
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && player.IsGameOver()) return;  // 需要添加IsGameOver方法

        if (isGameRunning)
        {
            timer += Time.deltaTime;
            currentScore = Mathf.FloorToInt(timer);
            scoreText.text = "Score: " + currentScore;
        }
        
    }

    // 开始游戏（由Start按钮调用）
    public void StartGame()
    {
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // 重置玩家
        if (player != null)
            player.RestartGame();

        // 启动生成器
        if (spawner != null)
            spawner.enabled = true;

        timer= 0f;
        isGameRunning = true;
        
    }

    // 游戏结束（由PlayerController调用）
    public void EndGame()
    {
        if (!isGameRunning) return;

        isGameRunning = false;
        gameOverPanel.SetActive(true);

        // 停止生成器
        if (spawner != null)
            spawner.enabled = false;
    }

    // 重玩（由Restart按钮调用）
    public void RestartGame()
    {
        // 先清除所有现有的障碍物
        ClearAllObstacles();

        // 重置玩家
        if (player != null)
            player.RestartGame();

        // 重新开始
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // 重置生成器计时
        if (spawner != null)
        {
            spawner.enabled = true;
            spawner.ResetTimer();  // 需要在Spawner中添加这个方法
        }
        timer= 0f;
        isGameRunning = true;
    }

    void ClearAllObstacles()
    {
        // 找到场景中所有Obstacle并销毁
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstacles)
        {
            // 如果用了对象池，归还；否则直接销毁
            ObjectPool pool = FindObjectOfType<ObjectPool>();
            if (pool != null)
                pool.ReturnObject(obs);
            else
                Destroy(obs);
        }
    }

    // 退出游戏
    public void QuitGame()
    {
        #if UNITY_EDITOR
                // 在编辑器模式下，停止运行
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // 在打包后的游戏中，退出程序
                Application.Quit();
        #endif
    }
}
