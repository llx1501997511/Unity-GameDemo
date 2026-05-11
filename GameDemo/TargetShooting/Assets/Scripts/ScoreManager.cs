using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager Instance;
    [Header("UI 引用")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI remainingTargetsText;  // 新增：剩余靶子数显示
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public GameObject startPanel;  // 新增：开始面板引用

    [Header("游戏参数")]
    public float gameTime = 30f;     // 游戏时长

    private int score = 0;
    private float timeRemaining;
    private bool isGameActive = false;  // 改为 false，等待开始按钮
    private TargetSpawner targetSpawner;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 初始状态：显示开始面板，隐藏结束面板
        if (startPanel != null)
            startPanel.SetActive(true);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        timeRemaining = gameTime;
        UpdateUI();

        // 获取生成器引用
        targetSpawner = FindObjectOfType<TargetSpawner>();

        // 游戏未开始时，禁止玩家移动和射击
        SetPlayerControls(false);

        // 锁定鼠标到游戏窗口（开始游戏后才锁定）
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!isGameActive) return;

        // 倒计时
        timeRemaining -= Time.deltaTime;
        UpdateUI();

        // 时间到，结束游戏
        if (timeRemaining <= 0)
        {
            EndGame(false);  // 时间结束
        }
    }
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
        if (remainingTargetsText != null && targetSpawner != null)
            remainingTargetsText.text = "Targets: " + targetSpawner.GetRemainingTargets();
    }
    public void AddScore(int points)
    {
        if (!isGameActive) return;
        score += points;
        UpdateUI();
    }
    // 胜利结束（所有靶子打完）
    public void GameWin()
    {
        if (!isGameActive) return;
        EndGame(true);
    }


    void EndGame(bool isWin)
    {
        isGameActive = false;

        // 显示结束面板
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            string winText = isWin ? "Win！" : "Time Out！";
            if (finalScoreText != null)
                finalScoreText.text = winText + "\nFinal Score: " + score;
        }

        // 释放鼠标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 禁止玩家控制
        SetPlayerControls(false);
    }

    // 开始游戏（由开始按钮调用）
    public void StartGame()
    {
        // 重置状态
        isGameActive = true;
        score = 0;
        timeRemaining = gameTime;

        // 隐藏开始面板和结束面板
        if (startPanel != null)
            startPanel.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // 重新生成靶子
        if (targetSpawner != null)
            targetSpawner.ResetAndRespawn();

        // 重置玩家位置（可选的）
        ResetPlayerPosition();

        // 启用玩家控制
        SetPlayerControls(true);

        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateUI();
    }
    // 重玩游戏（由Restart按钮调用）
    public void RestartGame()
    {
        StartGame();  // 直接调用开始游戏逻辑
    }

    // 退出游戏（由退出按钮调用）
    public void QuitGame()
    {
        #if UNITY_EDITOR
            // 在编辑器中停止运行
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 在打包后的游戏中退出
            Application.Quit();
        #endif
    }
    void SetPlayerControls(bool enable)
    {
        // 禁用/启用玩家移动脚本
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = enable;

        // 禁用/启用射击脚本
        Shooter shooter = FindObjectOfType<Shooter>();
        if (shooter != null)
            shooter.enabled = enable;
    }
    void ResetPlayerPosition()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0, 1, 5);

            // 重置摄像机旋转
            Camera cam = Camera.main;
            if (cam != null)
            {
                cam.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            // 重置玩家身体的旋转
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
